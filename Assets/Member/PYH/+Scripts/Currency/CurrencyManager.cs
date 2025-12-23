using Core;
using Core.Logger;
using UnityEngine;
using UnityEngine.Events;

namespace Member.PYH._Scripts.Currency
{
    public enum CurrencyType
    {
        Gold,
        MagicStone
    }
    
    public class CurrencyManager : MonoSingleton<CurrencyManager>
    {
        [Header("GOLD")]
        public UnityEvent<int> onRepaymentEvent;
        public UnityEvent<int> onGoldChanged;
        private int _currentGold = 0;
        [SerializeField] private int maxGold = 1000000000;

        [Header("MAGICSTONE")]
        public UnityEvent<int> onMagicstoneChanged;
        private int currentMagicstone = 1000000000;
        [SerializeField] private int maxMagicstone = 1000000000;
        
        public int CurrentGold
        {
            get => _currentGold;
            set
            {
                if (_currentGold == value) return;
                _currentGold = value;
                onGoldChanged?.Invoke(_currentGold);
            }
        }

        public int CurrentMagicStone
        {
            get => currentMagicstone;
            set
            {
                if (currentMagicstone == value) return;
                currentMagicstone = value;
                onMagicstoneChanged?.Invoke(currentMagicstone);
            }
        }
        
        private new void Awake()
        {
            base.Awake();
            onGoldChanged?.Invoke(CurrentGold);
            onMagicstoneChanged?.Invoke(CurrentMagicStone);
        }
        
        public bool CanUseCurrency(CurrencyType type, int price)
        {
            switch (type)
            {
                case CurrencyType.Gold:
                {
                    return CurrentGold - price >= 0;
                }
                
                case CurrencyType.MagicStone:
                {
                    return CurrentMagicStone - price >= 0;
                }

                default:
                {
                    return false;
                }
            }
        }
        public void TryUseCurrency(CurrencyType type, int price)
        {
            switch (type)
            {
                case CurrencyType.Gold:
                {
                    if (CurrentGold - price < 0) { Logging.Log("FAILED TO USE CURRENCY"); return; }
                    break;
                }
                
                case CurrencyType.MagicStone:
                {
                    if (CurrentMagicStone - price < 0) { Logging.Log("FAILED TO USE CURRENCY"); return; }
                    break;
                }
            }

            UseCurrency(type, price);
        } // Only Using In Shop

        public void TryGiveCurrency(CurrencyType type, int price)
        {
            switch (type)
            {
                case CurrencyType.Gold:
                {
                    CurrentGold = Mathf.Clamp(CurrentGold + price, 0, maxGold);
                    break;
                }
                
                case CurrencyType.MagicStone:
                {
                    CurrentMagicStone = Mathf.Clamp(CurrentGold + price, 0, maxMagicstone);
                    break;
                }
            }
        }
        public void TryRepayment(int price)
        {
            if (CurrentGold - price < 0) { Logging.Log("FAILED TO REPAYMENT CURRENCY"); return; }
            
            UseCurrency(CurrencyType.Gold, price);
            onRepaymentEvent?.Invoke(price);
        } // Only Using In Debt
        
        private void UseCurrency(CurrencyType type, int price)
        {
            Logging.Log("USE CURRENCY");

            switch (type)
            {
                case CurrencyType.Gold:
                {
                    CurrentGold = Mathf.Clamp(CurrentGold - price, 0, maxGold);
                    break;
                }
                
                case CurrencyType.MagicStone:
                {
                    CurrentMagicStone = Mathf.Clamp(CurrentGold - price, 0, maxMagicstone);
                    break;
                }
            }
        }
    }
}
