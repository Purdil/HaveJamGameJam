using System;
using Core.Logger;
using Core.PoolSystem;
using Member.YDW.AgentSystem;
using Member.YDW.AnimationSystem;
using UnityEngine;

namespace Member.YDW.HealthSystem
{
    public class AgentHealth : MonoBehaviour , IAgentComponent , IDamageable, IHealable
    {
        [SerializeField] private PoolableSO healEffect;
        [SerializeField] private AnimParamSO deathParam;
        [SerializeField] private AnimParamSO hurtParam;
        [SerializeField] private int maxHealth;

        private Agent _owner;
        
        public event Action<int> OnDamaged;
        public event Action<int> OnHealed; 
        public event Action<Agent> OnDeath;
        public int Health {get; private set;}
        
        public void Initialize(Agent owner)
        {
            _owner = owner;
            Health = maxHealth;
        }

        public void ApplyDamage(int damage, out int overDamage)
        {
            if (damage < 0)
            {
                ApplyHeal(-damage);
                Logging.Log($"Apply Heal {-damage}.");
                overDamage = 0;
                return;
            }
                
            Health -= damage;
            int over = Health;
            Health = Mathf.Clamp(Health, 0, maxHealth);
            if (Health <= 0)
            {
                _owner.AgentRenderer.SetParam(deathParam, true);
                _owner.AgentRenderer.OnAnimationEnd += HandleDeath;
                OnDeath?.Invoke(_owner);
                Logging.Log($"사망했습니다. {_owner.GetInstanceID()}");
            }
            else
            {
                _owner.AgentRenderer.SetParam(hurtParam,true); 
                _owner.AgentRenderer.OnAnimationEnd += HandleHitEnd;
            }
                
            OnDamaged?.Invoke(Health);
            if (over < 0)
            {
                overDamage = -over;
                return;
            }


            overDamage = 0;
        }

        private void HandleDeath()
        {
            _owner.AgentRenderer.OnAnimationEnd -= HandleDeath;
            PoolManager.Instance.Factory(_owner.PoolableSO).Push(_owner);
            _owner.AgentRenderer.SetParam(deathParam, false);
        }

        private void HandleHitEnd()
        {
            _owner.AgentRenderer.SetParam(hurtParam,false);
            _owner.AgentRenderer.OnAnimationEnd -= HandleHitEnd;
        }

        public void ApplyHeal(int heal)
        {
            if (heal < 0)
            {
                Logging.LogError("힐은 음수가 될 수 없습니다.");
                return;
            }
            PoolManager.Instance.Factory(healEffect).Pop().transform.position = transform.position;
            Health += heal;
            Health = Mathf.Clamp(Health, 0, maxHealth);
            OnHealed?.Invoke(Health);
        }
                
    }
}