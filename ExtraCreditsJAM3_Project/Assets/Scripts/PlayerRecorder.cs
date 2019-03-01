using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecorder : MonoBehaviour
{
    public bool isRecording;
    public float recordTime;

    public List<MovementRecord> playerMovementRecords = new List<MovementRecord>();
    public List<ShootingRecord> playerShootingRecords = new List<ShootingRecord>();
    
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(RecordGameplay());
    }

    // Update is called once per frame
    /*
    void Update()
    {
        
    }
    */

    public void AddMovementRecord()
    {
        playerMovementRecords.Add(new MovementRecord(transform.position, transform.forward));
        
    }

    public void AddShootingRecord()
    {
        var shootTime = Time.timeScale - GameManager.instance.roundStartTime;
        playerShootingRecords.Add(new ShootingRecord(shootTime, transform.rotation.eulerAngles.z));
    }

    IEnumerator RecordGameplay()
    {
        while (isRecording)
        {
            AddMovementRecord();
            yield return new WaitForSeconds(recordTime);
        }
    }
}
