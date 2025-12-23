using System;
using Member.YDW.AnimationSystem;
using UnityEngine;

namespace Member.YDW.AgentSystem
{
    public class AgentRenderer : MonoBehaviour, IAgentComponent , IAgentRenderer, IAnimationTrigger
    {
        private Agent _owner;
        private SpriteRenderer _renderer;
        private Animator _animator;
        
        public Sprite CurrentSprite => _renderer.sprite;
        public void Initialize(Agent owner)
        {
            _owner = owner;
            _renderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }


        
        public void SetParam(AnimParamSO param, bool value)
        {
            _animator.SetBool(param.HashValue, value);
        }

        public void SetParam(AnimParamSO param, int value)
        { 
            _animator.SetInteger(param.HashValue, value);
        }

        public void SetParam(AnimParamSO param, float value)
        {
            _animator.SetFloat(param.HashValue, value);
        }

        public void SetParam(AnimParamSO param)
        {
            _animator.SetTrigger(param.HashValue);
        }

        public event Action OnAnimationEnd;
        public event Action OnAttackTrigger;
        
        private void AnimationEnd() =>  OnAnimationEnd?.Invoke();
        private void AttackTrigger() =>  OnAttackTrigger?.Invoke();
    }
}