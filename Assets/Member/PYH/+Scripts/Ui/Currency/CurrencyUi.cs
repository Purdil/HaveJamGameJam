using TMPro;
using UnityEngine;

public class CurrencyUi : MonoBehaviour
{
    [SerializeField] private TMP_Text currencyText;
    [SerializeField] private char unit;

    public void SetText(int currency)
    {
        if (currency > 999999)
        {
            currencyText.text = $"999999{unit}+";
        }
        else
        { 
            currencyText.text = $"{currency.ToString("N0")}{unit}";
        }
    }
}
