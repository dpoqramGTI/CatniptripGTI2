using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public Cargador cargador;
    private string input;
    //Rect rect = new Rect(Screen.width / 2 - 100, 100, 250, 100);
    private bool insideOf = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (insideOf)
        {
            cargador.NombredelaFuncionysuputamadres
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Third Person Player"))
        {
            //Destroy(other.gameObject);
            insideOf = true;
        }
    }
    
}



