using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDeposit : MonoBehaviour
{
    public int score;
    public FlagHolder flagHolder;
    public GameHUD gameUI;

    private void Start()
    {
        score = 0;
    }

    private void IncreaseScore()
    {
        score++;
        if (flagHolder == FlagHolder.Enemy)
        {
            gameUI.IncreaseRedScore(score);
        }
        else
        {
            gameUI.IncreaseBlueScore(score);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FlagComponent>(out FlagComponent component))
        {
            if (component.FlagHolder == flagHolder)
            {
                component.FlagHolder = FlagHolder.None;
                component.isHolding = false;
            }
            
            
        }
    }
}
