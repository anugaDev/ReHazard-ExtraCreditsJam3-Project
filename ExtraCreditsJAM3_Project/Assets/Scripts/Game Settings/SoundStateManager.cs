using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStateManager : MonoBehaviour
{
    [SerializeField] private AudioSource effectPlayer;
    [SerializeField] private AudioClip deathEffect;
    [SerializeField] private AudioClip loopEffect;
    [SerializeField] private AudioClip levelCompletedEffect;
    [SerializeField] private AudioClip timePassingEffect;

    private IEnumerator secondCounter;
    
    
    

    private void Start()
    {
        GameManager.Instance.soundManager = this;
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

    public void StartSecondLoop()
    {
        secondCounter = CountSeconds();
        StartCoroutine(secondCounter);
    }

    public void StopSecondLoop()
    {
        StopCoroutine(secondCounter);
    }

    private IEnumerator CountSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            if (effectPlayer.clip != timePassingEffect)
            {
                if(effectPlayer.isPlaying) continue;

                effectPlayer.clip = timePassingEffect;
            }
            effectPlayer.Play();
        }


    }
}
