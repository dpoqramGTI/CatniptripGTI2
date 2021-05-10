using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjemploRandom : MonoBehaviour
{
    public float maxDistCircle;
    public string[] textos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //randomSpawnCubeOnRadius(maxDistCircle);
            //Debug.Log(Random.ColorHSV());
            //Debug.Log(ExtRandom.choose(textos));
            ExtRandom.RandomizeArray(textos);
        }
    }

    private void randomSpawnCubeOnRadius(float radius)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Vector2 randomVector = Random.insideUnitCircle * radius;
        cube.transform.position = new Vector3(randomVector.x, 0, randomVector.y);
        Debug.Log(randomVector);
    }

    
}
