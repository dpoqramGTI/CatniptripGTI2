using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BalaSwisher : MonoBehaviour
{
    private float startTime;
    private float roundTime = 6f;
    [Range(0.1f, 0.9f)] public float damage = .3f;
    public float fuerza = 60f;
    Rigidbody rb;
    int countRebotes = 0;
    public int maxRebotes;
    private GameObject thirdPersonPlayer;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
       // Debug.LogError(startTime);
        roundTime = 3f; // seconds.
        rb = GetComponent<Rigidbody>();
        thirdPersonPlayer = GameObject.Find("Third Person Player");
        transform.LookAt(thirdPersonPlayer.transform.position);
        rb.AddForce(transform.forward * fuerza, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if ((countRebotes > maxRebotes) || (Time.time - startTime >= roundTime))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    Debug.Log(other);
        if (other.gameObject.name.Equals("Third Person Player"))
        {
            // Le hace daño
            GameObject.Find("Third Person Player").gameObject.GetComponent<Platformer3D>().takeHit(2f);
            Destroy(gameObject);
        }
       
    }

}

