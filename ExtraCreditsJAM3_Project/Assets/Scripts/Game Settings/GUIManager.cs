﻿using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private Image fillTimerUI;

    [SerializeField] private Transform gameOverPanel;
    [SerializeField] private Transform gameplayPanel;

    [SerializeField] private Text loopsCountText;
    
   
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.levelGUI = this;
    }

    // Update is called once per frame


    public void UpdateTimeGUI(float _time, float _totalTime)
    {
        fillTimerUI.fillAmount = _time / _totalTime;
    }

    public void StartGameplayUI()
    {
        gameOverPanel.gameObject.SetActive(false);
        gameplayPanel.gameObject.SetActive(true);
        
        GameManager.instance.StartRound();

    }

    public void GameOverUI()
    {
        gameOverPanel.gameObject.SetActive(true);
        gameplayPanel.gameObject.SetActive(false);
    }

    public void GameSuccessUI()
    {
        gameplayPanel.gameObject.SetActive(false);
    }

    public void UpdateLoopText(int actualLoops, int totalLoops)
    {
        loopsCountText.text = actualLoops + " < " + totalLoops;
    }
}