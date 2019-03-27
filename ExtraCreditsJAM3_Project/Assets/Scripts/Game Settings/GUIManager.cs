using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private float timeOnBlack;
    private float roundedSecond;
    [SerializeField] private Transform blackPanel;
    [SerializeField] private Transform gameOverPanel;
    [SerializeField] private Transform winPanel;
    [SerializeField] private Transform inGamePanel;

    [SerializeField] private Text loopsCountText;
    [SerializeField] private Text timerUI;

    private bool alreadyPlaying;

    private void Start()
    {
        GameManager.Instance.levelGUI = this;
    }
    public void UpdateTimeGUI(float time, float totalTime)
    {
        var timeToUI = totalTime - time;
        timeToUI = Mathf.RoundToInt(timeToUI);
        if (roundedSecond != timeToUI)
        {
            GameManager.Instance.soundManager.PlaySecond();

            roundedSecond = timeToUI;
        }
        
        
        timerUI.text = timeToUI.ToString(CultureInfo.InvariantCulture);
    }
    public void StartGameplayUI()
    {
        alreadyPlaying = true;
        blackPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        inGamePanel.gameObject.SetActive(true);
        
        GameManager.Instance.StartRound();
    }
    public void GameOverUI()
    {
        alreadyPlaying = false;
        
        inGamePanel.gameObject.SetActive(false);

        StartCoroutine(WaitOnBlack(timeOnBlack));
    }
    public void GameSuccessUI()
    {
        winPanel.gameObject.SetActive(true);
        inGamePanel.gameObject.SetActive(false);
    }
    public void UpdateLoopText(int actualLoops, int totalLoops)
    {
        loopsCountText.text = actualLoops + " < " + totalLoops;
    }
    public void NextLevelUI()
    {
        GameManager.Instance.ChangeToNextLevel();
    }
    private IEnumerator WaitOnBlack(float _time)
    {
        blackPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(_time);

        if (!alreadyPlaying)
        {
            gameOverPanel.gameObject.SetActive(true);

        }
    }

    public void QuitGUI()
    {
        GameManager.QuitGame();
    }
}
