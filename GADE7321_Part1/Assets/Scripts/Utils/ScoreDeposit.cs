using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDeposit : MonoBehaviour
{
    public int score;
    public FlagHolder flagHolder;

    private void Start()
    {
        score = 0;
    }

    private void IncreaseScore()
    {
        score++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FlagComponent>(out FlagComponent component))
        {
            if (component.FlagHolder == flagHolder)
            {
                component.FlagHolder = FlagHolder.None;
                component.enabled = false; 
            }
            
            
        }
    }
}
