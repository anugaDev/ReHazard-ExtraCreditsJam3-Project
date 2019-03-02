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
    
    
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            

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
        levelShadowcreator.ResetShadows();
        levelShadowcreator.SetPlayerRecords(roundPlayerRecords);
        levelShadowcreator.CreateShadows();
        playerMov.ResetToSpawn();
        playerRec.ResetRecord();
        
        playerRec.BeginRound();
        roundStartTime = Time.time;
    }

    public void StoreRecordRound(List<MovementRecord> _movements, List<ShootingRecord> _shootings)
    {
        print(_movements.Count);
        roundPlayerRecords.Add(new RoundRecordContainer(_movements,_shootings));
    }

    private void CheckForFinish()
    
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            FinishRound(true);
        }
        
    }

    public void FinishRound(bool _win)
    {
        if (_win)
        {
            playerRec.isRecording = false;
            playerRec.RecordRound();


            StartRound();

        }
        else
        {
            playerRec.isRecording = false;
            StartRound();
        }
    }


    
}
