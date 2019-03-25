using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isAlive;
    // Start is called before the first frame update
    private void Start()
    {
        isAlive = true;
    }
    public virtual void KillEnemy()
    {
        if (!isAlive)
        {
            isAlive = false;
        }
   
    }
    public virtual void SpawnEnemy(Vector3 spawnPos)
    {
        transform.position = spawnPos;
        isAlive = true;

    }
}
