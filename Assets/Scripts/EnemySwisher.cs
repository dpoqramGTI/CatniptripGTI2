using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySwisher : GenericEnemy
{
    public Transform plataformaBoloncho;
    public Transform plataformaEmbestida;
    public GameObject balaSwisher;
    private GameObject ThirdPersonPlayer;
    private int checkpoint = 0;
    private float nextFire = 5f;
    private float nextFire2 = 4f;
    public float distMargin;
    private bool key = false;
    private float SavedTimeCircle = 0;
    private float SavedTimeFireBall = 0;
    private float SavedTimeKnock = 0;
    Rigidbody rb;
    public EnemySwisher(int currentCheckpoint, float attackRadius, float pursuitRadius, float movementSpeed, float attackDmg, Platformer3D playerUserController, Transform[] checkpoints, bool status, bool patrolStatus, EnemyHealthBar healthbar, float hitPoints, float maxHitPoints, float savedTime, float timeBetweenAttacks, float onDeathDisabledTime)
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
        ThirdPersonPlayer = GameObject.Find("Third Person Player");

        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
 
    void Update()
    {
        attack(1);
        if ((Time.time - SavedTime) > 5f)
        {
            Debug.LogError("change");
            SavedTime = Time.time;
            key = !key;
        }
       
        if (key)
        {
          attack(0);
            //Debug.LogWarning("0");
        }
        else
        {
            attack(2);
            //Debug.LogWarning("2");
        }
       
       
        transform.LookAt(ThirdPersonPlayer.transform.position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
   

    public void attack(int attackMode)
    {
        switch (attackMode)
        {
            case 0:
                attckKnockBack();
                break;
            case 1:
                attackRandomCircle();
                break;
            case 2:
                attackBall();
                break;
            case 3:
                attackRunToPlayer();
                // gameObject.transform.position = gameObject.Find("ThirdPersonPlayer").gameObject.transform.position
                break;
        }
    }

    private void attackBall()
    {
        balaSwisher.transform.position = transform.position;
        //Debug.Log(Time.time);


        if ((Time.time - SavedTimeFireBall) > 0.4f)
        {
            SavedTimeFireBall = Time.time;
            Instantiate(balaSwisher.gameObject, balaSwisher.transform.position, balaSwisher.transform.rotation);
            Debug.Log("Instantiate");

        }
    }

    private void attckKnockBack()
    {
        if ((Time.time - SavedTimeKnock) > 1.1f)
        {
            SavedTimeKnock = Time.time;
            float dist = Vector3.Distance(transform.position, ThirdPersonPlayer.gameObject.transform.position);
           // Debug.Log("dist: " + dist);
           // Debug.Log("distMargin: " + distMargin);
            if (dist < distMargin) {
                //Debug.Log("Te empujo");
                playerUserController.knockbackOnDirection(transform.forward*0.9f);
            }
            //ThirdPersonPlayer.gameObject.GetComponent<CharacterController>().SimpleMove(transform.forward * 500);
        }
    }
    private void attackRunToPlayer()
    {
        if (Time.time > nextFire)
        {
            Debug.LogError("INSIDE");

            rb.AddForce(transform.forward * 300f, ForceMode.Impulse);
            if (Time.time < nextFire + 4)
            {
                float dist = Vector3.Distance(transform.position, playerUserController.gameObject.transform.position);
                if (dist < 4) playerUserController.gameObject.GetComponent<CharacterController>().SimpleMove(transform.forward * 500);
                if (Time.time < nextFire + 3.5f) nextFire = Time.time + nextFire;
            }


            //gameObject.transform.position = transform.forward * 1;
        }
    }

    private void attackRandomCircle()
    {
    
        if ((Time.time - SavedTimeCircle) > 0.35f)
        {
            SavedTimeCircle = Time.time;
            Vector3 circle = this.transform.position;
            circle.x = circle.x * Random.insideUnitSphere.x;
            circle.z = circle.z * Random.insideUnitSphere.z;
            circle.y = playerUserController.gameObject.transform.position.y;

            Instantiate(balaSwisher, circle * 10, gameObject.transform.rotation);
            //Debug.Log("TE VOY A MATAR ZORRA");
        }

        

    }
}
