using Member.YDW.AgentSystem;
using UnityEngine;

namespace Member.YDW.Agents
{
    public abstract class AbstractEnemy : Agent
    {
        [SerializeField] protected float moveDamping = 1;
        protected Player target;

        public void InitTarget(Player target)
        {
            this.target ??= target;
        }

        public abstract void Attack();

    }
}