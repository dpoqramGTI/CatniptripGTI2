using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMove : MonoBehaviour
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
    // Propiedades healthbar
    public HealthBarBehaviour healthbar;
    public float hitPoints;
    public float maxHitPoints = 5;

    float SavedTime = 0;
    float DelayTime = 2;  

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(checkpoints[0].position);
        hitPoints = maxHitPoints;
        healthbar.setHealth(hitPoints, maxHitPoints);
    }
    void Update()
    {

        dist = Vector3.Distance(playerUser.position, transform.position);
        if (dist <= pursuitRadius)
        {
            patrolStatus = false;
            Debug.Log("TE VEO ZORRA");
            transform.LookAt(playerUser);
            if (dist <= attackRadius) {
                Debug.Log("TE VOY A MATAR ZORRA");
                // Si esta muy cerca ataca
                if ((Time.time - SavedTime) > DelayTime)
                {
                    SavedTime = Time.time;
                    playerUserController.takeHit(attackDmg);
                }
            }
            else
            {
                // Sino se acerca
                GetComponent<Rigidbody>().AddForce(transform.forward * movementSpeed);
            }
        } else
        {
            // Sino puede perseguir a nadie vuelve a patrullar
            if (!patrolStatus)
            {
                Debug.Log("Vuelvo a patrullar ZORRA");
                //bool patrullastatus = agent.SetDestination(checkpoints[0].position);
                //Debug.Log("status repatrol" + patrullastatus);
                agent.Warp(checkpoints[0].position);
                //agent.SetDestination(checkpoints[1].position);
                patrolStatus = true;
            }
        }
        
        if (agent.remainingDistance == 0 && patrolStatus)
        {
            currentChekpoint++;
            agent.SetDestination(checkpoints[currentChekpoint % checkpoints.Length].position);
        }

    }

    public void takeHit(float dmg)
    {
        hitPoints -= dmg;
        healthbar.setHealth(hitPoints, maxHitPoints);
        Debug.Log("hitpoints: " + hitPoints);
        Debug.Log("maxhitpoints: " + maxHitPoints);
        // Muerte
        if (hitPoints <= 0)
        {
            healthbar.disableHealthBar();
            Destroy(gameObject);
        }
    }
}