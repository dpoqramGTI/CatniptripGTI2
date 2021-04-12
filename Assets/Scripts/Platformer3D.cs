using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Platformer3D : MonoBehaviour
{
    CharacterController characterController;
    public Transform enemyTransform;
    public NavMove enemigoClass;
    private float dist;
    [Header("Movimiento")]
    public float velocidad = 6.0f;
    public float salto = 1.0f;
    public float gravedad = -10.0f;

    private Vector3 moveDirection = Vector3.zero;

    private bool groundedPlayer = false;
    private float attackDmg = 1;
    public float attackRadius = 3;
    // Propiedades healthbar
    public HealthBarBehaviour healthbar;
    public float hitPoints;
    public float maxHitPoints = 5;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        hitPoints = maxHitPoints;
        healthbar.setHealth(hitPoints, maxHitPoints);
    }

    void Update()
    {
        // Ataque
        if (Input.GetKeyDown("left ctrl"))
        {
            dist = Vector3.Distance(enemyTransform.position, transform.position);
            if (dist <= attackRadius)
            {
                enemigoClass.takeHit(attackDmg);
            }
        }

        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && moveDirection.y < 0)
        {
            moveDirection.y = -1f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * velocidad);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            moveDirection.y = Mathf.Sqrt(salto * -3.0f * gravedad);
        }

        moveDirection.y += gravedad * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
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
        velocidad *= 2;
    }
}
