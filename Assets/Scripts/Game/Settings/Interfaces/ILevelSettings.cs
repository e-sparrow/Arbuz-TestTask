namespace Game.Settings.Interfaces
{
    public interface ILevelSettings
    {
        float LoadingTimerLength
        {
            get;
        }

        float InGameTimerLength
        {
            get;
        }
        
        int CoinsCount
        {
            get;
        }
    }
}