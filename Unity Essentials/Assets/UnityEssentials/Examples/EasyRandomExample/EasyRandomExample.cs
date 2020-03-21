﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EasyRandomExample : MonoBehaviour
{
    [SerializeField] private KeyCode keyPseudoRandom;
    [SerializeField] private KeyCode keyRandomBool;
    [SerializeField] private KeyCode keyRandomFloat;

    private EasyRandom easyRandom;
    
    // Pseudo random testing
    private List<bool> randomResults = new List<bool>();
    private List<int> workingTryNumbers = new List<int>();
    private int maxTryNumber = 0;
    private int tryNumber = 0;
    int trueBooleans = 0;
    int falseBooleans = 0;
    
    private void Start()
    {
        easyRandom = new EasyRandom();
    }

    void Update()
    {
        // Random bool
        if (Input.GetKey(keyRandomBool))
        {
            bool obtainedBool = easyRandom.GetRandomBool(0.5f);
            if (obtainedBool) trueBooleans++; else falseBooleans++;
            Debug.Log("The gotten value is " + obtainedBool + ". Average = " + (trueBooleans*100f/(trueBooleans+falseBooleans))  + "%");
        }
            
        
        // Random float
        if (Input.GetKey(keyRandomFloat))
            Debug.Log("The gotten value is " + easyRandom.GetRandomFloat(3.5f, 5.0f));
        
        
        // Pseudo random example (and testing)
        if (Input.GetKey(keyPseudoRandom))
        {
            // You can use this code to better understand what is the result of each "RandomBool" method  
            bool result = easyRandom.GetPseudoRandomBool(tryNumber, 0.2f);
            //bool result = random.GetPseudoRandomBool(tryNumber, 15);
            //bool result = random.GetRandomBool(0.2f);
            
            randomResults.Add(result);
        
            if (result) {
                workingTryNumbers.Add(tryNumber);
                if (tryNumber > maxTryNumber)
                    maxTryNumber = tryNumber;
                tryNumber = 0;
            } else {
                tryNumber++;
            }

            int[] stats = new int[maxTryNumber];
            foreach (int regTry in workingTryNumbers)
                stats[regTry-1]++;
            

            int contador = 0;
            foreach (bool r in randomResults)
                if (r) contador++;
        
            Debug.Log("Percentage of positive values: " + (float)contador/(float)randomResults.Count + ". Try with the maximum value: " + maxTryNumber + ". Average needed quantity of tries:" + (workingTryNumbers.Count > 0 ? workingTryNumbers.Average() : 0.0));
            DebugPro.LogEnumerable(stats, "STATS: ");
        }

    }

}