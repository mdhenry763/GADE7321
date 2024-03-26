using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text redScoreText;
    [SerializeField] private TMP_Text blueScoreText;
    [SerializeField] private TMP_Text roundText;

    private int round = 0;

    private void Start()
    {
        round++;
        StartCoroutine(ShowRoundText(""));
    }

    public void IncreaseRedScore(int score)
    {
        redScoreText.text = score.ToString();
        round++;
        StartCoroutine(ShowRoundText("Red"));

    }

    public void IncreaseBlueScore(int score)
    {
        blueScoreText.text = score.ToString();
        round++;
        StartCoroutine(ShowRoundText("Blue"));
    }

    IEnumerator ShowRoundText(string winner)
    {
        if (round == 5) yield return null;
        
        WaitForSeconds wait = new WaitForSeconds(3f);
        if (winner != string.Empty)
        {
            roundText.text = $"Winner: {winner}";
        }

        yield return new WaitForSeconds(1.5f);
        roundText.text = $"Round: {round}";
        yield return wait;
        roundText.text = $"";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
    
}
