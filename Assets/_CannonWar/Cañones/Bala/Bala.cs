using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private GenericEnemy otherInside;
    [Range(0.1f,0.9f)] public float danyo = .3f;
    public float fuerza = 60f;
    Rigidbody rb;
    GameObject spawn;
    public float moveSpeed;
    public float range;
    private bool volver = false;
    private float dist;
    public static bool status = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawn = GameObject.Find("ShotSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(spawn.transform.position, rb.transform.position);
       
       // Debug.Log(dist);

        if (( dist <= range ) && !volver )
        {
            //Debug.Log("Hacia delante");
            rb.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (dist > range)
        {
            volver = true;     
        }
        if (volver)
        {
            //Debug.Log("Hacia detras");
            rb.transform.position += (spawn.transform.position - rb.transform.position) * moveSpeed * Time.deltaTime;
        }
       
        if (dist < 0.25f && volver)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemies"))
        {
            status = true;
            otherInside = other.gameObject.transform.root.gameObject.GetComponent<GenericEnemy>();
            otherInside.OnHit(3);
            Debug.Log("ASdasd" + otherInside);
        }
    }
    public GenericEnemy getTriggerEnemy()
    {
        Debug.LogWarning(otherInside);
        return (otherInside);
    }
    public bool statusAttack()
    {
        return status;
    }
    private void OnTriggerExit(Collider other)
    {
        status = false;
    }
}
