using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeztGen : MonoBehaviour
{
    System.Random rnd = new System.Random();
    
    void Start() 
    {
        Debug.Log(GenDeztQuestions());
    }
    
    public List<int> GenDeztQuestions()
    {
        int noOfRoots = rnd.Next(2, 5);
        List<int> roots = new List<int>();
        for (int i = 0; i < noOfRoots; i ++) 
        {
            roots.Add(rnd.Next(2002) - 1001); //(-1000, 1000)
        }
        
        List<int> polynomial = new List<int>{1};
        foreach(int root in roots)
        {
            polynomial.Add(0);
            for (int degree = 0; degree < polynomial.Count - 1; degree ++)
            {
                polynomial[degree + 1] += polynomial[degree];
                polynomial[degree] *= -(root);
            }
        }
        return polynomial;
    }
}
