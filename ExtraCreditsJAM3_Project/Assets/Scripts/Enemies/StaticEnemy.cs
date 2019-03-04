using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : Enemy
{
    
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
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
