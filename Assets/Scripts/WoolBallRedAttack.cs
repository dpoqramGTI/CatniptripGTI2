using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoolBallRedAttack : MonoBehaviour
{
    [Range(0.1f, 0.9f)] public float danyo = .3f;
    public float fuerza = 60f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * fuerza, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -1f)
        {
           // Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
       // Destroy(gameObject);
    }
}
