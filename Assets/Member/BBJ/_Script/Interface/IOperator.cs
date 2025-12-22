public interface IOperator<T>
{
    public string std { get; }
    public T Operation(T p1, T p2);
}