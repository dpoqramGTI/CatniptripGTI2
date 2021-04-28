using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
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
        if(other.gameObject.name.Equals("Third Person Player"))
        {
            GameObject.Find("Third Person Player").GetComponent<Platformer3D>().useSpeedItem();
            Destroy(gameObject);
        }
    }
}
