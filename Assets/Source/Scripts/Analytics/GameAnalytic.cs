using System.Collections.Generic;
#if GAME_ANALYTICS
using GameAnalyticsSDK;
#endif

namespace Source.Scripts.Analytics
{
    public class GameAnalytic : IAnalytic
    {
        public GameAnalytic()
        {
#if GAME_ANALYTICS
            GameAnalytics.Initialize();
#endif
        }

        public void OnGameInitialize(Dictionary<string, object> message)
        {
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, AnalyticNames.GameStart, message);
#endif
        }

        public void OnLevelStart(Dictionary<string, object> message)
        {
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, AnalyticNames.LevelStart, message);
#endif
        }

        public void OnLevelComplete(Dictionary<string, object> message)
        {
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, AnalyticNames.LevelComplete, message);
#endif
        }

        public void OnLevelFail(Dictionary<string, object> message)
        {
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, AnalyticNames.Fail, message);
#endif
        }

        public void OnLevelRestart(Dictionary<string, object> message)
        {
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, AnalyticNames.Restart, message);
#endif
        }

        public void OnSoftSpent(Dictionary<string, object> message)
        {
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, AnalyticNames.SoftSpent, message);
#endif
        }

        public void OnRegistrationDayIs(Dictionary<string, object> message)
        {
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, AnalyticNames.RegistrationDay,
                message);
#endif
        }

        public void OnSessionCountIs(Dictionary<string, object> message)
        {
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, AnalyticNames.SessionCount,
                message);
#endif
        }

        public void OnDaysInGameIs(Dictionary<string, object> message)
        {
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, AnalyticNames.DaysInGame,
                message);
#endif
        }

        public void OnCurrentSoftHave(Dictionary<string, object> message)
        {
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, AnalyticNames.CurrentSoft,
                message);
#endif
        }
    }
}