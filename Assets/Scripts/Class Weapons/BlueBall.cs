using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBall : MonoBehaviour, woolBall
{
    private float nextFire;
    private GameObject tr;

    private float damage = 1;
    public float fireRate;
    private float range = 3f;
    private AttackRadius AttackRadius;
    private Collider AttackRadiusCollider;
    private GenericEnemy enemy;
    private bool insideOf = false;
    
    public BlueBall()
    {

    }

    public void atacar(float dist, GenericEnemy enemyClass)
    {
        AttackRadius = FindObjectOfType<AttackRadius>();
        if (AttackRadius.statusAttack())
        {
            Debug.LogError("INSIDE");
            AttackRadius.getTriggerEnemy().OnHit(damage);

            //AttackRadius.getTriggerEnemy().gameObject.GetComponent<EnemyHealthBar>().onHit(damage);
        }
    }
    public void atacar(GameObject shot)
    {
        throw new System.NotImplementedException();
    }

}
