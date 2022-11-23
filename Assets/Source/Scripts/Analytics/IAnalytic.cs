using System.Collections.Generic;

namespace Source.Scripts.Analytics
{
    public interface IAnalytic
    {
        void OnGameInitialize(Dictionary<string, object> message);
        void OnLevelStart(Dictionary<string, object> message);
        void OnLevelComplete(Dictionary<string, object> message);
        void OnLevelFail(Dictionary<string, object> message);
        void OnLevelRestart(Dictionary<string, object> message);
        void OnSoftSpent(Dictionary<string, object> message);
        void OnRegistrationDayIs(Dictionary<string, object> message);
        void OnSessionCountIs(Dictionary<string, object> message);
        void OnDaysInGameIs(Dictionary<string, object> message);
        void OnCurrentSoftHave(Dictionary<string, object> message);
    }
}