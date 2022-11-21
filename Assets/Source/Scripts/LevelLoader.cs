using System.Collections;
#if YANDEX_GAMES
using Agava.YandexGames;
#endif
using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private bool _isClearDataOnStart;
    
    private IEnumerator Start()
    {
#if  YANDEX_GAMES && !UNITY_EDITOR
        yield return YandexGamesSdk.Initialize();
        PlayerAccount.RequestPersonalProfileDataPermission();
        PlayerAccount.Authorize();
#endif
        if (_isClearDataOnStart)
        {
            Debug.Log("Data cleared");
            yield return Storage.ClearData();
        }

        yield return Storage.SyncRemoteSave();
        
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        Debug.Log("level loaded");
        SceneManager.LoadScene(Storage.GetLevel());
    }
}