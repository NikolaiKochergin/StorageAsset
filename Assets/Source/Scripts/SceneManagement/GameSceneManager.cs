using Source.Scripts.Analytics;
using Source.Scripts.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.SceneManagement
{
    public class GameSceneManager : MonoBehaviour
    {
        [SerializeField] [Min(1)] private int _repeatFromLevel = 1;

        public IStorage Storage { get; private set; }
        public IAnalyticManager Analytic { get; private set; }

        private void Awake()
        {
            Storage = new Storage();
            Analytic = new AnalyticManager();
#if GAME_ANALYTICS
            Analytic.AddAnalytic(new GameAnalyticsAnalytic());
#endif
#if APP_METRICA
            Analytic.AddAnalytic(new AppMetricaAnalytic());
#endif
#if YANDEX_METRICA && !UNITY_EDITOR
            Analytic.AddAnalytic(new YandexMetricaAnalytic());
#endif
            Storage.SetLevel(SceneManager.GetActiveScene().buildIndex);
            Storage.Save();
        }

        private void Start()
        {
            Analytic.SendEventOnLevelStart(Storage.GetDisplayedLevelNumber());
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
#if UNITY_WEBGL
                Storage.SaveRemote();
#else
                Storage.Save();
#endif
                Analytic.SendEventOnGameExit(
                    Storage.GetRegistrationDate().ToString(),
                    Storage.GetSessionCount(),
                    Storage.GetNumberDaysAfterRegistration(),
                    Storage.GetSoft());
            }
        }

        private void OnApplicationQuit()
        {
#if UNITY_WEBGL
            Storage.SaveRemote();
#else
            Storage.Save();
#endif
            Analytic.SendEventOnGameExit(
                Storage.GetRegistrationDate().ToString(),
                Storage.GetSessionCount(),
                Storage.GetNumberDaysAfterRegistration(),
                Storage.GetSoft());
        }

        public void OnLoadNextLevel()
        {
            var nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextLevelIndex >= SceneManager.sceneCountInBuildSettings)
                nextLevelIndex = _repeatFromLevel;

            Storage.SetLevel(nextLevelIndex);
            Storage.AddDisplayedLevelNumber();
#if UNITY_WEBGL
            Storage.SaveRemote();
#else
            Storage.Save();
#endif
            Analytic.SendEventOnLevelComplete(Storage.GetDisplayedLevelNumber());

            SceneManager.LoadScene(nextLevelIndex);
        }

        public void OnLevelFail()
        {
#if UNITY_WEBGL
            Storage.SaveRemote();
#else
            Storage.Save();
#endif
            Analytic.SendEventOnFail(Storage.GetDisplayedLevelNumber());
        }

        public void OnReloadScene()
        {
#if UNITY_WEBGL
            Storage.SaveRemote();
#else
            Storage.Save();
#endif
            Analytic.SendEventOnLevelRestart(Storage.GetDisplayedLevelNumber());
        
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}