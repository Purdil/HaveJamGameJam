using System.Collections.Generic;
using System.Linq;
using Core.Logger;
using UnityEngine;

namespace Member.PYH._Scripts.SO
{
    [CreateAssetMenu(fileName = "ItemSOList", menuName = "SO/ITEM/ItemSO", order = 0)]
    public class ItemSOList : ScriptableObject
    {
        public ItemSO[] Items { get; private set; }
        private Dictionary<int, ItemSO> _items;

        private void OnEnable()
        {
            _items = Items.ToDictionary(c => c.index);
        }

        private void OnValidate()
        {
            if (Items != null)
            {
                for (int i = 0; i < Items.Length; i++)
                {
                    Items[i].index = i + 1;
                }
                
                Logging.Log("Index Refreshed");
            }
        }
        
        public ItemSO GetItem(int index)
        {
            ItemSO item = _items.GetValueOrDefault(index);
            
            return item;
        }
    }
}