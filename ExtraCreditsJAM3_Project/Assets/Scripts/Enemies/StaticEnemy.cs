using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : Enemy
{
    private AudioSource targetSouce;
    private Animator targetAnimator;
    public float rotationSpeed;
    private void Start()
    {
        targetSouce = GetComponent<AudioSource>();
        targetAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
    public override void KillEnemy()
    {
        targetSouce.Play();
        targetAnimator.SetTrigger("dead");
    }
    public override void SpawnEnemy(Vector3 spawnPos)
    {
        gameObject.SetActive(true);
        targetAnimator.SetTrigger("alive");
        
        base.SpawnEnemy(spawnPos);
    }
}
