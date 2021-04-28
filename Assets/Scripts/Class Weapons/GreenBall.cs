using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBall : MonoBehaviour, woolBall
{
    public float damage;
    public float range;
    public int jumps;

    [Header("Shooting...")]

    // private GameObject shotInstantiate = new GameObject();
    //public Transform showSpawn;
    public float fireRate = 1;
    private float nextFire;
    private GameObject tr;
   
    public GreenBall()
    {
   
    }

    public void atacar(GameObject shot)
    {
        tr = GameObject.Find("ShotSpawn");
        Debug.Log("ATACAR: ");
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot.gameObject, tr.transform.position, tr.transform.rotation);
        }
    }
    public void atacar(float dist, GenericEnemy enemyClass)
    {
        throw new System.NotImplementedException();
    }
}
