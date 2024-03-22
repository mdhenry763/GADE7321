using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text redScoreText;
    [SerializeField] private TMP_Text blueScoreText;

    public void IncreaseRedScore(int score)
    {
        redScoreText.text = score.ToString();
        
    }

    public void IncreaseBlueScore(int score)
    {
        blueScoreText.text = score.ToString();
    }
    
}
