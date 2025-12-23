using System.Collections.Generic;
using Core.Logger;
using Member.YDW.HealthSystem;
using UnityEngine;

namespace Member.YDW.AgentSystem
{
    public class AgentAttack : MonoBehaviour, IAgentComponent
    {
        private Agent _owner;



        public void Initialize(Agent owner)
        {
            _owner = owner;
        }
        public void Attack(List<IDamageable> targets, int damage)
        {
            IDamageable target = targets[0];
            target.ApplyDamage(damage, out int overDamage);
            if (overDamage > 0 && targets.Count > 1)
            {
                targets.RemoveAt(0);
                Attack(targets, overDamage);
            }
        }
    }
}