using System;
using System.Collections;
using System.Collections.Generic;
using Core.Logger;
using Core.PoolSystem;
using Member.YDW.AgentSystem;
using Member.YDW.AnimationSystem;
using Member.YDW.EventChannels;
using Member.YDW.EventStruct;
using Member.YDW.HealthSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Member.YDW.Agents
{
    public class Player : Agent
    {
        [SerializeField] private float initEscapeChance;
        [SerializeField] private TryEscapeEvent escapeEvent;
        [SerializeField] private EscapeValueEvent  escapeValueEvent;
        [SerializeField] private PlayerInputSO input;
        [SerializeField] private PoolableSO swordPrefab;
        [SerializeField] private AnimParamSO animParam;
        [SerializeField] private Transform auraSpawnPoint;
        
        private AgentAttack _attackCompo;
        
        private readonly List<IDamageable>  _enemies = new();

        private bool _isAniEnd;
        
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
                int damage = RouletteManager.Instance.StartPlayerSpin(numberProbList, operatorProbList)();
                Logging.Log($"Player damage : {damage}");
                StartCoroutine(Attack(damage));// 플레이어는 검기를 날림. 데미지는 룰렛 산출로 넣어줌.
            }
            #endregion
        }
        

        private IEnumerator Attack(int damage)
        {
            AgentRenderer.SetParam(animParam,true);
            AgentRenderer.OnAnimationEnd += HandleAnimationEnd;
            yield return new WaitUntil( () => _isAniEnd);
            _isAniEnd = false;
            AgentRenderer.SetParam(animParam, false);
            MonoBehaviour swordAuraInstance = PoolManager.Instance.Factory(swordPrefab).Pop();
            if (swordAuraInstance is SwordAura swordAura)
            {
                swordAuraInstance.transform.position = auraSpawnPoint.position;
                swordAura.Initialize(damage);
                swordAura.SettingSO(swordPrefab);
                CombatCamera.Instance.SetFollow(swordAura.transform);
                CombatCamera.Instance.SetLensSize(4);
                CombatCamera.Instance.SetFollowOffSet(new Vector3(2,1,-10));
                OnTurnEnd = true;
            }
        }

        private void HandleAnimationEnd()
        {
            _isAniEnd = true;
            AgentRenderer.OnAnimationEnd -= HandleAnimationEnd;
        }

        public override void EndTurn()
        {
            base.EndTurn();
            _escapeChanceCount = 1;
        }


        private float _escapeChance;
        public Action<float> EscapeChanceCallback;
        private int _escapeChanceCount = 1;

        private void ModifyEscapeChance(float amount)
        {
            _escapeChance = Mathf.Clamp(_escapeChance + amount, 0f, 100f);
            EscapeChanceCallback?.Invoke(_escapeChance);
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

        public void IsTurnEnd()
        {
            OnTurnEnd = true;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            escapeValueEvent.OnEvent -= ModifyEscapeChance;
        }
    }
}