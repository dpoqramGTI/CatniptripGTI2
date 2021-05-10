using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBall : MonoBehaviour,woolBall
{
    public float damage = 5;
    public float range;

    [Header("Shooting...")]
   
   // private GameObject shotInstantiate = new GameObject();
    //public Transform showSpawn;
    public float fireRate = 1;
    private float nextFire;
    private GameObject tr;
    private Bala Bala;
    public float moveSpeed;
    public RedBall()
    {
           
    }
    
    public void atacar(GameObject shot)
    {
        tr = GameObject.Find("ShotSpawn");
        //Debug.Log("ATACAR: ");
        if (Time.time > nextFire) {
             nextFire = Time.time + fireRate;
            Instantiate(shot.gameObject, tr.transform.position, tr.transform.rotation);
            Bala = FindObjectOfType<Bala>();
            if (Bala.statusAttack())
            {
               // Debug.LogError("INSIDE");
                Bala.getTriggerEnemy().OnHit(damage);
            }
        }
    }
    public void atacar(float dist, GenericEnemy enemyClass)
    {
        throw new System.NotImplementedException();
    }
}
