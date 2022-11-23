#if GAME_ANALYTICS
using System.Collections.Generic;
using GameAnalyticsSDK;

namespace Source.Scripts.Analytics
{
    public class GameAnalytic : IAnalytic
    {
        public GameAnalytic()
        {
            GameAnalytics.Initialize();
        }

        public void OnGameInitialize(Dictionary<string, object> message)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, AnalyticNames.GameStart, message);
        }

        public void OnLevelStart(Dictionary<string, object> message)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, AnalyticNames.LevelStart, message);
        }

        public void OnLevelComplete(Dictionary<string, object> message)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, AnalyticNames.LevelComplete, message);
        }

        public void OnLevelFail(Dictionary<string, object> message)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, AnalyticNames.Fail, message);
        }

        public void OnLevelRestart(Dictionary<string, object> message)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, AnalyticNames.Restart, message);
        }

        public void OnSoftSpent(Dictionary<string, object> message)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, AnalyticNames.SoftSpent, message);
        }

        public void OnRegistrationDayIs(Dictionary<string, object> message)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, AnalyticNames.RegistrationDay,
                message);
        }

        public void OnSessionCountIs(Dictionary<string, object> message)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, AnalyticNames.SessionCount,
                message);
        }

        public void OnDaysInGameIs(Dictionary<string, object> message)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, AnalyticNames.DaysInGame,
                message);
        }

        public void OnCurrentSoftHave(Dictionary<string, object> message)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, AnalyticNames.CurrentSoft,
                message);
        }
    }
}
#endif
