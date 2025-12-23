using System;
using Core;
using Core.Logger;
using Core.SaveSystem;
using Member.YDW.EventChannels;
using UnityEngine;
using UnityEngine.Events;

namespace Member.PYH._Scripts.Currency
{
    public enum CurrencyType
    {
        GOLD,
        MAGICSTONE
    }

    public class CurrencyManager : MonoSingleton<CurrencyManager>, ISaveable
    {
        [Header("Save")]
        [SerializeField] private SaveEventChannel saveEventChannel;
        [SerializeField] private SaveId saveId;
        public SaveId SaveId => saveId;

        [Header("Events")]
        public UnityEvent<int> onRepaymentEvent;
        public UnityEvent<int> onGoldChanged;
        public UnityEvent<int> onMagicstoneChanged;

        private int _maxGold = 1000000;
        [SerializeField] private int _currentGold = 5000;

        private int _maxMagicstone = 1000000;
        [SerializeField] private int _currentMagicstone = 5000;

        private bool _suppressAutoSave;

        [Serializable]
        private struct CurrencySavePayload
        {
            public int gold;
            public int magicstone;
        }

        public int CurrentGold
        {
            get => _currentGold;
            set
            {
                if (_currentGold == value) return;
                _currentGold = value;
                onGoldChanged?.Invoke(_currentGold);
                RequestSave();
            }
        }

        public int CurrentMagicstone
        {
            get => _currentMagicstone;
            set
            {
                if (_currentMagicstone == value) return;
                _currentMagicstone = value;
                onMagicstoneChanged?.Invoke(_currentMagicstone);
                RequestSave();
            }
        }

        private new void Awake()
        {
            base.Awake();

            _suppressAutoSave = true;

            RequestLoad();

            _suppressAutoSave = false;

            onGoldChanged?.Invoke(_currentGold);
            onMagicstoneChanged?.Invoke(_currentMagicstone);
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
            var payload = new CurrencySavePayload
            {
                gold = _currentGold,
                magicstone = _currentMagicstone
            };
            return JsonUtility.ToJson(payload);
        }
        public void RestoreData(string loadData)
        {
            if (string.IsNullOrEmpty(loadData)) return;

            var payload = JsonUtility.FromJson<CurrencySavePayload>(loadData);
            _currentGold = Mathf.Clamp(payload.gold, 0, _maxGold);
            _currentMagicstone = Mathf.Clamp(payload.magicstone, 0, _maxMagicstone);
        }

        public bool CanUseCurrency(CurrencyType type, int price)
        {
            return type switch
            {
                CurrencyType.GOLD => CurrentGold - price >= 0,
                CurrencyType.MAGICSTONE => CurrentMagicstone - price >= 0,
                _ => false
            };
        }
        public void TryUseCurrency(CurrencyType type, int price)
        {
            switch (type)
            {
                case CurrencyType.GOLD:
                    if (CurrentGold - price < 0) { Logging.Log("FAILED TO USE GOLD"); return; }
                    break;
                case CurrencyType.MAGICSTONE:
                    if (CurrentMagicstone - price < 0) { Logging.Log("FAILED TO USE MAGICSTONE"); return; }
                    break;
            }

            UseCurrency(type, price);
        }

        public void TryGiveCurrency(CurrencyType type, int amount)
        {
            switch (type)
            {
                case CurrencyType.GOLD:
                    CurrentGold = Mathf.Clamp(CurrentGold + amount, 0, _maxGold);
                    break;
                case CurrencyType.MAGICSTONE:
                    CurrentMagicstone = Mathf.Clamp(CurrentMagicstone + amount, 0, _maxMagicstone);
                    break;
            }
        }
        public void TryRepayment(int price)
        {
            if (CurrentGold - price < 0) { Logging.Log("FAILED TO REPAYMENT CURRENCY"); return; }

            UseCurrency(CurrencyType.GOLD, price);
            onRepaymentEvent?.Invoke(price);
        }

        private void UseCurrency(CurrencyType type, int price)
        {
            switch (type)
            {
                case CurrencyType.GOLD:
                    CurrentGold = Mathf.Clamp(CurrentGold - price, 0, _maxGold);
                    break;
                case CurrencyType.MAGICSTONE:
                    CurrentMagicstone = Mathf.Clamp(CurrentMagicstone - price, 0, _maxMagicstone);
                    break;
            }
            Logging.Log("USE CURRENCY");
        }
    }
}
