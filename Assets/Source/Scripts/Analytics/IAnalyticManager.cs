using System.Collections.Generic;

namespace Source.Scripts.Analytics
{
    public interface IAnalyticManager
    {
        void SendEventOnGameInitialize(int sessionCount);
        void SendEventOnLevelStart(int levelNumber);
        void SendEventOnLevelComplete(int levelNumber);
        void SendEventOnFail(int levelNumber);
        void SendEventOnLevelRestart(int levelNumber);
        void SendEventOnSoftSpent(string purchaseType, string storeName, int purchaseAmount, int purchasesCount);
        void SendEventOnGameExit(string registrationDate, int sessionCount, int daysInGame);
        void SendEventOnGameExit(string registrationDate, int sessionCount, int daysInGame, int currentSoft);
        void SendEvent(string eventName, Dictionary<string, object> dataObjects);
        void SendEvent(string eventName);
    }
}