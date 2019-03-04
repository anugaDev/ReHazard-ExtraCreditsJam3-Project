using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : Enemy
{
    private AudioSource targetSouce;
    private Animator targetAnimator;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        targetSouce = GetComponent<AudioSource>();
        targetAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
    public override void KillEnemy()
    {
        targetSouce.Play();
        targetAnimator.SetTrigger("dead");

    }

    public virtual void SpawnEnemy()
    {
        gameObject.SetActive(true);
        targetAnimator.SetTrigger("alive");
    }
}
