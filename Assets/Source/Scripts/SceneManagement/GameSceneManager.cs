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
        public AnalyticManager Analytic { get; private set; }

        private void Awake()
        {
            Storage = new Storage();
            Analytic = new AnalyticManager();
            Storage.SetLevel(SceneManager.GetActiveScene().buildIndex);
        }

        private void Start()
        {
            Analytic.SendEventOnLevelStart(Storage.GetDisplayedLevelNumber());
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                Storage.SaveRemote();
            
                Analytic.SendEventOnGameExit(
                    Storage.GetRegistrationDate().ToString(),
                    Storage.GetSessionCount(),
                    Storage.GetNumberDaysAfterRegistration(),
                    Storage.GetSoft());
            }
        }

        private void OnApplicationQuit()
        {
            Storage.SaveRemote();
        
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
            Storage.SaveRemote();

            Analytic.SendEventOnLevelComplete(Storage.GetDisplayedLevelNumber());

            SceneManager.LoadScene(nextLevelIndex);
        }

        public void OnLevelFail()
        {
            Storage.SaveRemote();
        
            Analytic.SendEventOnFail(Storage.GetDisplayedLevelNumber());
        }

        public void OnReloadScene()
        {
            Storage.SaveRemote();
        
            Analytic.SendEventOnLevelRestart(Storage.GetDisplayedLevelNumber());
        
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}