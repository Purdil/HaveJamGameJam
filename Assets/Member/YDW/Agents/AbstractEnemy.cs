using Member.YDW.AgentSystem;
using Unity.Cinemachine;
using UnityEngine;

namespace Member.YDW.Agents
{
    public abstract class AbstractEnemy : Agent
    {
        [SerializeField] protected CinemachineImpulseSource attackImpulse;
        [SerializeField] protected float moveDamping = 1;
        protected Player target;

        protected override void Awake()
        {
            base.Awake();
            if(MoneyStateManager.Instance.state != 0)
                Health.SetMaxHealth(Health.GetMaxHealth() * MoneyStateManager.Instance.state + MoneyStateManager.Instance.state / 10);
        }

        public void InitTarget(Player target)
        {
            this.target ??= target;
        }

        public override void StartTurn()
        {
            base.StartTurn();
            CombatCamera.Instance.SetLensSize(3);
            CombatCamera.Instance.SetFollow(transform);
            CombatCamera.Instance.SetFollowOffSet(new Vector3(-0.5f,1,-10));
        }

        public override void EndTurn()
        {
            base.EndTurn();
            CombatCamera.Instance.SetDefaultLensSize();
            CombatCamera.Instance.SetFollowOffSet();
            CombatCamera.Instance.MoveCenterPos();
        }

        public virtual void Attack()
        {
            
        }

    }
}