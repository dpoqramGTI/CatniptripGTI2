using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scenes
{
    public class Cargador : MonoBehaviour
    {
        public Slider slider;
        public GameObject PantallaBarra;

        public void CargarNivel(int NumeroEscena)
        {
            StartCoroutine(CargarAsync(NumeroEscena));
        }

        IEnumerator CargarAsync(int NumeroEscena)
        {
            AsyncOperation Operacion = SceneManager.LoadSceneAsync(NumeroEscena);
            GameObject.Find("Button").SetActive(false);
            PantallaBarra.SetActive(true);

            while (!Operacion.isDone)
            {
                float Progreso = Mathf.Clamp01(Operacion.progress / .9f);

                slider.value = Progreso;

                yield return null;
            }
        }
    }
}
