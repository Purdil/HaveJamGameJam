using Member.PYH._Scripts.Inventory;
using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class RouletteManager : MonoBehaviour
{
    [SerializeField] private NumberProbListSO numProb;
    [SerializeField] private OperatorProbListSO operProb;

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartRoulette();
        }
    }
#endif
    public void StartRoulette()
    {
        // 시작 전 이벤트를 실행하여 가중치에 따라서 초기값을 설정


        //  룰렛을 돌려야하는 거 
    }
}

public struct RouletNum: IRouletApply, IRouletInfo
{
    public int? Num1, Num2, Num3;
    public int Priority;
    public RouletNum(int? num1 = default, int? num2 = default, int? num3 = default, int Priority = default)
    {
        this.Num1 = num1;
        this.Num2 = num2;
        this.Num3 = num3;
        this.Priority = Priority;
    }
}
public struct ApplyFinal : IRouletApply
{
    public int final;
    public OperatorSO ApplyOperator;
    public ApplyFinal(int final = default, OperatorSO applyOperator = default)
    {
        this.final = final;
        this.ApplyOperator = applyOperator;
    }
}
public struct ApplyRouletNum: IRouletApply
{
    public RouletNum RouletNum;
    public OperatorSO ApplyOperator;
    public ApplyRouletNum(int num1 = default, int num2 = default, int num3 = default, OperatorSO applyOperator = default)
    {
        this.RouletNum = new RouletNum(num1, num2, num3);
        this.ApplyOperator = applyOperator;
    }
    public ApplyRouletNum(RouletNum rouletNum, OperatorSO applyOperator = default)
    {
        this.RouletNum = rouletNum;
        this.ApplyOperator = applyOperator;
    }
}
public struct RouletOperator:IRouletApply, IRouletInfo
{
    public OperatorSO Operator1, Operator2;
    public int Priority;
    public RouletOperator(OperatorSO operator1 = default, OperatorSO operator2 = default, int priority = default)
    {
        this.Operator1 = operator1;
        this.Operator2 = operator2;
        this.Priority = priority;
    }
}
public interface IRouletApply{}
public interface IRouletInfo {}
