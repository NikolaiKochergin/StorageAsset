using System.Collections.Generic;
using UnityEngine;

#if GAME_ANALYTICS
using GameAnalyticsSDK;
#endif

namespace Source.Scripts.Analytics
{
    public class AnalyticManager
    {
        public AnalyticManager()
        {
#if GAME_ANALYTICS
            GameAnalytics.Initialize();
#endif
        }

        public void SendEventOnGameInitialize(int sessionCount)
        {
            var obj = new Dictionary<string, object>
            {
                {AnalyticNames.Count, sessionCount}
            };
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, AnalyticNames.GameStart, obj);
#endif
        }

        public void SendEventOnLevelStart(int levelNumber)
        {
            var obj = new Dictionary<string, object>
            {
                {AnalyticNames.Level, levelNumber}
            };
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, AnalyticNames.LevelStart, obj);
#endif
        }

        public void SendEventOnLevelComplete(int levelNumber)
        {
            var obj = new Dictionary<string, object>
            {
                {AnalyticNames.Level, levelNumber},
                {AnalyticNames.TimeSpent, (int)Time.timeSinceLevelLoad}
            };
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, AnalyticNames.LevelComplete, obj);
#endif
        }

        public void SendEventOnFail(int levelNumber)
        {
            var obj = new Dictionary<string, object>
            {
                {AnalyticNames.Level, levelNumber},
                {AnalyticNames.TimeSpent, (int)Time.timeSinceLevelLoad}
            };
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, AnalyticNames.Fail, obj);
#endif
        }

        public void SendEventOnLevelRestart(int levelNumber)
        {
            var obj = new Dictionary<string, object>
            {
                {AnalyticNames.Level, levelNumber}
            };
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, AnalyticNames.Restart, obj);
#endif
        }

        public void SendEventOnSoftSpend(string purchaseType, string storeName, int purchaseAmount, int purchasesCount)
        {
            var obj = new Dictionary<string, object>
            {
                {AnalyticNames.Type, purchaseType},
                {AnalyticNames.Name, storeName},
                {AnalyticNames.Amount, purchaseAmount},
                {AnalyticNames.Count, purchasesCount}
            };
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, AnalyticNames.SoftSpent, obj);
#endif
        }

        public void SendEventOnGameExit(string registrationDate, int sessionCount, int daysInGame)
        {
            var regDayObj = new Dictionary<string, object>
            {
                {AnalyticNames.Date, registrationDate}
            };
            var sessionCountObj = new Dictionary<string, object>
            {
                {AnalyticNames.Count, sessionCount}
            };
            var daysInGameObj = new Dictionary<string, object>
            {
                {AnalyticNames.Day, daysInGame}
            };
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, AnalyticNames.RegistrationDay, regDayObj);
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, AnalyticNames.SessionCount, sessionCountObj);
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, AnalyticNames.DaysInGame, daysInGameObj);
#endif
        }

        public void SendEventOnGameExit(string registrationDate, int sessionCount, int daysInGame, int currentSoft)
        {
            var obj = new Dictionary<string, object>
            {
                {AnalyticNames.CurrentSoft, currentSoft}
            };
            SendEventOnGameExit(registrationDate, sessionCount, daysInGame);
#if GAME_ANALYTICS
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, AnalyticNames.CurrentSoft, obj);
#endif
        }
    }
}