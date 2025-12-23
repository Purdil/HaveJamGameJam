using Core.Logger;
using DG.Tweening;
using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RouletteUIManager : MonoBehaviour
{
    [SerializeField] private RouletUIEvent rouletEndChannel;
    [SerializeField] private RouletUIEvent rouletApplyChannel;
    [SerializeField] private RouletUIEvent rouletResultChannel;

    [SerializeField] private ApplyInfoUI[] infoUI = new ApplyInfoUI[3];
    [SerializeField] private RouletteSlotUI[] numInfoUI = new RouletteSlotUI[3];
    [SerializeField] private RouletteSlotUI[] operInfoUI = new RouletteSlotUI[2];
    [SerializeField] private RouletteSlotUI resultUI;
    [SerializeField] private ApplyInfoUI resultInfoUI;

    [SerializeField] private float roulettTime;
    [SerializeField] private float roulettDelay;
    [SerializeField] private float applyDelay;
    [SerializeField] public float finalTime;

    private Coroutine coroutine;
    private Coroutine Roulettecoroutine;


    private bool isRoulette;


    private void Awake()
    {
        rouletEndChannel.OnEvent += OnRouletteStart;
        rouletApplyChannel.OnEvent += OnApplyItem;
        rouletResultChannel.OnEvent += OnOpertion;
    }

    private void OnOpertion(RouletData data)
    {
        StartCoroutine(enumerator(data));
    }
    private IEnumerator enumerator(RouletData data)
    {
        yield return new WaitUntil(() => isRoulette == false);
        //float result = 0;

        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(ApplyCoroutine(data));
        yield return new WaitForSeconds(applyDelay);
        for (int i = 0; i < data.applyrouletNum.Count; i++)
        {
            data.rouletNum.Apply(data.applyrouletNum[i]);
        }

        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(ApplyCoroutine(data));
        infoUI[0].TweenClear();
        infoUI[1].TweenClear();
        infoUI[2].TweenClear();

        yield return new WaitForSeconds(finalTime);

        resultInfoUI.TweenClear();
        resultUI.TweenStart(data.GetResult().ToString());

    }

    private void OnDestroy()
    {
        rouletEndChannel.OnEvent -= OnRouletteStart;
        rouletApplyChannel.OnEvent -= OnApplyItem;
        rouletResultChannel.OnEvent -= OnOpertion;
    }

    private void OnRouletteStart(RouletData rouletData)
    {
        resultUI.TweenStart("");
        isRoulette = true;
        StartCoroutine(RouletteCoroutine(rouletData, () => isRoulette = false));
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
        if (coroutine != null)
            StopCoroutine(coroutine);
           coroutine = StartCoroutine(ApplyCoroutine(rouletData));
    }
    private IEnumerator ApplyCoroutine(RouletData rouletData)
    {
        yield return new WaitUntil(() => isRoulette == false);
        //Logging.Log("적용 시작");

        numInfoUI[0].ApplyItem(rouletData.rouletNum.Num1.ToString());
        operInfoUI[0].ApplyItem(rouletData.rouletOperator.Operator1.std);
        numInfoUI[1].ApplyItem(rouletData.rouletNum.Num2.ToString());
        operInfoUI[1].ApplyItem(rouletData.rouletOperator.Operator2.std);
        numInfoUI[2].ApplyItem(rouletData.rouletNum.Num3.ToString());

        if (rouletData.applyrouletNum != null)
        {
            ApplyRouletNum[] a1;
            List<ApplyStruct> a2 = new List<ApplyStruct>();

            a1 = rouletData.applyrouletNum.FindAll(x => x.RouletNum.Num1.HasValue == true && x.ApplyOperator != null).ToArray();
            if (a1.Length != 0)
            {
                a2.Clear();
                foreach (var item in a1) a2.Add(new ApplyStruct(item.RouletNum.Num1.GetValueOrDefault(), item.ApplyOperator));
                infoUI[0].TweenStart(a2.ToArray());
            }

            a1 = rouletData.applyrouletNum.FindAll(x => x.RouletNum.Num2.HasValue == true && x.ApplyOperator != null).ToArray();
            if (a1.Length != 0)
            {
                a2.Clear();
                foreach (var item in a1) a2.Add(new ApplyStruct(item.RouletNum.Num2.GetValueOrDefault(), item.ApplyOperator));
                infoUI[1].TweenStart(a2.ToArray());
            }

            a1 = rouletData.applyrouletNum.FindAll(x => x.RouletNum.Num3.HasValue == true && x.ApplyOperator != null).ToArray();
            if (a1.Length != 0)
            {
                a2.Clear();
                foreach (var item in a1) a2.Add(new ApplyStruct(item.RouletNum.Num3.GetValueOrDefault(), item.ApplyOperator));
                infoUI[2].TweenStart(a2.ToArray());
            }
            //yield return new WaitForSeconds(applyDelay);

        }

        if (rouletData.applyFinal != null)
        {
            List<ApplyStruct> a2 = new List<ApplyStruct>();
            foreach (var item in rouletData.applyFinal)
            {
                a2.Add(new ApplyStruct(item.final, item.ApplyOperator));
            }
            resultInfoUI.TweenStart(a2.ToArray());
        }
    }
}

public struct ApplyStruct
{
    IOperator<float> Operator;
    float value;
    public readonly string str;
    public ApplyStruct(float value, IOperator<float> operatorVlaue)
    {
        this.value = value;
        this.Operator = operatorVlaue;
        this.str = operatorVlaue.std + value.ToString();
    }
    public float Operation(float p1) => Operator.Operation(p1, value);
}