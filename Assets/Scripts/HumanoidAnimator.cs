using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAnimator : EntityAnimator
{
    public int Idle { get; private set; }
    public int Run { get; private set; }
    public int Lie { get; private set; }

    private void Start()
    {
        Idle = Animator.StringToHash("Idle");
        Run = Animator.StringToHash("Run");
        Lie = Animator.StringToHash("Lie");
    }
}
