using System;
using System.Collections;
using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
#if YANDEX_GAMES
using Agava.YandexGames;
#endif

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private bool _isClearDataOnStart;
    
    public Storage Storage { get; private set; }

    private void Awake()
    {
        Storage = new Storage();
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
            yield return Storage.ClearData();
        }
#if UNITY_WEBGL
        yield return Storage.SyncRemoteSave(()=> Debug.Log("Data is syncronized"));
        yield return new WaitForSeconds(0.25f);
#endif
        LoadLevel();
    }

    private void LoadLevel()
    {
        Storage.AddSession();
        Storage.SetLastLoginDate();

        var index = Storage.GetLevel();
        index = Mathf.Clamp(index, 1, SceneManager.sceneCountInBuildSettings - 1);

        SceneManager.LoadScene(index);
    }
}