using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Clase padre que extiende de monobehaviour
// * Falta el script general que maneja las diferentes mecánicas del enemigo
public class GenericEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public int currentCheckpoint = 0;

    public float attackRadius;
    public float pursuitRadius;
    public float movementSpeed;
    public float attackDmg = 1;
    private GameObject tr;
    // Algunos enemigos dropean aleatoriamente un objeto (como un powerup) o items (como la llave)
    public GameObject dropableItem;

    // Referencia al script de control del jugador
    public Platformer3D playerUserController;
    // Checkpoints por los que patrullará el enemigo
    public Transform[] checkpoints;

    // Estado enemigo -> activo/desactivado (Sawcat)
    public bool status = true;

    // Indica si el enemigo se encuentra patrullando
    public bool patrolStatus;

    // Propiedades healthbar
    public EnemyHealthBar healthbar;
    public float hitPoints;
    public float maxHitPoints;

    // Cada cuanto tiempo ataca el enemigo
    public float SavedTime = 0;
    public float TimeBetweenAttacks = 2;
    public float OnDeathDisabledTime = 10;

    private void Start()
    {
        playerUserController = GetComponent<Platformer3D>();
    }

    public void initHealthbar()
    {
        GameObject.Find("enemyHBKeyCat").gameObject.GetComponent<EnemyHealthBar>().initHealth(maxHitPoints);
        //healthbar.initHealth(maxHitPoints);
    }

    public void returnToPatrol()
    {
        if (!patrolStatus)
        {
            patrolStatus = true;
            agent.ResetPath();
            currentCheckpoint = 0;
            agent.SetDestination(checkpoints[0].position);
        }

        startPatrol();
    }

    public void startPatrol()
    {
        if (agent.remainingDistance >= 0 && agent.remainingDistance < 0.1)
        {
            currentCheckpoint++;
            if (currentCheckpoint == checkpoints.Length)
            {
                currentCheckpoint = 0;
            }
            //agent.SetDestination(checkpoints[currentChekpoint % checkpoints.Length].position);
            agent.SetDestination(checkpoints[currentCheckpoint].position);
        }
    }

    public virtual void enemyBehaviourController()
    {

        tr = GameObject.Find("Third Person Player");
        //Debug.Log("playerUserController.gameObject.transform.position-" + tr.gameObject.transform.position);
        //Debug.Log("transform.position-" + transform.position);
        float dist = Vector3.Distance(tr.gameObject.transform.position, transform.position);
        if (dist <= pursuitRadius)
        {
            // Tras hacer un lookat al transform del jugador se inclinaba el game object
            transform.LookAt(tr.gameObject.transform);
            // Endereza el gameobject
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            patrolStatus = false;
            if (dist <= attackRadius)
            {
                agent.isStopped = true;
                attack();
            }
            else
            {
                if (agent.isStopped)
                {
                    agent.isStopped = false;
                }
                // Se mueve hacia el jugador
                agent.SetDestination(tr.gameObject.transform.position);
            }
        }
        else
        {
            returnToPatrol();
        }
    }

    public virtual void attack()
    {
        //Debug.Log("TE VOY A MATAR ZORRA");
        // Si esta muy cerca ataca
        if ((Time.time - SavedTime) > TimeBetweenAttacks)
        {
            SavedTime = Time.time;
            playerUserController.takeHit(attackDmg);
            //Debug.Log("transform.forward" + transform.forward);
            //playerUserController.knockbackOnDirection(transform.forward);
        }
    }
    public virtual void OnHit(float ammount)
    {
        Debug.Log("Te estan haciendo :" + ammount + " pts de daño");
        hitPoints -= ammount;
        healthbar.onHit(ammount);
        // Debug.Log("hitpoints: " + hitPoints);
        //Debug.Log("maxhitpoints: " + maxHitPoints);
        // Muerte
        if (hitPoints <= 0)
        {
            OnDeath();
            // Abajo esta la muerte generica con destruccion del gameobject
            //healthbar.disableHealthBar();
            //Destroy(gameObject);
        }
    }

    public virtual void OnDeath()
    {
        Debug.Log("Muerte generica");
        status = false;
        Destroy(gameObject);
    }

    public void restoreHealth()
    {
        hitPoints = maxHitPoints;
        healthbar.restoreHealth();
    }

    public void dropItem()
    {
        Debug.LogError("Tu polla");
        Instantiate(dropableItem, transform.position, Quaternion.identity);
    }
}
