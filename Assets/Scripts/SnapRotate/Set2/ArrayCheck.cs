using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ArrayCheck : MonoBehaviour
{
    public int[] myArray = { 1, 2, 3, 4, 5 };
    public int valueToCheck = 3;

    private void Start()
    {
        bool exists = myArray.Contains(valueToCheck);

        if (exists)
        {
            Debug.Log("The value " + valueToCheck + " exists in the array.");
        }
        else
        {
            Debug.Log("The value " + valueToCheck + " does not exist in the array.");
        }
    }
}
