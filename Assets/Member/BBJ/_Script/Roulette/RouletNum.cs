using Core.Logger;
using Member.PYH._Scripts.SO;
using System;

public struct RouletNum : IRouletApply, IRouletInfo, IPriority
{
    public Vector<float?, int>[] Num;
    public float? Num1
    {
        get
        {
            if (Num == null) Num = new Vector<float?, int>[3];
            return Num[0].x;
        }
        set
        {
            if (Num == null) Num = new Vector<float?, int>[3];
            Num[0].x = value;
        }
    }

    public float? Num2
    {
        get
        {
            if (Num == null) Num = new Vector<float?, int>[3];
            return Num[1].x;
        }
        set
        {
            if (Num == null) Num = new Vector<float?, int>[3];
            Num[1].x = value;
        }
    }

    public float? Num3
    {
        get
        {
            if (Num == null) Num = new Vector<float?, int>[3];
            return Num[2].x;
        }
        set
        {
            if (Num == null) Num = new Vector<float?, int>[3];
            Num[2].x = value;
        }
    }
    public int Priority { get; set; }
    public RouletNum(float? num1 = default, int? num2 = default, int? num3 = default, int Priority = default)
    {
        this.Num = new Vector<float?, int>[3];
        this.Priority = Priority;
        this.Num[0] = new Vector<float?, int>(num1, Priority);
        this.Num[1] = new Vector<float?, int>(num2, Priority);
        this.Num[2] = new Vector<float?, int>(num3, Priority);
    }
    public void Apply(RouletNum p2)
    {
        if (p2.Num == null) return;

        if (p2.Num1 != null &&( this.Num1.HasValue == false || this.Num[0].y < p2.Num[0].y))
            this.Num[0] = p2.Num[0]; 
        if (p2.Num2 != null && ( this.Num2.HasValue == false || this.Num[1].y < p2.Num[1].y))
            this.Num[1] = p2.Num[1];
        if (p2.Num3 != null && (this.Num3.HasValue == false || this.Num[2].y < p2.Num[2].y))
            this.Num[2] = p2.Num[2];
    }

    internal void Apply(ApplyRouletNum applyRouletNum)
    {
        if (applyRouletNum.ApplyOperator == null) return;
        if (applyRouletNum.RouletNum.Num1.HasValue == true)
        this.Num1 = applyRouletNum.ApplyOperator.Operation(this.Num1.GetValueOrDefault() ,applyRouletNum.RouletNum.Num1.GetValueOrDefault());
        if (applyRouletNum.RouletNum.Num2.HasValue == true)          
        this.Num2 = applyRouletNum.ApplyOperator.Operation(this.Num2.GetValueOrDefault() ,applyRouletNum.RouletNum.Num2.GetValueOrDefault());
        if (applyRouletNum.RouletNum.Num3.HasValue == true)          
        this.Num3 = applyRouletNum.ApplyOperator.Operation(this.Num3.GetValueOrDefault(), applyRouletNum.RouletNum.Num3.GetValueOrDefault());
    }
}
public struct Vector<T, V>
{
    public T x;
    public V y;
    public Vector(T x = default, V y = default)
    {
        this.x = x;
        this.y = y;
    }
}