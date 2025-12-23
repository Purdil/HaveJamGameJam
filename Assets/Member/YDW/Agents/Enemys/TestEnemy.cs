using Core.Logger;
using DG.Tweening;
using Member.YDW.AnimationSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Member.YDW.Agents.Enemys
{
    public class TestEnemy : AbstractEnemy
    {
        [SerializeField] private AnimParamSO attackParam;
        private Vector3 beforePosition;
        private void Update()
        {
            #region Test

            if (Keyboard.current.kKey.wasPressedThisFrame)
            {
                OnTurnEnd = true;
            }

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                ApplyDamage(1000);
            }
            

            #endregion
        }

        public override void StartTurn()
        {
            base.StartTurn();
            Attack();
        }

        public override void Attack()
        {
            if (target == null)
            {
                Logging.Log("타켓이 null입니다.");
                return;
            }

            beforePosition = transform.position;
            transform.DOMove(target.transform.position + new Vector3(moveDamping,0,0), 2).SetEase(Ease.OutQuart);
            AgentRenderer.SetParam(attackParam,true);
            AgentRenderer.OnAnimationEnd += HandleAttackAniEnd;

        }

        private void HandleAttackAniEnd()
        {
            AgentRenderer.OnAnimationEnd -= HandleAttackAniEnd;
            AgentRenderer.SetParam(attackParam,false);
            target.ApplyDamage(10, out _); //룰렛 연산으로 나온 데미지 부여.
            transform.DOMove(beforePosition, 2).SetEase(Ease.OutQuart);
            OnTurnEnd = true;
            
        }
    }
}