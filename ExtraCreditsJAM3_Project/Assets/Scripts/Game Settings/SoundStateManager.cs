using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStateManager : MonoBehaviour
{
    [SerializeField] private AudioSource effectPlayer;
    [SerializeField] private AudioSource ambiencePlayer;

    [SerializeField] private AudioClip
        deathEffect,
        loopEffect,
        levelCompletedEffect;
        
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.soundManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLevelCompleted()
    {
        effectPlayer.clip = levelCompletedEffect;
        effectPlayer.Play();
    }

    public void PlayDeath()
    {
        effectPlayer.clip = deathEffect;
        effectPlayer.Play();
        
    }

    public void LoopEffect()
    {
        effectPlayer.clip = loopEffect;
        effectPlayer.Play();
    }
}
