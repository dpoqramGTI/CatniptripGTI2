using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public GameObject bala;

    // Start is called before the first frame update
    public void Disparar()
    {
            Instantiate(bala, transform.position, transform.rotation);
    }
}
