using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    [SerializeField] private ShapeFillController[] fillControllers;
    private int currentIndex = 0;
    internal Action ShapeCompleted;

    void Start()
    {
        for(int i =0; i<fillControllers.Length ; i++)
        {
            fillControllers[i].filled += StartNext;
            if(i == 0)
            {
                fillControllers[i].gameObject.SetActive(true);
            }
            else
            {
                fillControllers[i].gameObject.SetActive(false);
            }
        }
    }

    void StartNext()
    {
        currentIndex++;
        if (currentIndex < fillControllers.Length)
        {
            fillControllers[currentIndex].gameObject.SetActive(true);
        }
        else
        {
            ShapeCompleted.Invoke();
        }
    }

    
}
