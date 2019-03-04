using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{


    [SerializeField] private float timeOnBlack;
    [SerializeField] private Transform blackPanel;
    [SerializeField] private Transform gameOverPanel;
    [SerializeField] private Transform winPanel;
    [SerializeField] private Transform gameplayPanel;

    [SerializeField] private Text loopsCountText;
    [SerializeField] private Text timerUI;


    private bool alreadyPlaying;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.levelGUI = this;
    }

    // Update is called once per frame


    public void UpdateTimeGUI(float _time, float _totalTime)
    {
        var timeToUI = _totalTime - _time;
        

        timeToUI = Mathf.RoundToInt(timeToUI);
        
        timerUI.text = timeToUI.ToString();
        
        
    }

    public void StartGameplayUI()
    {
        alreadyPlaying = true;
        blackPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        gameplayPanel.gameObject.SetActive(true);
        
        GameManager.instance.StartRound();

    }

    public void GameOverUI()
    {
   
        gameplayPanel.gameObject.SetActive(false);

        StartCoroutine(WaitOnBlack(timeOnBlack));
    }

    public void GameSuccessUI()
    {
        winPanel.gameObject.SetActive(true);
        gameplayPanel.gameObject.SetActive(false);
    }

    public void UpdateLoopText(int actualLoops, int totalLoops)
    {
        loopsCountText.text = actualLoops + " < " + totalLoops;
    }

    public void NextLevelUI()
    {
        GameManager.instance.ChangeToNextLevel();
    }

    IEnumerator WaitOnBlack(float _time)
    {
        blackPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(_time);

        if (!alreadyPlaying)
        {
            gameOverPanel.gameObject.SetActive(true);

        }
    }
}
