using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public float levelTime,actualLevelTime;
    public int loopTimes,actualLoops;

    [SerializeField] private List<Vector3> playerSpawningPositions = new List<Vector3>();
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.levelSettings = this;
        actualLoops = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEndedLoop()
    {
        actualLoops++;
    }

    public void ResetLoops()
    {
        actualLoops = 0;
    }

    public bool LevelHasEnded()
    {
        return actualLoops > loopTimes;
    }

    public void SetPlayer(Transform player)
    {
        player.position = playerSpawningPositions[Random.Range(0,playerSpawningPositions.Count - 1)];
    }
}
