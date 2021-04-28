using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform enemyTransform;
    public NavMove enemigoClass;
    private float attackDmg = 1;
    public float attackRadius = 3;
    private float dist;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    // Propiedades healthbar
    public HealthBarBehaviour healthbar;
    public float hitPoints;
    public float maxHitPoints = 5;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = maxHitPoints;
        healthbar.setHealth(hitPoints, maxHitPoints);
        //controller = GetComponent<CharacterController>();
    }

    private void Update()
    {

        // Ataque
        if (Input.GetKeyDown("left ctrl"))
        {
            dist = Vector3.Distance(enemyTransform.position, transform.position);
            if (dist <= attackRadius)
            {
                enemigoClass.OnHit(attackDmg);
            }
        }

        // Movimiento y salto
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

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
            Destroy(gameObject);
        }
    }

    public void useSpeedItem()
    {
        speed *= 2;
    }
    // Update is called once per frame
}
