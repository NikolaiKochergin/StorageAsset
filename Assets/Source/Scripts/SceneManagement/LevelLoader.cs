using System.Collections;
using System.Collections.Generic;
using Source.Scripts.Analytics;
using Source.Scripts.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
#if YANDEX_GAMES && !UNITY_EDITOR
using Agava.YandexGames;
#endif

namespace Source.Scripts.SceneManagement
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private bool _isClearDataOnStart;
    
        public IStorage Storage { get; private set; }
        public IAnalyticManager Analytic { get; private set; }

        private void Awake()
        {
            Storage = new Storage(DataNames.MyGameName);
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
        }

        private IEnumerator Start()
        {
#if YANDEX_GAMES && !UNITY_EDITOR
        yield return YandexGamesSdk.Initialize(()=>Debug.Log("Yandex SDK is initialized"));
        PlayerAccount.RequestPersonalProfileDataPermission();
        if(PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize();
        else
            Debug.Log("Player is autorized.");    
#endif
            if (_isClearDataOnStart)
            {
                Debug.Log("Data is cleared");
                yield return Storage.ClearDataRemote();
            }
#if UNITY_WEBGL
            yield return Storage.SyncRemoteSave(()=> Debug.Log("Data is synchronized"));
            yield return new WaitForSeconds(0.25f);
#endif
            LoadLevel();
        }

        private void LoadLevel()
        {
            Storage.AddSession();
            Storage.SetLastLoginDate();
            Storage.Save();
            Analytic.SendEventOnGameInitialize(Storage.GetSessionCount());

            var index = Storage.GetLevel();
            index = Mathf.Clamp(index, 1, SceneManager.sceneCountInBuildSettings - 1);

            SceneManager.LoadScene(index);
        }
    }
}