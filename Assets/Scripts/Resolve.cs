using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resolve : MonoBehaviour { 

   
    public Text pistaText;
    private string input;
    public InputField answer;
    private string message = "PRESIONA Q PARA INVESTIGAR";
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
            //Debug.Log("dentro");
            if (Input.GetKey(KeyCode.Q))
            {
                pistaText.text = "No muerde ni ladra, pero tiene dientes y la casa guarda. ¿Qué es?";
                //pistaController.gameObject.SetActive(true);
                answer.gameObject.SetActive(true);
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                pistaText.text = message;
                //pistaController.gameObject.SetActive(false);
                answer.gameObject.SetActive(false);
                //pistaController.gameObject.SetActive(insideOf);
                pistaText.gameObject.SetActive(false);
            }
        }
        else
        {
            pistaText.text = message;
            //pistaController.gameObject.SetActive(false);
            answer.gameObject.SetActive(false);
            //pistaController.gameObject.SetActive(insideOf);
            pistaText.gameObject.SetActive(false);
           
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
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("Third Person Player"))
        {
            // Destroy(other.gameObject);
            insideOf = false;
        }
    }
    public void ReadStringInput(string s)
    {
        input = s.ToLower();
        Debug.Log(s);
        if (input == "llave" || input == "una llave" || input == "la llave" )
        {
            GameObject door = GameObject.Find("PTK_DoubleDoor_Medium (2)");
            Destroy(door);
        }
    }
}

