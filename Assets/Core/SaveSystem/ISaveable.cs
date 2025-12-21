namespace Core.SaveSystem
{
    public interface ISaveable
    {
        SaveId SaveId { get; }
        string GetSaveData();
        void RestoreData(string loadData);
    }
}