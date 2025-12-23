using System;
using System.Collections.Generic;
using System.Linq;
using Core.PoolSystem;
using Member.YDW.HealthSystem;
using UnityEngine;

namespace Member.YDW.AgentSystem
{
    public enum AgentType
    {
        Player,
        Enemy,
        Empty
    }
    public abstract class Agent : MonoBehaviour , IDamageable , IPoolable, ITurnAgent
    {
        private Dictionary<Type,IAgentComponent> _components;

        public bool IsActivated {get; private set;}
        public AgentHealth Health { get; private set; }
        public AgentRenderer AgentRenderer {get; private set;}
        protected virtual void Awake()
        {
            _components = GetComponentsInChildren<IAgentComponent>().ToDictionary(compo => compo.GetType());
            InitComponent();
        }

        private void InitComponent()
        {
            foreach (var component in _components.Values)
            {
                component.Initialize(this);
            }
            Health = GetComponentInChildren<AgentHealth>();
            Health.OnDeath += HandleDead;
            AgentRenderer = GetCompo<AgentRenderer>();
        }

        private void HandleDead(Agent agent)
        {
            OnDead = true;
        }


        public T GetCompo<T>() 
        {
            if (_components.TryGetValue(typeof(T), out IAgentComponent component) && component is T compo)
            {
                return compo;
            }
            IAgentComponent findComponent = _components.Values.FirstOrDefault(c => c is T);
            if(findComponent != null && findComponent is T findCompo)
                return findCompo;

            return default(T);
        }


        public void ApplyDamage(int damage , out int overDamage)
        {
            Health.ApplyDamage(damage, out int damageOver);
            
            overDamage = damageOver;
            return;
        }


        public void SettingSO(PoolableSO poolableSO)
        {
            PoolableSO = poolableSO;
        }

        public void OnPopObject()
        {
            Health.Initialize(this);
            OnDead = false;
            IsActivated = true;
        }
        public void OnPushObject()
        {
            OnTurnEnd = false;
            IsActivated = false;
        }

        public PoolableSO PoolableSO { get; private set; }

        public Agent User => this;
        public bool OnTurnEnd { get; protected set; }
        public bool OnDead { get; private set; }
        
        public virtual void StartTurn()
        {
        }

        public virtual void Turning()
        {
            
        }

        public virtual void EndTurn()
        {
            OnTurnEnd = false;
        }
        protected virtual void OnDestroy()
        {
            Health.OnDeath -= HandleDead;
        }
    }
}