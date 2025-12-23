using System.Collections;
using Core.Logger;
using DG.Tweening;
using Member.YDW.AnimationSystem;
using UnityEngine;

namespace Member.YDW.Agents.Enemys
{
    public class ForestGolemEnemy : AbstractEnemy, ICanHoldRandomEvent
    {
      [SerializeField] private AnimParamSO attackParam;
        private Vector3 beforePosition;
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
            StartCoroutine(AttackAni());
            

        }

        private IEnumerator AttackAni()
        {
            yield return new WaitForSeconds(1);
            AgentRenderer.SetParam(attackParam,true);
            AgentRenderer.OnAnimationEnd += HandleAttackAniEnd;
            AgentRenderer.OnAttackTrigger += HandleAttack;
        }

        private void HandleAttack()
        {
            AgentRenderer.OnAttackTrigger -= HandleAttack;
            attackImpulse.GenerateImpulse();
            int damage = RouletteManager.Instance.StartPlayerSpin(numberProbList, operatorProbList)();

            if (minusDamage)
            {
                damage = -damage;
                minusDamage = false;
            }
            else if (doubleDamage)
            {
                damage *= 2;
                doubleDamage = false;
            }
            else if (halfDamage)
            {
                damage = Mathf.CeilToInt(damage / 2f);
                halfDamage = false;

            }
            else if (plusDamage)
            {
                if (damage < 0)
                    damage *= -1;
                plusDamage = false;
            }
            else if(reSpine)
                damage = RouletteManager.Instance.StartPlayerSpin(numberProbList, operatorProbList)();
            Logging.Log($"{gameObject.name} damage {damage}.");
            target.ApplyDamage(damage, out _); //룰렛 연산으로 나온 데미지 부여.
        }

        private void HandleAttackAniEnd()
        {
            AgentRenderer.OnAnimationEnd -= HandleAttackAniEnd;
            AgentRenderer.SetParam(attackParam,false);
            transform.DOMove(beforePosition, 2).SetEase(Ease.OutQuart);
            StartCoroutine(TurnEnd());
        }

        private IEnumerator TurnEnd()
        {
            yield return new WaitForSeconds(1);
            Logging.Log("Attack end.");
            OnTurnEnd = true;
        }

        public bool doubleDamage { get; set; }
        public bool halfDamage { get; set; }
        public bool minusDamage { get; set; }
        public bool plusDamage { get; set; }
        public bool reSpine { get; set; }
    }
}