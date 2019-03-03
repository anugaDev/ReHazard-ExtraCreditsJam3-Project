using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{// Start is called before the first frame update

    public bool isAlive;
    
    void Start()
    {
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void KillEnemy()
    {
        
        if (isAlive)
        {
            //this.enabled = false;
            isAlive = false;
        }
    }

    public virtual void SpawnEnemy(Vector3 newPos)
    {
        transform.position = newPos;
        isAlive = true;
        // this.enabled = true;
    }
}
