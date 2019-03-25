using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnim : StateMachineBehaviour
{
    private void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Destroy(animator.gameObject);
    }

}
