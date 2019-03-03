using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public float levelTime,actualLevelTime;
    public int loopTimes,actualLoops;

    [SerializeField] private List<Vector3> playerSpawningPositions = new List<Vector3>();
    [SerializeField] private List<Vector3> enemySpawningPositions = new List<Vector3>();
  


    [SerializeField] private List<Enemy> levelEnemies = new List<Enemy>();

    [SerializeField] private Transform positionPointer;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.levelSettings = this;

        GameManager.instance.actualLevelEnemies = levelEnemies;
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

    public Vector3  GetPlayerRandomSpawnPosition(int index)
    {
//        return playerSpawningPositions[Random.Range(0,playerSpawningPositions.Count - 1)];
        return playerSpawningPositions[index];
    }
    public Vector3  GetEnemyRandomSpawnPosition(int index)
    {
//        var position = enemySpawningPositions[Random.Range(0,enemySpawningPositions.Count - 1)];
        var position = enemySpawningPositions[index];
        
        return position;
    }

    public void FetchEnemies()
    {
        levelEnemies = new List<Enemy>();
        foreach (Enemy e in FindObjectsOfType<Enemy>())
        {
            levelEnemies.Add(e);
            
        }
    }

    public List<Enemy>  ListToManager()
    {
        return new List<Enemy>(levelEnemies);
    }

    public void AddPositionPointerToSpawn()
    {
        playerSpawningPositions.Add(positionPointer.position);
    }
    public void AddPositionPointerToEnemyPositions()
    {
        enemySpawningPositions.Add(positionPointer.position);
    }
}
