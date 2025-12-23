using Core;
using Core.Logger;
using Member.PYH._Scripts.SO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RouletteManager : MonoSingleton<RouletteManager>
{
    [SerializeField] private RouletEvent rouletStartChannel;
    [SerializeField] private RouletEvent rouletEndChannel;

    [SerializeField] private RouletUIEvent rouletUIChannel;
    [SerializeField] private RouletUIEvent rouletApplyUIChannel;
    [SerializeField] private RouletUIEvent rouletResultChannel;

    [SerializeField] private ItemListEvent itemSubChannel;
    [SerializeField] private ItemListEvent itemUnsubChannel;
    //[SerializeField] private NumberProbListSO numProb;
    //[SerializeField] private OperatorProbListSO operProb;

    public RouletData rouletData { get; private set; }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            //StartPlayerSpin();
        }
    }
#endif

    public Func<int> StartPlayerSpin(NumberProbListSO numProb, OperatorProbListSO operProb, List<ItemSO> list = default)
    {
        itemSubChannel.Raise(list);

        rouletData = new RouletData(new RouletNum(null), new RouletOperator(null));
        rouletStartChannel.Raise((data) => { rouletData.Setter(data); }); // ����

        var a = Spin(numProb, operProb);
        Logging.Log($"{a.rouletNum.Num1.Value} {a.rouletOperator.Operator1.std} {a.rouletNum.Num2.Value} {a.rouletOperator.Operator2.std} {a.rouletNum.Num3.Value}");
        rouletUIChannel.Raise(a); // �귿UI

        rouletEndChannel.Raise((data) => { rouletData.Setter(data); }); // ����
        a.rouletNum.Apply(rouletData.rouletNum);
        a.rouletOperator.Apply(rouletData.rouletOperator);
        Logging.Log($"{a.rouletNum.Num1.Value} {a.rouletOperator.Operator1.std} {a.rouletNum.Num2.Value} {a.rouletOperator.Operator2.std} {a.rouletNum.Num3.Value}");
        rouletApplyUIChannel.Raise(a); // ���밪UI


        rouletEndChannel.Raise((data) => { rouletData.Setter(data); }); // ����
        a.rouletNum.Apply(rouletData.rouletNum);
        a.rouletOperator.Apply(rouletData.rouletOperator);
        rouletApplyUIChannel.Raise(a); // ���밪UI

        Logging.Log($"{a.rouletNum.Num1.Value} {a.rouletOperator.Operator1.std} {a.rouletNum.Num2.Value} {a.rouletOperator.Operator2.std} {a.rouletNum.Num3.Value}");

        itemUnsubChannel.Raise(list);
        rouletData = a;
        return () => rouletData.GetResult();
    }
    public RouletData Spin(NumberProbListSO numberProb, OperatorProbListSO operatorProb)
    {
        var result = new RouletData(
            new RouletNum(numberProb.GetRendomNum(), numberProb.GetRendomNum(), numberProb.GetRendomNum(), -1),
            new RouletOperator(operatorProb.GetRendom(), operatorProb.GetRendom(), -1));
        //  UI���� �귿�� ��������(���� ���� �־��ֱ�)
        return result;
    }
}