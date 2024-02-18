using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField]private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameoverPanel;
    private void Awake()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                SetMenu();
                break;
            case GameState.Game:
                SetGame();
                break;
            case GameState.Gameover:
                SetGameover();
                break;
        }
    }

    private void Start()
    {
        //SetMenu();
    }
    private void SetMenu()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameoverPanel.SetActive(false);
    }
    private void SetGame()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameoverPanel.SetActive(false);
    }
    private void SetGameover()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameoverPanel.SetActive(true);
    }
    public void PlayButtonCallback()
    {
        GameManager.instance.SetGameState();
        SetGame();
    }
}
