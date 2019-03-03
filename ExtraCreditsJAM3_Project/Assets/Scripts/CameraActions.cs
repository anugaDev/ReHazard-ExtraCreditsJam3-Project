using System.Collections;
using System.Collections.Generic;
using Kino;
using UnityEngine;

public class CameraActions : MonoBehaviour
{
    
    [SerializeField] private AnalogGlitch cameraGlitch;

    [SerializeField] private AnimationClip loopTransitionAnimation;
    [SerializeField] private Animation cameraAnimation;

    [HideInInspector] public bool playing;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.levelCamera = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GlitchCameraLoop()
    {
        cameraAnimation.clip = loopTransitionAnimation;

        cameraAnimation.Play();

//        playing = true;
//
//        while (cameraAnimation.isPlaying)
//        {
//            yield return null;
//        }
//
//        playing = false;

    }
}
