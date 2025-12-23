using Core.Logger;
using Member.PYH._Scripts.SO;

public struct RouletNum : IRouletApply, IRouletInfo, IPriority
{
    public Vector<float?, int>[] _num;
    public float? Num1
    {
        get => _num[0].x;
        set => _num[0].x = value;
    }
    public float? Num2
    {
        get => _num[1].x;
        set => _num[1].x = value;
    }
    public float? Num3
    {
        get => _num[2].x;
        set => _num[2].x = value;
    }
    public int Priority { get; set; }
    public RouletNum(float? num1 = default, int? num2 = default, int? num3 = default, int Priority = default)
    {
        this._num = new Vector<float?, int>[3];
        this.Priority = Priority;
        this._num[0] = new Vector<float?, int>(num1, Priority);
        this._num[1] = new Vector<float?, int>(num2, Priority);
        this._num[2] = new Vector<float?, int>(num3, Priority);
    }
    public void Apply(RouletNum p2)
    {
        if (p2._num == null) return;

        if (p2.Num1 != null &&( this.Num1.HasValue == false || this._num[0].y < p2._num[0].y))
            this._num[0] = p2._num[0]; 
        if (p2.Num2 != null && ( this.Num2.HasValue == false || this._num[1].y < p2._num[1].y))
            this._num[1] = p2._num[1];
        if (p2.Num3 != null && (this.Num3.HasValue == false || this._num[2].y < p2._num[2].y))
            this._num[2] = p2._num[2];
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