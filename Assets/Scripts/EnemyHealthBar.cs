using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    private float health;
    public float maxHealth;

    public GameObject enemyHBContainer;
    public Slider enemyHealthBarSlider;
    public Transform playerTransform;

    private float attackDmg = 1;
    public float attackRadius = 3;
    private float dist;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        enemyHealthBarSlider.value = health;
        //enemyHBContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTransform);
        enemyHealthBarSlider.value = health;
        if (health < maxHealth)
        {
            enemyHBContainer.SetActive(true);
        }

        if (health <= 0)
        {
            //Destroy(gameObject);
            enemyHBContainer.SetActive(false);
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void onHit(float attackDmg)
    {
        Debug.Log("antes" + health);
        Debug.LogWarning("On hit barrraputa");
        health -= attackDmg;
        Debug.Log("despues" + health);
    }

    internal void initHealth(float health)
    {
        maxHealth = health;
        this.health = health;
    }
    public void restoreHealth()
    {
        this.health = maxHealth;
        enemyHBContainer.SetActive(true);
    }

    public float getHealth()
    {
        return this.health;
    }
}

