using System.Collections.Generic;
using Core;
using Core.Logger;
using Member.PYH._Scripts.SO;
using UnityEngine;

namespace Member.PYH._Scripts.Inventory
{
    public class InventoryManager : MonoSingleton<InventoryManager>
    {
        private Dictionary<int, ItemSO> _inventory = new Dictionary<int, ItemSO>();
        [SerializeField] private int maxSlot;

        public void TryAddItem(ItemSO item)
        {
            if (_inventory.Count + 1 > maxSlot) return;

            int empty = 0;
            
            for (int i = 0; i < _inventory.Count; i++)
            {
                if (_inventory.ContainsKey(i) == false)
                {
                    empty = i;
                    break;
                }
            }
            _inventory.TryAdd(empty, item);
        }

        public void TryRemoveItem(int index)
        {
            if (_inventory.Count == 0) { Logging.LogError("Can't Remove Inventroy"); return; }
            if (_inventory.ContainsKey(index) == false) { return; }
            
            _inventory.Remove(index);
        }
    }
}
