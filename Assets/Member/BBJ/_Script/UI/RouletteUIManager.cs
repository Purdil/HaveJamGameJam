using Core.Logger;
using System;
using System.Collections;
using UnityEngine;

public class RouletteUIManager : MonoBehaviour
{
    [SerializeField] private RouletUIEvent rouletEndChannel;
    [SerializeField] private RouletUIEvent rouletApplyChannel;
    //[SerializeField] private RouletEvent rouletFinalChannel;

    [SerializeField] private ApplyInfoUI[] infoUI = new ApplyInfoUI[3];
    [SerializeField] private RouletteSlotUI[] numInfoUI = new RouletteSlotUI[3];
    [SerializeField] private RouletteSlotUI[] operInfoUI = new RouletteSlotUI[2];

    [SerializeField] private float roulettTime;
    [SerializeField] private float roulettDelay;
    [SerializeField]private float applyDelay;

    private bool isRoulette;

    private void Awake()
    {
        rouletEndChannel.OnEvent += OnRouletteStart;
        rouletApplyChannel.OnEvent += OnApplyItem;
    }
    private void OnDestroy()
    {
        rouletEndChannel.OnEvent -= OnRouletteStart;
        rouletApplyChannel.OnEvent -= OnApplyItem;
    }

    private void OnRouletteStart(RouletData rouletData)
    {
        isRoulette = true;
        StartCoroutine(RouletteCoroutine(rouletData, ()=> isRoulette = false));
    }

    private IEnumerator RouletteCoroutine(RouletData rouletData, Action callback = default)
    {
        numInfoUI[0].StartRoulette(roulettTime, rouletData.rouletNum.Num1.ToString());
        yield return new WaitForSeconds(roulettDelay);

        operInfoUI[0].StartRoulette(roulettTime, rouletData.rouletOperator.Operator1.std);
        yield return new WaitForSeconds(roulettDelay);

        numInfoUI[1].StartRoulette(roulettTime, rouletData.rouletNum.Num2.ToString());
        yield return new WaitForSeconds(roulettDelay);

        operInfoUI[1].StartRoulette(roulettTime, rouletData.rouletOperator.Operator2.std);
        yield return new WaitForSeconds(roulettDelay);

        numInfoUI[2].StartRoulette(roulettTime, rouletData.rouletNum.Num3.ToString());
        yield return new WaitForSeconds(roulettTime);
        callback?.Invoke();
    }
    public void OnApplyItem(RouletData rouletData)
    {
        StartCoroutine(ApplyCoroutine(rouletData));
    }
    private IEnumerator ApplyCoroutine(RouletData rouletData)
    {
        yield return new WaitUntil(() => isRoulette == false);

        yield return new WaitForSeconds(applyDelay);
        Logging.Log("적용 시작");

        numInfoUI[0].ApplyItem(rouletData.rouletNum.Num1.ToString());
        operInfoUI[0].ApplyItem(rouletData.rouletOperator.Operator1.std);
        numInfoUI[1].ApplyItem(rouletData.rouletNum.Num2.ToString());
        operInfoUI[1].ApplyItem(rouletData.rouletOperator.Operator2.std);
        numInfoUI[2].ApplyItem(rouletData.rouletNum.Num3.ToString());
        yield return new WaitForSeconds(applyDelay);

        if (rouletData.applyrouletNum != null)
            foreach (var item in rouletData.applyrouletNum)
            {
                if (item.RouletNum.Num1.HasValue == true)
                    infoUI[0].TweenStart(item.ApplyOperator.std + item.RouletNum.Num1);

                if (item.RouletNum.Num2.HasValue == true)
                    infoUI[1].TweenStart(item.ApplyOperator.std + item.RouletNum.Num2);

                if (item.RouletNum.Num3.HasValue == true)
                    infoUI[2].TweenStart(item.ApplyOperator.std + item.RouletNum.Num3);

                yield return new WaitForSeconds(applyDelay);
            }

        if (rouletData.applyFinal != null)
            foreach (var item in rouletData.applyFinal)
            {
                infoUI[0].TweenStart(item.ApplyOperator.std + item.final);
                yield return new WaitForSeconds(applyDelay);
            }
    }
}
