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
        public event Action<int> OnHealthChanged;
        public event Action<Agent> OnDeath;
        public int Health {get; private set;}
        
        public void Initialize(Agent owner)
        {
            _owner = owner;
            Health = maxHealth;
        }

        public int ApplyDamage(int damage)
        {
            if (damage < 0)
            {
                Logging.LogError("데미지는 음수가 될 수 없습니다.");
                return 0;
            }
                
            Health -= damage;
            int overDamage = Health;
            Health = Mathf.Clamp(Health, 0, maxHealth);
            if (Health <= 0)
            {
                OnDeath?.Invoke(_owner);
                Logging.Log($"사망했습니다. {_owner.gameObject.name}");
                PoolManager.Instance.Factory(_owner.PoolableSO).Push(_owner);
            }
            OnHealthChanged?.Invoke(Health);
            if (overDamage < 0)
            {
                return -overDamage;
            }
            return 0;
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
            OnHealthChanged?.Invoke(Health);
        }
                
    }
}