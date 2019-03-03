using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void KillEnemy()
    {
        this.enabled = false;

    }

    public virtual void SpawnEnemy()
    {
        this.enabled = true;
    }
}
