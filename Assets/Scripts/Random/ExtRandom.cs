using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ExtRandom
{
    // Ilist es una interfaz utilizada tanto por array como por list
    // T -> es el tipo genérico puede ser cualquiera
    public static T choose<T>(IList<T> array) {
        return array[Random.Range(0, array.Count)];
    }

    public static void RandomizeArray(IList array)
    {
        int n = array.Count;
        while (n>1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            // var es cualquier tipo de variable
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }
    /*
    public static T ChooseWeighted<T>(Dictionary<T, int> weightedItems)
    {
        int total = weightedItems.Sum(v => v.Value);
        int r = Random.Range(0, total);
        int cuenta = 0;
    }*/


}
