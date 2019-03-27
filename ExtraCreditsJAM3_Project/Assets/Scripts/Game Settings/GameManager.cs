using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ShadowCreator levelShadowCreator;
    public List<BulletBehaviour> gameBullets = new List<BulletBehaviour>();
    public List<Enemy> actualLevelEnemies = new List<Enemy>();
    public CameraActions levelCamera;
    public LevelSettings levelSettings;
    public GUIManager levelGUI;
    public SoundStateManager soundManager;

    [SerializeField] private float timeBetweenTransitions;

    [HideInInspector] public float roundStartTime;
    [HideInInspector] public PlayerMovement playerMov;
    [HideInInspector] public PlayerRecorder playerRec;
    [HideInInspector] public List<Transform> effectsToDestroy = new List<Transform>();

    private List<RoundRecordContainer> roundPlayerRecords = new List<RoundRecordContainer>();

    public bool onRound, failed,loading;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(FirstStart());
    }
    private IEnumerator FirstStart()
    {
        yield return null;
        
        soundManager.LoopEffect();
        levelCamera.GlitchCameraLoop();
    }
    private IEnumerator LateStart()
    {
        yield return null;
        StartRound();
    }
    private void Update()
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

        playerMov.Respawn();

        levelSettings.actualLevelTime = 0;
        
        levelShadowCreator.SetPlayerRecords(roundPlayerRecords);
        levelShadowCreator.CreateShadows();
        playerMov.ResetToSpawn(levelSettings.GetPlayerRandomSpawnPosition(levelSettings.actualLoops));
        playerRec.ResetRecord();
        
        playerRec.BeginRound();
        roundStartTime = Time.time;

        soundManager.LoopEffect();
        levelCamera.GlitchCameraLoop();
        

    }

    public void StoreRecordRound(List<MovementRecord> _movements, List<ShootingRecord> _shootings)
    {
        roundPlayerRecords.Add(new RoundRecordContainer(_movements,_shootings));
    }

    private void CheckForFinish()
    {
        if (levelShadowCreator.levelShadows.Count <= 0 &&  actualLevelEnemies.Count <= 0 && !loading)
        {
            StartCoroutine(ExecuteLateFinish(true));
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

    private IEnumerator ExecuteLateFinish(bool condition)
    {
        loading = true;
        yield return new WaitForSeconds(timeBetweenTransitions);
        loading = false;
        FinishRound(condition);
            
    }

    public void FinishRound(bool win)
    {
        onRound = false;
        levelShadowCreator.ResetShadows();
        


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
        if (win)
        {
            levelSettings.AddEndedLoop();
            playerRec.isRecording = false;
            playerRec.RecordRound();
            
            
            if (levelSettings.LevelHasEnded())
            {

                soundManager.PlayLevelCompleted();
                playerMov.gameObject.SetActive(false);
                levelCamera.GlitchCameraLoop();

                levelGUI.GameSuccessUI();
            }
            else
            {
                StartRound();
            }
        }
        else
        {
            soundManager.PlayDeath();
            levelCamera.EndLevelAnimationCamera();
            playerMov.PlayerDeath();
            levelSettings.ResetLoops();
            levelGUI.GameOverUI();

            failed = true;
            playerRec.isRecording = false;
            roundPlayerRecords = new List<RoundRecordContainer>();
            
        }
    }
    public void RemoveShadowAt(ShadowBehaviour shadow)
    {
        if (!onRound) return;
        levelShadowCreator.levelShadows.RemoveAt((levelShadowCreator.levelShadows.IndexOf(shadow)));
        shadow.DestroyShadow();
        Destroy(shadow.gameObject);

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
    public static void QuitGame()
    {
        Application.Quit();
    }
}
