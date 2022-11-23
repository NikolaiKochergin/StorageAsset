#if APP_METRICA
using System.Collections.Generic;

namespace Source.Scripts.Analytics
{
    public class AppMetricaAnalytic : IAnalytic
    {
        public void OnGameInitialize(Dictionary<string, object> message)
        {
            AppMetrica.Instance.ReportEvent(AnalyticNames.GameStart, message);
        }

        public void OnLevelStart(Dictionary<string, object> message)
        {
            AppMetrica.Instance.ReportEvent(AnalyticNames.LevelStart, message);
        }

        public void OnLevelComplete(Dictionary<string, object> message)
        {
            AppMetrica.Instance.ReportEvent(AnalyticNames.LevelComplete, message);
        }

        public void OnLevelFail(Dictionary<string, object> message)
        {
            AppMetrica.Instance.ReportEvent(AnalyticNames.Fail, message);
        }

        public void OnLevelRestart(Dictionary<string, object> message)
        {
            AppMetrica.Instance.ReportEvent(AnalyticNames.Restart, message);
        }

        public void OnSoftSpent(Dictionary<string, object> message)
        {
            AppMetrica.Instance.ReportEvent(AnalyticNames.SoftSpent, message);
        }

        public void OnRegistrationDayIs(Dictionary<string, object> message)
        {
            AppMetrica.Instance.ReportEvent(AnalyticNames.RegistrationDay, message);
        }

        public void OnSessionCountIs(Dictionary<string, object> message)
        {
            AppMetrica.Instance.ReportEvent(AnalyticNames.SessionCount, message);
        }

        public void OnDaysInGameIs(Dictionary<string, object> message)
        {
            AppMetrica.Instance.ReportEvent(AnalyticNames.DaysInGame, message);
        }

        public void OnCurrentSoftHave(Dictionary<string, object> message)
        {
            AppMetrica.Instance.ReportEvent(AnalyticNames.CurrentSoft, message);
        }
    }
}
#endif