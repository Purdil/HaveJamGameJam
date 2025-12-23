using System.Collections.Generic;
using UnityEngine;

public struct RouletData: IRouletApply
{
    public RouletNum rouletNum;
    public RouletOperator rouletOperator;

    public List<ApplyFinal> applyFinal;
    public List<ApplyRouletNum> applyrouletNum;

    public RouletData(RouletNum rouletNum = default, RouletOperator rouletOperator = default, List<ApplyFinal> applyFinals = default, List<ApplyRouletNum> applyRoulets = default)
    {
        this.rouletNum = rouletNum;
        this.rouletOperator = rouletOperator;
        this.applyFinal = new List<ApplyFinal>(applyFinals);
        this.applyrouletNum = new List<ApplyRouletNum>(applyRoulets);
    }
    public RouletData(RouletNum rouletNum = default, RouletOperator rouletOperator = default)
    {
        this.rouletNum = rouletNum;
        this.rouletOperator = rouletOperator;
        this.applyFinal = new List<ApplyFinal>();
        this.applyrouletNum = new List<ApplyRouletNum>();
    }
    public void Setter(IRouletApply rouletInfo)
    {
        if (rouletInfo == null) return;

        if (rouletInfo is RouletNum num)
        {
            this.rouletNum.Apply(num);
        }
        else if (rouletInfo is RouletOperator oper)
        {
            this.rouletOperator.Apply(oper);
        }
        else if (rouletInfo is ApplyRouletNum rouletNum)
        {
            if (applyrouletNum == null) applyrouletNum = new(); 
            applyrouletNum.Add(rouletNum);
        }
        else if (rouletInfo is ApplyFinal final)
        {
            if (applyFinal == null) applyFinal = new(); 
            applyFinal.Add(final);
        }
        else if (rouletInfo is RouletData roulet)
        {
            Setter(roulet.rouletNum);
            Setter(roulet.rouletOperator);
            //if (applyrouletNum == null) applyrouletNum = new(); 
            //applyrouletNum.AddRange( roulet.applyrouletNum);
            //if (applyFinal == null) applyFinal = new(); 
            //applyFinal.AddRange( roulet.applyFinal);
        }
    }

    internal int GetResult()
    {
        applyrouletNum.Sort();
        float result = 0;
        for (int i = 0; i < applyrouletNum.Count; i++)
        {
            ApplyRouletNum item = applyrouletNum[i];
            var a = item.RouletNum;
            rouletNum.Num1 = item.ApplyOperator.Operation(rouletNum.Num1.Value, a.Num1.Value);
            rouletNum.Num2 = item.ApplyOperator.Operation(rouletNum.Num2.Value, a.Num2.Value);
            rouletNum.Num3 = item.ApplyOperator.Operation(rouletNum.Num3.Value, a.Num3.Value);
        }
        if (Mathf.Max(rouletOperator.oper[0].x.Priority , rouletOperator.oper[0].y) < Mathf.Max(rouletOperator.oper[1].x.Priority, rouletOperator.oper[1].y))
        {
            var temp = rouletOperator.oper[1].x.Operation(rouletNum.Num2.GetValueOrDefault(), rouletNum.Num3.GetValueOrDefault());

           result = rouletOperator.oper[0].x.Operation(rouletNum.Num1.GetValueOrDefault(), temp);
        }
        else
        {
            var temp = rouletOperator.oper[0].x.Operation(rouletNum.Num1.GetValueOrDefault(), rouletNum.Num2.GetValueOrDefault());

            result = rouletOperator.oper[1].x.Operation(temp,rouletNum.Num3.GetValueOrDefault());
        }

        for (int i = 0; i < applyFinal.Count; i++)
        {
            var item = applyFinal[i];
            result = item.ApplyOperator.Operation(result, item.final);
        }

        return Mathf.CeilToInt(result);
    }
}