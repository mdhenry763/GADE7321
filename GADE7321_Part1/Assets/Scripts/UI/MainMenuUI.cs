using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI Instance;

    [SerializeField] private GameObject _menu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StatGame()
    {
        _menu.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        _menu.SetActive(true);
        SceneManager.LoadScene(0);
    }
    
}
