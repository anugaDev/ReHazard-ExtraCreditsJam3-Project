using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public CameraActions levelCamera;

    public LevelSettings levelSettings;

    public GUIManager levelGUI;

    public bool onRound, failed, first;
    [HideInInspector]public List<Transform> effectsToDestroy = new List<Transform>();
    
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
        print("start");
        StartCoroutine(LateStart());
    }
 
    IEnumerator LateStart()
    {
        yield return null;
        first = false;
        StartRound();
        //Your Function You Want to Call
    }

    // Update is called once per frame
    void Update()
    {
        if(onRound)
         CheckForFinish();

        else
        {
            if(failed && Input.GetKeyDown(KeyCode.R) )
                levelGUI.StartGameplayUI();
           
        }
    }

    public void StartRound()
    {
        failed = false;
        onRound = true;
        
        actualLevelEnemies = levelSettings.ListToManager();
        

        foreach (var enemy in actualLevelEnemies)
        {
            enemy.gameObject.SetActive(true);
            enemy.SpawnEnemy(levelSettings.GetEnemyRandomSpawnPosition(levelSettings.actualLoops));
        }
        
        playerMov.gameObject.SetActive(true);

        levelSettings.actualLevelTime = 0;
        
        levelShadowcreator.SetPlayerRecords(roundPlayerRecords);
        levelShadowcreator.CreateShadows();
        playerMov.ResetToSpawn(levelSettings.GetPlayerRandomSpawnPosition(levelSettings.actualLoops));
        playerRec.ResetRecord();
        
        playerRec.BeginRound();
        roundStartTime = Time.time;

        levelCamera.GlitchCameraLoop();
    }

    public void StoreRecordRound(List<MovementRecord> _movements, List<ShootingRecord> _shootings)
    {
        
        roundPlayerRecords.Add(new RoundRecordContainer(_movements,_shootings));
    }

    private void CheckForFinish()
    {
        if (levelShadowcreator.levelShadows.Count <= 0 &&  actualLevelEnemies.Count <= 0)
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

        foreach (var effect in effectsToDestroy)
        {
        if(effect != null) Destroy(effect.gameObject);
            
        }
        
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
                playerMov.gameObject.SetActive(false);
                levelCamera.EndLevelAnimationCamera();
                levelGUI.GameSuccessUI();
            }
            else
            {
                StartRound();
            }
        }
        else
        {
            levelCamera.EndLevelAnimationCamera();

            failed = true;

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
        if (!onRound) return;
        levelShadowcreator.levelShadows.RemoveAt((levelShadowcreator.levelShadows.IndexOf(_shadow)));
        _shadow.DestroyShadow();
        Destroy(_shadow.gameObject);

    }

    public void KillEnemy(Enemy enemy)
    {
      
        actualLevelEnemies.RemoveAt(actualLevelEnemies.IndexOf(enemy));
        enemy.KillEnemy();
    }

    public void ChangeToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        roundPlayerRecords = new List<RoundRecordContainer>();
        
        StartCoroutine(LateStart());
    }

    public void ChangeToSpecifiedLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);

        roundPlayerRecords = new List<RoundRecordContainer>();
        
        
        StartCoroutine(LateStart());
    }


    
}
