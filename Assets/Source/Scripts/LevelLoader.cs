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

    private IEnumerator Start()
    {
#if YANDEX_GAMES && !UNITY_EDITOR
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