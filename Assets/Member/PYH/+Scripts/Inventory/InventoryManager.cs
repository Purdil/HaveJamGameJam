using System.Collections.Generic;
using System.Linq;
using BBJ;
using Core;
using Core.Logger;
using Member.PYH._Scripts.SO;
using UnityEngine;

namespace Member.PYH._Scripts.Inventory
{
    public class InventoryManager : MonoSingleton<InventoryManager>
    {
        private Dictionary<int, ItemSO> _inventory = new Dictionary<int, ItemSO>();
        private List<ItemSO> _forInspector;
        [SerializeField] private int maxSlot;
        [SerializeField] private ItemChannel itemAddChannel;
        [SerializeField] private ItemChannel itemRemoveChannel;

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
            _inventory.Clear();

            for (int i = 0; i < maxSlot; i++)
            {
                var item = _forInspector[i];
                if (item != null)
                    _inventory[i] = item;
            }
        }

        public void TryAddItem(ItemSO item)
        {
            if (item == null) return;
            if (maxSlot <= 0) return;

            EnsureInspectorSize();

            if (_inventory.Count >= maxSlot) return;

            int empty = -1;

            for (int i = 0; i < maxSlot; i++)
            {
                if (_inventory.ContainsKey(i) == false)
                {
                    empty = i;
                    break;
                }
            }

            if (empty == -1) return;

            if (item is IItem apply)
                apply.Acquire();
            if (item is IDestroyItem destroy)
                destroy.Destroyed += TryRemoveItem;

            _inventory[empty] = item;
            _forInspector[empty] = item;
            itemAddChannel.Raise(item);
        }
        public void TryRemoveItem(IDestroyItem item)
        {
            item.Destroyed -= TryRemoveItem;
            ItemSO destoryItem = item as ItemSO;
            TryRemoveItem(destoryItem.index);
        }
        public void TryRemoveItem(int index)
        {
            if (maxSlot <= 0) return;
            if (index < 0 || index >= maxSlot) return;

            EnsureInspectorSize();

            if (_inventory.Count == 0)
            {
                Logging.LogError("Can't Remove Inventory");
                return;
            }

            if (_inventory.ContainsKey(index) == false) return;

            var item = _inventory[index];
            if (item is IItem apply)
                apply.UnAcquire();

            _inventory.Remove(index);
            _forInspector[index] = null; // 인스펙터 표시도 동기화
            itemRemoveChannel.Raise(_inventory[index]);
        }
    }
}
