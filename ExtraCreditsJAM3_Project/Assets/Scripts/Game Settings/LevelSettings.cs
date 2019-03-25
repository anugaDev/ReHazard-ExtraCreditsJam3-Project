using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public float levelTime;
    public float actualLevelTime;
    public int loopTimes,actualLoops;

    [SerializeField] private List<Vector3> playerSpawningPositions = new List<Vector3>();
    [SerializeField] private List<Vector3> enemySpawningPositions = new List<Vector3>();
    [SerializeField] private List<Enemy> levelEnemies = new List<Enemy>();
    [SerializeField] private Transform positionPointer;

    private void Start()
    {
        GameManager.Instance.levelSettings = this;

        GameManager.Instance.actualLevelEnemies = levelEnemies;
        actualLoops = 0;
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
        var position = playerSpawningPositions[index];
        return position;
    }
    public Vector3  GetEnemyRandomSpawnPosition(int index)
    {
        var position = enemySpawningPositions[index];
        return position;
    }

    public void FetchEnemies()
    {
        levelEnemies = new List<Enemy>();
        foreach (var e in FindObjectsOfType<Enemy>())
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
