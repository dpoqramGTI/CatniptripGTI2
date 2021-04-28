using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public float health;
    public float maxHealth = 5;

    public GameObject enemyHBContainer;
    public Slider slider;
    public Transform playerTransform;

    private float attackDmg = 1;
    public float attackRadius = 3;
    private float dist;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        slider.value = health;
        //enemyHBContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTransform);
        slider.value = health;
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
        Debug.LogWarning("On hit barrraputa");
        health -= attackDmg;
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
}