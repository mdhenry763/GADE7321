using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text redScoreText;
    [SerializeField] private TMP_Text blueScoreText;
    [SerializeField] private TMP_Text roundText;

    private int round = 0;

    private void Start()
    {
        round++;
        StartCoroutine(ShowRoundText());
    }

    public void IncreaseRedScore(int score)
    {
        redScoreText.text = score.ToString();
        round++;
        StartCoroutine(ShowRoundText());

    }

    public void IncreaseBlueScore(int score)
    {
        blueScoreText.text = score.ToString();
        round++;
        StartCoroutine(ShowRoundText());
    }

    IEnumerator ShowRoundText()
    {
        if (round == 5) yield return null;
        
        WaitForSeconds wait = new WaitForSeconds(3f);
        roundText.text = $"Round: {round}";
        yield return wait;
        roundText.text = $"";
    }
    
}
