using System.Collections.Generic;
using Core;
using Member.PYH._Scripts.SO;
using UnityEngine;

namespace Member.PYH._Scripts.Inventory
{
    public class InventoryManager : MonoSingleton<InventoryManager>
    {
        private List<ItemSO> _inventory = new List<ItemSO>();
        [SerializeField] private int maxSlot;
        [SerializeField] private ItemChannel itemAddChannel;
        [SerializeField] private ItemChannel itemRemoveChannel;

        private new void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        public void TryAddItem(ItemSO item)
        {
            if (_inventory.Count + 1 > maxSlot) return;
            
            _inventory.Add(item);
        }
        public void TryRemoveItem(IDestroyItem item)
        {
            item.Destroyed -= TryRemoveItem;
            ItemSO destoryItem = item as ItemSO;
            TryRemoveItem(destoryItem);
        }
        public void TryRemoveItem(ItemSO item)
        {
            _inventory.Remove(item);
        }
        public void TryRemoveItem(List<ItemSO> items)
        {
            foreach (var var in items)
            {
                _inventory.Remove(var);
            }
        }
        public List<ItemSO> GetAllItem()
        {
            return _inventory;
        }
        public bool HasAllItem(List<ItemSO> items)
        {
            foreach (var var in items)
            {
                if (!_inventory.Contains(var))
                {
                    return false; 
                }
            }
            
            return true;
        }
        public bool HasItem(ItemSO items)
        {
            return _inventory.Contains(items);
        }
        public int GetMaxCount()
        {
            return _inventory.Count;
        }

        public bool CanGetItem()
        {
            return _inventory.Count < maxSlot;
        }
    }
}
