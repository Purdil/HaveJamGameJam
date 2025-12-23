using System;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Slider hpBar;

    public float maxHp = 100;
    public float currentHp = 100;

    private void Start()
    {
        hpBar.value = currentHp / maxHp;
    }
    private void Update()
    {
        HpUp();
    }

    private void HpUp()
    {
        hpBar.value = Mathf.Lerp(hpBar.value, (float)currentHp / (float)maxHp, Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHp -= 1;
        }
    }
}
