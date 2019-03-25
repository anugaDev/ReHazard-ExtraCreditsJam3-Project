using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterAnim : StateMachineBehaviour
{
    private void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.gameObject.SetActive(false);
    }

   
}
