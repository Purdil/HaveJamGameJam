using System.Collections.Generic;
using Core.Logger;
using Member.YDW.AgentSystem;
using Member.YDW.EventChannels;
using Member.YDW.EventStruct;
using Member.YDW.HealthSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Member.YDW.Agents
{
    public class Player : Agent
    {
        [SerializeField] private float initEscapeChance;
        [SerializeField] private TryEscapeEvent escapeEvent;
        [SerializeField] private EscapeValueEvent  escapeValueEvent;
        [SerializeField] private PlayerInputSO input;
        private AgentAttack _attackCompo;

        private List<IDamageable>  _enemies = new();

        protected override void Awake()
        {
            base.Awake();
            _attackCompo = GetCompo<AgentAttack>();
            ModifyEscapeChance(initEscapeChance);
            escapeValueEvent.OnEvent += ModifyEscapeChance;
        }

        public void InitTargets(List<AbstractEnemy> targets)
        {
            _enemies.Clear();
            _enemies.AddRange(targets);
            Logging.Log($"Init Target Count : {_enemies.Count}");
        }
        private void Update()
        
        {
            //룰렛 연산이 끝난 후, 아무곳이나 클릭하면 공격함.
            #region Test

            if (Keyboard.current.nKey.wasPressedThisFrame)
            {
                OnTurnEnd = true;
            }

            if (Keyboard.current.pKey.wasPressedThisFrame)
            {
                ApplyDamage(1000, out _);
            }

           
            #endregion
        }

        public override void Turning()
        {
            base.Turning();

            #region TestCode
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                TryEscape();
            }
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Attack(100);// 플레이어는 검기를 날림. 데미지는 룰렛 산출로 넣어줌.
                OnTurnEnd = true;
            }
            #endregion
        }

        private void Attack(int damage)
        {
            
        }

        public override void EndTurn()
        {
            base.EndTurn();
            _escapeChanceCount = 1;
        }


        private float _escapeChance;

        private int _escapeChanceCount = 1;

        private void ModifyEscapeChance(float amount)
        {
            _escapeChance = Mathf.Clamp(_escapeChance + amount, 0f, 100f);
        }

        private void TryEscape()
        {
            if (_escapeChanceCount == 0)
            {
                Logging.Log("기회를 모두 소진했습니다.");
                return;
            }
            _escapeChanceCount--;
            float roll = Random.Range(0f, 100f);
            escapeEvent.Raise(roll < _escapeChance);
            OnTurnEnd = true;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            escapeValueEvent.OnEvent -= ModifyEscapeChance;
        }
    }
}