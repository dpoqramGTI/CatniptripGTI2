using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBall : MonoBehaviour
{
    [Header("Shooting...")]

    //private GameObject BalaBomba;
    private Bomba BalaBomba;

    // private GameObject shotInstantiate = new GameObject();
    //public Transform showSpawn;
    public float fireRate = 1;
    private float nextFire;
    private GameObject tr;

    public float damage = 5;
    public float range;

    public YellowBall()
    {
       
    }

    public void atacar(GameObject shot)
    {
        tr = GameObject.Find("Cuerpo");
        Debug.Log("ATACAR: ");
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot.gameObject, tr.transform.position, tr.transform.rotation);
        }
        BalaBomba = FindObjectOfType<Bomba>();
        if (BalaBomba.statusAttack())
        {
            Debug.LogError("INSIDE");
            BalaBomba.getTriggerEnemy().OnHit(damage);
        }
    }
    public void atacar(float dist, GenericEnemy enemyClass)
    {
        throw new System.NotImplementedException();
    }
}

