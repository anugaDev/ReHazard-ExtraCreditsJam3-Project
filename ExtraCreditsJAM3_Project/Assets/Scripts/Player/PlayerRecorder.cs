using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecorder : MonoBehaviour
{
    public bool isRecording;
    [SerializeField] private float recordTime;

    public List<MovementRecord> playerMovementRecords = new List<MovementRecord>();
    public List<ShootingRecord> playerShootingRecords = new List<ShootingRecord>();
    
    // Start is called before the first frame update
    void Start()
    {
        

        BeginRound();
        GameManager.instance.playerRec = this;
    }


    public void BeginRound()
    {
        StartCoroutine(RecordGameplay());
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
        isRecording = false;
        GameManager.instance.StoreRecordRound(playerMovementRecords,playerShootingRecords);
    }

    public void ResetRecord()
    {
        playerMovementRecords = new List<MovementRecord>();
        playerShootingRecords = new List<ShootingRecord>();
        isRecording = true;
        BeginRound();
    }
}
