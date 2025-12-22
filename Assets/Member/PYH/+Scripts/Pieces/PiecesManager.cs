using System.Collections.Generic;
using Core;
using Core.Logger;
using Member.PYH._Scripts.SO;
using UnityEngine;

namespace Member.PYH._Scripts.Pieces
{
    public class PiecesManager : MonoSingleton<PiecesManager>
    { 
        private Dictionary<int, ItemSO> _pieces = new Dictionary<int, ItemSO>();
        private List<ItemSO> _forInspector;
        [SerializeField] private int maxSlot;

        private new void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            
            EnsureInspectorSize();
            RebuildDictionaryFromInspector();
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            EnsureInspectorSize();
        }
#endif

        private void EnsureInspectorSize()
        {
            if (maxSlot < 0) maxSlot = 0;

            if (_forInspector == null)
                _forInspector = new List<ItemSO>(maxSlot);

            if (_forInspector.Count < maxSlot)
            {
                int addCount = maxSlot - _forInspector.Count;
                for (int i = 0; i < addCount; i++)
                    _forInspector.Add(null);
            }
            else if (_forInspector.Count > maxSlot)
            {
                _forInspector.RemoveRange(maxSlot, _forInspector.Count - maxSlot);
            }
        }
        private void RebuildDictionaryFromInspector()
        {
            _pieces.Clear();

            for (int i = 0; i < maxSlot; i++)
            {
                var item = _forInspector[i];
                if (item != null)
                    _pieces[i] = item;
            }
        }

        public void TryAddItem(ItemSO item)
        {
            if (item == null) return;
            if (maxSlot <= 0) return;

            EnsureInspectorSize();

            if (_pieces.Count >= maxSlot) return;

            int empty = -1;

            for (int i = 0; i < maxSlot; i++)
            {
                if (_pieces.ContainsKey(i) == false)
                {
                    empty = i;
                    break;
                }
            }

            if (empty == -1) return;

            _pieces[empty] = item;
            _forInspector[empty] = item;
        }
        public void TryRemoveItem(int index)
        {
            if (maxSlot <= 0) return;
            if (index < 0 || index >= maxSlot) return;

            EnsureInspectorSize();

            if (_pieces.Count == 0)
            {
                Logging.LogError("Can't Remove Inventory");
                return;
            }

            if (_pieces.ContainsKey(index) == false) return;

            _pieces.Remove(index);
            _forInspector[index] = null; // 인스펙터 표시도 동기화
        }
    }
}
