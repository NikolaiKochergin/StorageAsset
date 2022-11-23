using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Analytics
{
    public class AnalyticManager
    {
        private readonly List<IAnalytic> _analytics = new();

        public AnalyticManager()
        {
#if GAME_ANALYTICS
            _analytics.Add(new GameAnalytic());
#endif
#if APP_METRICA
            _analytics.Add(new AppMetricaAnalytic());
#endif
        }

        public void SendEventOnGameInitialize(int sessionCount)
        {
            var obj = new Dictionary<string, object>
            {
                {AnalyticNames.Count, sessionCount}
            };

            foreach (var analytic in _analytics)
                analytic.OnGameInitialize(obj);
        }

        public void SendEventOnLevelStart(int levelNumber)
        {
            var obj = new Dictionary<string, object>
            {
                {AnalyticNames.Level, levelNumber}
            };
            foreach (var analytic in _analytics)
                analytic.OnLevelStart(obj);
        }

        public void SendEventOnLevelComplete(int levelNumber)
        {
            var obj = new Dictionary<string, object>
            {
                {AnalyticNames.Level, levelNumber},
                {AnalyticNames.TimeSpent, (int) Time.timeSinceLevelLoad}
            };
            foreach (var analytic in _analytics)
                analytic.OnLevelComplete(obj);
        }

        public void SendEventOnFail(int levelNumber)
        {
            var obj = new Dictionary<string, object>
            {
                {AnalyticNames.Level, levelNumber},
                {AnalyticNames.TimeSpent, (int) Time.timeSinceLevelLoad}
            };
            foreach (var analytic in _analytics)
                analytic.OnLevelFail(obj);
        }

        public void SendEventOnLevelRestart(int levelNumber)
        {
            var obj = new Dictionary<string, object>
            {
                {AnalyticNames.Level, levelNumber}
            };
            foreach (var analytic in _analytics)
                analytic.OnLevelRestart(obj);
        }

        public void SendEventOnSoftSpent(string purchaseType, string storeName, int purchaseAmount, int purchasesCount)
        {
            var obj = new Dictionary<string, object>
            {
                {AnalyticNames.Type, purchaseType},
                {AnalyticNames.Name, storeName},
                {AnalyticNames.Amount, purchaseAmount},
                {AnalyticNames.Count, purchasesCount}
            };
            foreach (var analytic in _analytics)
                analytic.OnSoftSpent(obj);
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
            foreach (var analytic in _analytics)
            {
                analytic.OnRegistrationDayIs(regDayObj);
                analytic.OnSessionCountIs(sessionCountObj);
                analytic.OnDaysInGameIs(daysInGameObj);
            }
        }

        public void SendEventOnGameExit(string registrationDate, int sessionCount, int daysInGame, int currentSoft)
        {
            var obj = new Dictionary<string, object>
            {
                {AnalyticNames.CurrentSoft, currentSoft}
            };
            SendEventOnGameExit(registrationDate, sessionCount, daysInGame);
            foreach (var analytic in _analytics)
                analytic.OnCurrentSoftHave(obj);
        }
    }
}