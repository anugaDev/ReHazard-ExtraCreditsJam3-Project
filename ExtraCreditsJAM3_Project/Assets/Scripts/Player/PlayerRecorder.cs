using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecorder : MonoBehaviour
{
    public bool isRecording;
    public List<MovementRecord> playerMovementRecords = new List<MovementRecord>();
    public List<ShootingRecord> playerShootingRecords = new List<ShootingRecord>();
    
    [SerializeField] public float recordTime;
    private IEnumerator recorderStored;
    private void Start()
    {
        BeginRound();
        GameManager.Instance.playerRec = this;
    }


    public void BeginRound()
    {
        isRecording = true;
        
        recorderStored = RecordGameplay();
        StartCoroutine(recorderStored);
    }

    private void AddMovementRecord()
    {
        playerMovementRecords.Add(new MovementRecord(transform.position, transform.right));
        
    }

    public void AddShootingRecord()
    {
        var shootTime = Time.time - GameManager.Instance.roundStartTime;
        playerShootingRecords.Add(new ShootingRecord(shootTime, transform.right));
    }

    private IEnumerator RecordGameplay()
    {
        while (isRecording)
        {
            AddMovementRecord();
            yield return new WaitForSeconds(recordTime);
        }
    }

    public void RecordRound()
    {
        GameManager.Instance.StoreRecordRound(playerMovementRecords,playerShootingRecords);
        
    }
    public void ResetRecord()
    {
        isRecording = false;
        
        StopCoroutine(recorderStored);
        
        playerMovementRecords = new List<MovementRecord>();
        playerShootingRecords = new List<ShootingRecord>();
        
        
    }
}
