using System;
using System.Collections.Generic;
using System.Linq;
using Core.Logger;
using UnityEngine;

namespace Core.PoolSystem
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        [SerializeField] private List<PoolableListSO> poolableLists;
        
        private Dictionary<PoolableSO,Factory> _factoryDictionary = new();

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            InitDictionary();
        }
        private void InitDictionary()
        {
            List<PoolableSO> poolableSO = new List<PoolableSO>();
            foreach(PoolableListSO poolableListSO in poolableLists)
                poolableSO.AddRange(poolableListSO.PoolableList);
            GameObject factoryOb = new  GameObject("Factory");
            factoryOb.transform.SetParent(transform);
            _factoryDictionary = poolableSO.ToDictionary(poolable => poolable, poolable => new Factory(poolable.Prefab,8,factoryOb.transform));
        }

        public Factory Factory(PoolableSO poolableSO)
        {
            if (_factoryDictionary.TryGetValue(poolableSO, out var factory))
            {
                return factory;
            }
            
            Logging.LogError("해당 팩토리가 존재하지 않습니다.SO의 리스트를 확인하세요.");
            return null;
        }
    }
}
