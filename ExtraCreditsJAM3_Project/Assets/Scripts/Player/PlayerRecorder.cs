using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecorder : MonoBehaviour
{
    public bool isRecording;
    [SerializeField] public float recordTime;

    public List<MovementRecord> playerMovementRecords = new List<MovementRecord>();
    public List<ShootingRecord> playerShootingRecords = new List<ShootingRecord>();

    private IEnumerator recorderStored;
    // Start is called before the first frame update
    void Start()
    {
        

        BeginRound();
        GameManager.instance.playerRec = this;
    }


    public void BeginRound()
    {
        isRecording = true;
        
        recorderStored = RecordGameplay();
        StartCoroutine(recorderStored);
    }
    public void AddMovementRecord()
    {
        playerMovementRecords.Add(new MovementRecord(transform.position, transform.right));
        
    }

    public void AddShootingRecord()
    {
        var shootTime = Time.time - GameManager.instance.roundStartTime;
        playerShootingRecords.Add(new ShootingRecord(shootTime, transform.right));
    }

    IEnumerator RecordGameplay()
    {
        while (isRecording)
        {
            AddMovementRecord();
            yield return new WaitForSeconds(recordTime);
        }
        
        
    }

    public void RecordRound()
    {
        
        GameManager.instance.StoreRecordRound(playerMovementRecords,playerShootingRecords);
        
    }

    public void ResetRecord()
    {
        isRecording = false;
        
        StopCoroutine(recorderStored);
        
        playerMovementRecords = new List<MovementRecord>();
        playerShootingRecords = new List<ShootingRecord>();
        
        
    }
}
