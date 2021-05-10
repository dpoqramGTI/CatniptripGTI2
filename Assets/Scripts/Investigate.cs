using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Investigate : MonoBehaviour
{
    public GameObject pistaController;
    public Text pistaText;
    public string mensaje = "";
    Rect rect = new Rect(Screen.width / 2 - 100, 100, 250, 100);
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
            pistaText.gameObject.SetActive(true);
            Debug.Log("dentro");
            if (Input.GetKey(KeyCode.Q))
            {
                pistaController.gameObject.SetActive(true);
            }
            else
            {
                pistaText.text = "MANTÉN PRESIONADO Q PARA INVESTIGAR";
                pistaController.gameObject.SetActive(false);
            }
        }
        else
        {
            pistaController.gameObject.SetActive(insideOf);
            pistaText.gameObject.SetActive(false);
        }
    }

    private void OnGUI()
    {
        if (insideOf)
        {
            GUI.Label(rect, mensaje);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Third Person Player"))
        {
            // Destroy(other.gameObject);
            insideOf = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        insideOf = false;
    }
}
