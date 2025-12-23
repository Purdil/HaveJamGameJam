using System;
using UnityEngine;

public class NPCAnimator : MonoBehaviour
{
    private readonly int animatorSellHash = Animator.StringToHash("SELL");
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnSellEvent()
    {
        _animator.SetTrigger(animatorSellHash);
    }
}
