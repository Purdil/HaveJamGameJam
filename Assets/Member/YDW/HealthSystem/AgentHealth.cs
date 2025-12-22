using System;
using Core.Logger;
using Core.PoolSystem;
using Member.YDW.AgentSystem;
using UnityEngine;

namespace Member.YDW.HealthSystem
{
    public class AgentHealth : MonoBehaviour , IAgentComponent , IDamageable, IHealable
    {
        [SerializeField] private int maxHealth;

        private Agent _owner;
        public event Action<int> OnDamaged;
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
                Logging.LogError("데미지는 음수가 될 수 없습니다.");
                overDamage = 0;
                return;
            }
                
            Health -= damage;
            int over = Health;
            Health = Mathf.Clamp(Health, 0, maxHealth);
            if (Health <= 0)
            {
                OnDeath?.Invoke(_owner);
                Logging.Log($"사망했습니다. {_owner.GetInstanceID()}");
                PoolManager.Instance.Factory(_owner.PoolableSO).Push(_owner);
            }
            OnDamaged?.Invoke(Health);
            if (over < 0)
            {
                overDamage = -over;
                return;
            }

            overDamage = 0;
        }

        public void ApplyHeal(int heal)
        {
            if (heal < 0)
            {
                Logging.LogError("힐은 음수가 될 수 없습니다.");
                return;
            }
            Health += heal;
            Health = Mathf.Clamp(Health, 0, maxHealth);
            OnDamaged?.Invoke(Health);
        }
                
    }
}