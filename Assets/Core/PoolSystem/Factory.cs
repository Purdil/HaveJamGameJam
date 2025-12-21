using System.Collections.Generic;
using Core.Logger;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.PoolSystem
{
    public class Factory
    {
        private readonly MonoBehaviour _prefab;
        private readonly Transform _parent;
        private Stack<MonoBehaviour> _pool;
        
        public Factory(MonoBehaviour prefab,int initCapacity , Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
            _pool = new Stack<MonoBehaviour>(initCapacity);
        }

        public MonoBehaviour Pop(Transform parent = null)
        {
            MonoBehaviour obj = null;
            if (_pool.Count > 0)
            {
                obj = _pool.Pop();
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj = Object.Instantiate(_prefab);
            }
            if(obj is IPoolable poolable)
                poolable.OnPopObject();
            SetParentObject(parent, obj);

            return obj;
        }

        public void Push(MonoBehaviour obj)
        {
            if (obj == null)
            {
                Logging.Log("obj is null");
            }
            if(obj is IPoolable poolable)
                poolable.OnPushObject();
            _pool.Push(obj);
            SetParentObject(_parent, obj);
            obj.gameObject.SetActive(false);
            
        }

        private static void SetParentObject(Transform parent, MonoBehaviour obj)
        {
            obj.transform.SetParent(parent);
            if (!parent)
                SceneManager.MoveGameObjectToScene(obj.gameObject, SceneManager.GetActiveScene());
        }
    }
}