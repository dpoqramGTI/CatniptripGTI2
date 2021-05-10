using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scenes;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Platformer3D : MonoBehaviour 
{

    public GameObject rojo;
    public GameObject azul;
    public GameObject verde;
    public GameObject amarillo;

    CharacterController characterController;
    public Transform enemyTransform;
    public GenericEnemy enemigoClass;
    private float dist;
    [Header("Attack")]
    public GameObject shotRedBall;
    public GameObject shotGreenBall;
    public GameObject shotBlueBall;
    public GameObject shotYellowBall;
    [Header("Movimiento")]
    public float velocidad = 6.0f;
    public float salto = 1.0f;
    public float gravedad = -10.0f;
    public float rotateSpeed;

    private Vector3 moveDirection = Vector3.zero;
   
    private bool groundedPlayer = false;
    private float attackDmg = 1;
    public float attackRadius = 3;
    // Propiedades healthbar
    public HealthBarBehaviour healthbar;
    private float hitPoints = 1;
    private float maxHitPoints = 1;
    BlueBall bb;
    YellowBall yb;
    RedBall rb;
    GreenBall gb;
    int ballCount= 0;
    GameObject pistaController;
    private bool initHealthBar = false;

    void Start()
    {
        hitPoints = maxHitPoints;
        healthbar.initHealthBar(maxHitPoints);
        bb = new BlueBall();
        rb = new RedBall();
        gb = new GreenBall();
        yb = new YellowBall();
        characterController = GetComponent<CharacterController>();
       // pistaController = GameObject.Find("mates");
       // pistaController.SetActive(false);
    }
    
    void Update()
    {
        if (initHealthBar)
        {
            hitPoints = maxHitPoints;
            healthbar.initHealthBar(maxHitPoints);
            //initHealthBar = true;
        }
        // Ataque
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // Si el jugador ha seleccionado un arma
            ballCount++;
            if (ballCount > 3) ballCount = 0;
            switch (ballCount)
            {
                case 0:
                    Debug.Log(azul);
                    dist = 0f;
                    //Debug.DrawLine(enemyTransform.position, transform.position,Color.red,3f);
                   
                    //rb.atacar(shotRedBall);
                    amarillo.SetActive(false);
                    rojo.SetActive(false);
                    verde.SetActive(false);
                    azul.SetActive(true);
                    break;
                case 1:
                    Debug.Log(rojo);
                    amarillo.SetActive(false);
                    rojo.SetActive(true);
                    verde.SetActive(false);
                    azul.SetActive(false);
                    break;
                case 2:
                    Debug.Log(verde);
                    //Debug.Log(shotGreenBall)
                    amarillo.SetActive(false);
                    rojo.SetActive(false);
                    verde.SetActive(true);
                    azul.SetActive(false);
                    break;
                case 3:
                    Debug.Log(amarillo);
                    amarillo.SetActive(true);
                    rojo.SetActive(false);
                    verde.SetActive(false);
                    azul.SetActive(false);
                    break;
            }
        }

        // Ataque
        if (Input.GetKeyDown("left ctrl"))
        {
            // Si el jugador ha seleccionado un arma
            switch (ballCount)
            {
                case 0:
                    Debug.Log(azul);
                    dist = 0f;
                    //Debug.DrawLine(enemyTransform.position, transform.position,Color.red,3f);
                    bb.atacar(dist, enemigoClass);
                    //rb.atacar(shotRedBall);
                    
                    break;
                case 1:
                    Debug.Log(rojo);
                    rb.atacar(shotRedBall);
                    
                    break;
                case 2:
                    Debug.Log(verde);
                    //Debug.Log(shotGreenBall);
                    gb.atacar(shotGreenBall);
                   
                    break;
                case 3:
                    Debug.Log(amarillo);
                   
                    yb.atacar(shotYellowBall);

                    break;
            }
            /*
             BLUE BALL
             dist = Vector3.Distance(enemyTransform.position, transform.position);
             bb.atacar(dist, enemigoClass);*/

        }

        // Detectar el suelo
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && moveDirection.y < 0)
        {
            moveDirection.y = -1f;
        }

        // Mover el personaje en el suelo
        Vector3 move = transform.forward * Input.GetAxis("Vertical");
        characterController.Move(move * Time.deltaTime * velocidad);

        // Girar el personaje
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime);

        // Saltar
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            // De la fórmula del MRUA -> v0 = sqr(-2 * a * dx)
            moveDirection.y = Mathf.Sqrt(-2f * gravedad * salto);
        }

        // Aplicar efecto de la gravedad
        moveDirection.y += gravedad * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
    public void knockbackOnDirection(Vector3 direction)
    {
        Debug.Log("knockbackdirection" + direction);
        characterController.SimpleMove(direction * 150);
        //characterController.Move(moveDirection * Time.deltaTime);
    }
    public void takeHit(float dmg)
    {
        hitPoints -= 0.1f;
        healthbar.setHealth(hitPoints, maxHitPoints);
        //Debug.Log("hitpoints: " + hitPoints);
        //Debug.Log("maxhitpoints: " + maxHitPoints);
        // Muerte
        if (hitPoints <= 0)
        {
            OnDeath();
        }
    }


    public GameObject Miauerte;
    
    private void OnDeath()
    {
        Destroy(gameObject);
        Miauerte.SetActive(true);
        healthbar.setHealth(maxHitPoints, maxHitPoints);
        
    }

    public void useSpeedItem()
    {
        velocidad *= 2;
    }
}
