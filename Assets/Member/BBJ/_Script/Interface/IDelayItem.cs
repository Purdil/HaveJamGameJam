using BBJ;

public interface IDelayItem : IItem
{
    public bool isDelay { get; }
    public void StartDelay();
}
