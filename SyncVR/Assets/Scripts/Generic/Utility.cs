using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility 
{
    public static void ShuffleArray<T>(T[] arr)
    {
        for (int i = arr.Length - 1; i > 0; --i)
        {
            int r = Random.Range(0, i + 1);
            T tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }
}
