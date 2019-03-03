using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public float roundStartTime;

    [HideInInspector] public
        PlayerMovement playerMov;

    [HideInInspector] public
        PlayerRecorder playerRec;

    private List<RoundRecordContainer> roundPlayerRecords = new List<RoundRecordContainer>();

    public ShadowCreator levelShadowcreator;

    public List<BulletBehaviour> gameBullets = new List<BulletBehaviour>();
    public List<Transform>  levelEnemies, actualEnemies = new List<Transform>();

    public LevelSettings levelSettings;

    public GUIManager levelGUI;

    public bool onRound;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
            DontDestroyOnLoad(this.gameObject);
            

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForFinish();
    }

    public void StartRound()
    {
        playerMov.gameObject.SetActive(true);

        levelSettings.actualLevelTime = 0;
        
        levelShadowcreator.SetPlayerRecords(roundPlayerRecords);
        levelShadowcreator.CreateShadows();
        playerMov.ResetToSpawn();
        playerRec.ResetRecord();
        
        playerRec.BeginRound();
        roundStartTime = Time.time;
    }

    public void StoreRecordRound(List<MovementRecord> _movements, List<ShootingRecord> _shootings)
    {
        
        roundPlayerRecords.Add(new RoundRecordContainer(_movements,_shootings));
    }

    private void CheckForFinish()
    
    {
        if (Input.GetKeyDown(KeyCode.R) && levelShadowcreator.levelShadows.Count <= 0 &&  levelEnemies.Count <= 0)
        {
            FinishRound(true);
        }
        else if (levelSettings.actualLevelTime > levelSettings.levelTime)
        {
            FinishRound(false);
        }
        else
        {
            levelSettings.actualLevelTime += Time.deltaTime;
            
            levelGUI.UpdateTimeGUI(levelSettings.actualLevelTime,levelSettings.levelTime);
            
            levelGUI.UpdateLoopText(levelSettings.actualLoops,levelSettings.loopTimes);
            
        }
        
    }

    public void FinishRound(bool _win)
    {
        levelShadowcreator.ResetShadows();

        
        
        if (_win)
        {
            levelSettings.AddEndedLoop();
            playerRec.isRecording = false;
            playerRec.RecordRound();
            
            if (levelSettings.LevelHasEnded())
            {
                levelGUI.GameSuccessUI();
            }
            else
            {
                StartRound();
            }
        }
        else
        {
            playerMov.gameObject.SetActive(false);
            
            levelSettings.ResetLoops();
            
            playerRec.isRecording = false;
            
            roundPlayerRecords = new List<RoundRecordContainer>();
            
            levelGUI.GameOverUI();
           
        }

        foreach (var bullet in gameBullets)
        {
            Destroy(bullet);
                
        }
        gameBullets = new List<BulletBehaviour>();

        
        
        //StartRound();
    }

    public void RemoveShadowAt(ShadowBehaviour _shadow)
    {
        levelShadowcreator.levelShadows.RemoveAt((levelShadowcreator.levelShadows.IndexOf(_shadow)));
        Destroy(_shadow.gameObject);
    }

    public void KillEnemy(Transform enemy)
    {
        levelEnemies.RemoveAt(levelEnemies.IndexOf(enemy));
        Destroy(enemy.gameObject);
    }


    
}
