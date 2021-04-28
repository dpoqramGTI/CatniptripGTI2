using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadius : MonoBehaviour
{
    private GenericEnemy otherInside;
    public static bool status = false;
    public float damage = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogError(other.gameObject.tag);
        if (other.gameObject.tag.Equals("Enemies"))
        {
           
            status = true;
            otherInside = other.gameObject.transform.root.gameObject.GetComponent<GenericEnemy>();
            Debug.Log( otherInside);
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
