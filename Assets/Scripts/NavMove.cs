using DigitalRuby.PyroParticles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMove : MonoBehaviour, IKillable, IHitableHealable
{
    public Transform[] checkpoints;
    public Transform playerUser;
    public Platformer3D playerUserController;
    private NavMeshAgent agent;
    public float attackRadius;
    public float pursuitRadius;
    public float movementSpeed;
    private float dist;
    private int currentChekpoint = 0;
    private float attackDmg = 1;

    private bool patrolStatus = true;

    public GameObject llaveDropeable;

    // Estado enemigo -> activo/desacticvado
    private bool status = true;

    // Propiedades healthbar
    public HealthBarBehaviour healthbar;
    public float hitPoints;
    public float maxHitPoints = 5;

    // Cada cuanto tiempo ataca el enemigo
    float SavedTime = 0;
    float TimeBetweenAttacks = 2;
    float OnDeathDisabledTime = 10;

    public NavMove(Transform[] checkpoints, Transform playerUser, Platformer3D playerUserController, NavMeshAgent agent, float attackRadius, float pursuitRadius, float movementSpeed, float dist, int currentChekpoint, float attackDmg, bool patrolStatus, GameObject llaveDropeable, bool status, HealthBarBehaviour healthbar, float hitPoints, float maxHitPoints, float savedTime, float timeBetweenAttacks, float onDeathDisabledTime)
    {
        this.checkpoints = checkpoints;
        this.playerUser = playerUser;
        this.playerUserController = playerUserController;
        this.agent = agent;
        this.attackRadius = attackRadius;
        this.pursuitRadius = pursuitRadius;
        this.movementSpeed = movementSpeed;
        this.dist = dist;
        this.currentChekpoint = currentChekpoint;
        this.attackDmg = attackDmg;
        this.patrolStatus = patrolStatus;
        this.llaveDropeable = llaveDropeable;
        this.status = status;
        this.healthbar = healthbar;
        this.hitPoints = hitPoints;
        this.maxHitPoints = maxHitPoints;
        SavedTime = savedTime;
        TimeBetweenAttacks = timeBetweenAttacks;
        OnDeathDisabledTime = onDeathDisabledTime;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(checkpoints[0].position);
        hitPoints = maxHitPoints;
        healthbar.setHealth(hitPoints, maxHitPoints);
    }
    void Update()
    { 
        if (status)
        {
            dist = Vector3.Distance(playerUser.position, transform.position);
            if (dist <= pursuitRadius)
            {
                // Tras hacer un lookat al transform del jugador se inclinaba el game object
                transform.LookAt(playerUser);
                // Endereza el gameobject
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                patrolStatus = false;
                if (dist <= attackRadius)
                {
                    agent.isStopped = true;
                    Debug.Log("TE VOY A MATAR ZORRA");
                    // Si esta muy cerca ataca
                    if ((Time.time - SavedTime) > TimeBetweenAttacks)
                    {
                        SavedTime = Time.time;
                        playerUserController.takeHit(attackDmg);
                        //Debug.Log("transform.forward" + transform.forward);
                        playerUserController.knockbackOnDirection(transform.forward);
                    }
                }
                else
                {
                    if (agent.isStopped)
                    {
                        agent.isStopped = false;
                    }
                    moveTowardsPlayer();
                }
            }
            else
            {
                if (!patrolStatus)
                {
                    patrolStatus = true;
                    agent.ResetPath();
                    currentChekpoint = 0;
                    agent.SetDestination(checkpoints[0].position);
                }

                startPatrol();
            }
        }
        else
        {
            Debug.Log("Now time -> " + Time.time);
            Debug.Log("Saved time -> " + Time.time);
            // Si esta desactivado despues de x tiempo vuelve a reactivarse -> sawcat
            if ((Time.time - SavedTime) > OnDeathDisabledTime)
            {
                //SavedTime = Time.time;
                restoreHealth();
                status = true;
            }
        }
    }

    public void OnHit(float ammount)
    {
        hitPoints -= ammount;
        healthbar.setHealth(hitPoints, maxHitPoints);
        Debug.Log("hitpoints: " + hitPoints);
        Debug.Log("maxhitpoints: " + maxHitPoints);
        // Muerte
        if (hitPoints <= 0)
        {
            OnDeath();
            // Abajo esta la muerte generica con destruccion del gameobject
            //healthbar.disableHealthBar();
            //Destroy(gameObject);
        }
    }

    private void startPatrol()
    {
        //Debug.Log("Checkpoint current: " + currentChekpoint);
        //Debug.Log("Remaining distance: " + agent.remainingDistance);
        if (agent.remainingDistance >= 0 && agent.remainingDistance < 0.1)
        {
            currentChekpoint++;
            if (currentChekpoint == checkpoints.Length)
            {
                currentChekpoint = 0;
            }
            //agent.SetDestination(checkpoints[currentChekpoint % checkpoints.Length].position);
            agent.SetDestination(checkpoints[currentChekpoint].position);
        }
    }


    private void moveTowardsPlayer()
    {
        agent.SetDestination(playerUser.position);
    }

    public void OnDeath()
    {
        SavedTime = Time.time;
        status = false;
        dropItem();
    }

    public void dropItem()
    {
        Instantiate(llaveDropeable, transform.position, Quaternion.identity);
    }
    public void restoreHealth()
    {
        hitPoints = maxHitPoints;
        healthbar.setHealth(hitPoints, maxHitPoints);
    }
    public void OnHeal(float amount)
    {
        throw new System.NotImplementedException();
    }

    public void useFlameThrower()
    {
        GameObject lanzallamas = GameObject.Find("Flamethrower");
        lanzallamas.transform.position = transform.position;
        FireBaseScript scriptLanzallamas = lanzallamas.GetComponent<FireBaseScript>();
        scriptLanzallamas.enabled = true;
    }
}