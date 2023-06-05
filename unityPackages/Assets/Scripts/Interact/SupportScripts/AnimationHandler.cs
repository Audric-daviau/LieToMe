using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator animator;

    public void ToggleBool(string boolName)
    {
        animator.SetBool(boolName, !animator.GetBool(boolName));
    }
}
