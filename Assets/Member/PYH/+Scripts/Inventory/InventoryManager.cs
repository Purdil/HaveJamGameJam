using System;
using System.Collections.Generic;
using Core;
using Core.SaveSystem;
using Member.PYH._Scripts.SO;
using Member.YDW.EventChannels;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.Inventory
{
    public class InventoryManager : MonoSingleton<InventoryManager>, ISaveable
    {
        [Header("Save")]
        [SerializeField] private SaveEventChannel saveEventChannel;
        [SerializeField] private SaveId saveId;
        public SaveId SaveId => saveId;

        [Header("Item List")]
        [SerializeField] private ItemSOList itemSOList;

        private List<ItemSO> _inventory = new List<ItemSO>();

        [Header("Inventory")]
        [SerializeField] private int maxSlot;

        [SerializeField] private ItemChannel itemAddChannel;
        [SerializeField] private ItemChannel itemRemoveChannel;

        private bool _suppressAutoSave;

        [Serializable]
        private struct InventorySavePayload
        {
            public List<int> itemIndexes;
        }

        private new void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            _suppressAutoSave = true;
            RequestLoad();
            _suppressAutoSave = false;
        }

        private void RequestSave()
        {
            if (_suppressAutoSave) return;
            if (saveEventChannel == null) return;

            saveEventChannel.Raise(SaveEventType.Save);
        }

        private void RequestLoad()
        {
            if (saveEventChannel == null) return;

            saveEventChannel.Raise(SaveEventType.Load);
        }

        public string GetSaveData()
        {
            var payload = new InventorySavePayload
            {
                itemIndexes = new List<int>(_inventory.Count)
            };

            foreach (var item in _inventory)
            {
                if (item == null) continue;
                payload.itemIndexes.Add(item.index);
            }

            return JsonUtility.ToJson(payload);
        }

        public void RestoreData(string loadData)
        {
            _inventory.Clear();

            if (string.IsNullOrEmpty(loadData)) return;
            if (itemSOList == null) return;

            var payload = JsonUtility.FromJson<InventorySavePayload>(loadData);
            if (payload.itemIndexes == null) return;

            foreach (int idx in payload.itemIndexes)
            {
                var item = itemSOList.GetItem(idx);
                if (item != null)
                    _inventory.Add(item);
            }
        }

        public void TryAddItem(ItemSO item)
        {
            if (item == null) return;
            if (_inventory.Count + 1 > maxSlot) return;

            _inventory.Add(item);
            RequestSave();
        }

        public void TryRemoveItem(IDestroyItem item)
        {
            item.Destroyed -= TryRemoveItem;
            if (item is ItemSO destroyItem)
                TryRemoveItem(destroyItem);
        }

        public void TryRemoveItem(ItemSO item)
        {
            if (item == null) return;

            if (_inventory.Remove(item))
                RequestSave();
        }

        public void TryRemoveItem(List<ItemSO> items)
        {
            if (items == null) return;

            bool changed = false;
            foreach (var it in items)
                changed |= _inventory.Remove(it);

            if (changed) RequestSave();
        }

        public List<ItemSO> GetAllItem() => _inventory;
        public bool HasAllItem(List<ItemSO> items)
        {
            foreach (var it in items)
                if (!_inventory.Contains(it)) return false;
            return true;
        }
        public bool HasItem(ItemSO item) => _inventory.Contains(item);
        public int GetMaxCount() => _inventory.Count;
        public bool CanGetItem() => _inventory.Count < maxSlot;

        public void RemoveRendItem()
        {
            int removedCount = 0;
            
            foreach (var itemSo in _inventory)
            {
                if (Random.Range(0, 2) == 1)
                {
                    removedCount++;
                    _inventory.Remove(_inventory[Random.Range(0, _inventory.Count + 1)]);
                }
            }
        }
    }
}
