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
    public List<Enemy> actualLevelEnemies = new List<Enemy>();

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

    void Start()
    {
        StartCoroutine(LateStart());
    }
 
    IEnumerator LateStart()
    {
        yield return null;
        
        StartRound();
        //Your Function You Want to Call
    }

    // Update is called once per frame
    void Update()
    {
        if(onRound)
         CheckForFinish();
    }

    public void StartRound()
    {
        onRound = true;
        
        actualLevelEnemies = levelSettings.ListToManager();
        
        print(actualLevelEnemies.Count);

        foreach (var enemy in actualLevelEnemies)
        {
            enemy.gameObject.SetActive(true);
            enemy.SpawnEnemy(levelSettings.GetEnemyRandomSpawnPosition());
        }
        
        playerMov.gameObject.SetActive(true);

        levelSettings.actualLevelTime = 0;
        
        levelShadowcreator.SetPlayerRecords(roundPlayerRecords);
        levelShadowcreator.CreateShadows();
        playerMov.ResetToSpawn(levelSettings.GetPlayerRandomSpawnPosition());
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
        if (Input.GetKeyDown(KeyCode.R) && levelShadowcreator.levelShadows.Count <= 0 &&  actualLevelEnemies.Count <= 0)
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
        
        onRound = false;
        
        levelShadowcreator.ResetShadows();
        
        
        foreach (var enemy in levelSettings.ListToManager())
        {
            enemy.gameObject.SetActive(false);
         
        }
        
        foreach (var bullet in gameBullets)
        {
            Destroy(bullet.gameObject);
                
        }
        gameBullets = new List<BulletBehaviour>();

        
        
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

       

        
        
        //StartRound();
    }

    public void RemoveShadowAt(ShadowBehaviour _shadow)
    {
        levelShadowcreator.levelShadows.RemoveAt((levelShadowcreator.levelShadows.IndexOf(_shadow)));
        Destroy(_shadow.gameObject);
    }

    public void KillEnemy(Enemy enemy)
    {
      
        actualLevelEnemies.RemoveAt(actualLevelEnemies.IndexOf(enemy));
        enemy.KillEnemy();
    }


    
}
