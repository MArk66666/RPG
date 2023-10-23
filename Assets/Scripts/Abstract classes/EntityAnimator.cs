using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class EntityAnimator : MonoBehaviour
{
    protected int currentState = -1;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public virtual void PlayAnimation(int state)
    {
        if (currentState == state) return;
        currentState = state;
        _animator.CrossFade(state, 0.3f, 0);
    }

    public int GetCurrentState()
    {
        return currentState;
    }
}
