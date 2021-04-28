using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaRebota : MonoBehaviour
{
    private float startTime;
    private float roundTime = 3.5f;
    [Range(0.1f, 0.9f)] public float damage = .3f;
    public float fuerza = 60f;
    Rigidbody rb;
    int countRebotes = 0;
    public int maxRebotes;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        Debug.LogError(startTime);
        roundTime = 3f; // seconds.
        rb = GetComponent<Rigidbody>();
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
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag.Equals("Enemies"))
        {
            countRebotes++;
            collision.gameObject.transform.root.gameObject.GetComponent<GenericEnemy>().OnHit(damage);
            Destroy(gameObject);
        }
    }
}
