using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    private float startTime;
    private float roundTime;
    private bool status;
    private GenericEnemy otherInside;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        Debug.LogError(startTime);
        roundTime = 2f; // seconds.
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= roundTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogError(other.gameObject.tag);
        if (other.gameObject.tag.Equals("Enemies"))
        {
            status = true;
       other.gameObject.transform.root.gameObject.GetComponent<GenericEnemy>().OnHit(5);
        }
    }
    public GenericEnemy getTriggerEnemy()
    {
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
