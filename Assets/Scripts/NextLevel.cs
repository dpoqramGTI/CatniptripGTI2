using System.Collections;
using System.Collections.Generic;
using Assets.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public Cargador cargador;
    private string input;
    //Rect rect = new Rect(Screen.width / 2 - 100, 100, 250, 100);
    private bool inside = false;
    public static bool keyOnPossesion = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (inside && keyOnPossesion)
        {
            Debug.Log("HOLA");
            Debug.Log("HOLAotraves");

            StartCoroutine(CargarAsync(2));
            //Destroy(other.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Third Person Player"))
        {
          
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inside = false;
    }

    IEnumerator CargarAsync(int NumeroEscena)
    {
        AsyncOperation Operacion = SceneManager.LoadSceneAsync(NumeroEscena);

        //PantallaBarra.SetActive(true);

        /*while (!Operacion.isDone)
        {
           
        }*/
        yield return null;
    }
}

