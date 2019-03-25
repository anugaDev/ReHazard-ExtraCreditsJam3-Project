using System.Collections;
using System.Collections.Generic;
using Kino;
using UnityEngine;

public class CameraActions : MonoBehaviour
{
    [SerializeField] private AnalogGlitch cameraGlitch;
    [SerializeField] private AnimationClip loopTransitionAnimation;
    [SerializeField] private AnimationClip levelEndAnimation;
    [SerializeField] private Animation cameraAnimation;

    [HideInInspector] public bool playing;

    private void Start()
    {
        GameManager.Instance.levelCamera = this;
    }

    public void GlitchCameraLoop()
    {
        cameraAnimation.clip = loopTransitionAnimation;
        cameraAnimation.Play();
    }

    public void EndLevelAnimationCamera()
    {
        cameraAnimation.clip = levelEndAnimation;

        cameraAnimation.Play();
    }
}
