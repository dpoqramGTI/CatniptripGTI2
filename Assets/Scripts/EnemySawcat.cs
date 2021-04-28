using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySawcat : GenericEnemy
{
    public EnemySawcat(int currentCheckpoint, float attackRadius, float pursuitRadius, float movementSpeed, float attackDmg, Platformer3D playerUserController, Transform[] checkpoints, bool status, bool patrolStatus, EnemyHealthBar healthbar, float hitPoints, float maxHitPoints, float savedTime, float timeBetweenAttacks, float onDeathDisabledTime)
    {
        this.checkpoints = checkpoints;
        this.playerUserController = playerUserController;
        this.agent = GetComponent<NavMeshAgent>();
        this.attackRadius = attackRadius;
        this.pursuitRadius = pursuitRadius;
        this.movementSpeed = movementSpeed;
        this.currentCheckpoint = currentCheckpoint;
        this.attackDmg = attackDmg;
        this.patrolStatus = patrolStatus;
        this.status = status;
        this.healthbar = healthbar;
        this.hitPoints = hitPoints;
        this.maxHitPoints = maxHitPoints;
        this.SavedTime = savedTime;
        this.TimeBetweenAttacks = timeBetweenAttacks;
        this.OnDeathDisabledTime = onDeathDisabledTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        status = true;
        //this.updateDropsItemOnDeath(true);
    }

    // Update is called once per frame
    void Update()
    {
        enemyBehaviourController();
    }
    public override void enemyBehaviourController()
    {
        //Debug.Log(status);
        if (status)
        {
            base.enemyBehaviourController();
        }
        else
        {
            //Debug.Log("Now time -> " + Time.time);
            //Debug.Log("Saved time -> " + Time.time);
            // Si esta desactivado despues de x tiempo vuelve a reactivarse -> sawcat
            if ((Time.time - SavedTime) > OnDeathDisabledTime)
            {
                //SavedTime = Time.time;
                restoreHealth();
                status = true;
            }
        }
    }

    public override void OnDeath()
    {
        status = false;
    }
}
