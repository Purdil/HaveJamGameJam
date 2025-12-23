using System;
using Member.PYH._Scripts.Currency;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BlackmarketUi : MonoBehaviour
{
    [SerializeField] private TMP_Text _todayText;
    private int _todayPP;
    public UnityEvent OnSellSuccessEvent;

    private void Awake()
    {
        TodayEnd();
    }

    public void TodayEnd()
    {
        _todayPP = Random.Range(1, 1500);
        _todayText.text = _todayPP.ToString("N0") + "G";
    }

    public void SellMagicstone()
    {
        int magicstone = CurrencyManager.Instance.CurrentMagicStone;
        
        if (magicstone == 0) return;
        
        CurrencyManager.Instance.TryUseCurrency(CurrencyType.MagicStone, magicstone);
        CurrencyManager.Instance.TryGiveCurrency(CurrencyType.Gold, magicstone * _todayPP);
        OnSellSuccessEvent?.Invoke();
    }
}
