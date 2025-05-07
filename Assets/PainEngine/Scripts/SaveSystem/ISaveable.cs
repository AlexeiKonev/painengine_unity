namespace MyGame.SaveSystem
{
    public interface ISaveable
    {
        SaveData GetSaveData();
        void LoadSaveData(SaveData data);
    }
}