using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBigfoot : GenericEnemy
{
    public EnemyBigfoot(int currentCheckpoint, float attackRadius, float pursuitRadius, float movementSpeed, float attackDmg, Platformer3D playerUserController, Transform[] checkpoints, bool status, bool patrolStatus, EnemyHealthBar healthbar, float hitPoints, float maxHitPoints, float savedTime, float timeBetweenAttacks, float onDeathDisabledTime)
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
    }

    // Update is called once per frame
    void Update()
    {
        enemyBehaviourController();
    }

    public override void attack()
    {
        Debug.Log("TE VOY A MATAR ZORRA");
        // Si esta muy cerca ataca
        if ((Time.time - SavedTime) > TimeBetweenAttacks)
        {
            SavedTime = Time.time;
            playerUserController.takeHit(attackDmg);
            playerUserController.knockbackOnDirection(transform.forward);
        }
    }
}
