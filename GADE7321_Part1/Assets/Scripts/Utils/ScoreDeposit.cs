using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils;

public class ScoreDeposit : MonoBehaviour
{
    public int score;

    [Header("Game End")] 
    public GameObject gameEndScreen;
    public TMP_Text gameEndText;
    
    
    [Header("References: ")]
    public FlagHolder flagHolder;
    public GameHUD gameUI;
    public Respawner flagSpawner;
    
    //Events
    public static event Action OnScored;

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
            OnScored?.Invoke();
        }
        else
        {
            gameUI.IncreaseBlueScore(score);
            OnScored?.Invoke();
        }

        if (score >= 5)
        {
            gameEndScreen.SetActive(true);
            gameEndText.text = $"Winner: {flagHolder.ToString()}";
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FlagComponent>(out FlagComponent component))
        {
            if (component.FlagHolder == flagHolder && component.isHolding == true)
            {
                component.FlagHolder = FlagHolder.None;
                component.isHolding = false;
                IncreaseScore();
                
                var isPlayer = flagHolder == FlagHolder.Player;
                var trans = isPlayer ? flagSpawner.playerFlagSpawn : flagSpawner.enemyFlagSpawn;
                
                flagSpawner.SpawnFlag(true, isPlayer,trans.position );
            }
            
            
        }
    }
}
