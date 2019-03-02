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

    private List<RoundRecordContainer> roundPlayerRecords;

    public ShadowCreator levelShadowcreator;
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
        roundStartTime = Time.time;
    }

    public void StoreRecordRound(List<MovementRecord> _movements, List<ShootingRecord> _shootings)
    {
        roundPlayerRecords.Add(new RoundRecordContainer(_movements,_shootings));
    }

    private void CheckForFinish()
    {
        
    }

    public void FinishRound(bool _win)
    {
        if (_win)
        {
            playerRec.isRecording = false;
            playerRec.RecordRound();
            playerRec.ResetRecord();

        }
        else
        {
            playerRec.isRecording = false;
        }
    }
    
}
