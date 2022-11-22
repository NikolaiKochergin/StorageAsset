using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] [Min(1)] private int _repeatFromLevel = 1;

    private void Awake()
    {
        Storage.SetLevel(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        Debug.Log("Level" + SceneManager.GetActiveScene().buildIndex + "Start");
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) 
            Storage.SyncRemoteSave();
    }

    private void OnApplicationQuit()
    {
        Storage.SyncRemoteSave();
    }

    public void LoadNextLevel()
    {
        var nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevelIndex >= SceneManager.sceneCountInBuildSettings)
            nextLevelIndex = _repeatFromLevel;

        Storage.SetLevel(nextLevelIndex);
        Storage.AddDisplayedLevelNumber();
        Storage.SyncRemoteSave();

        Debug.Log("Level" + SceneManager.GetActiveScene().buildIndex + "Complete");

        SceneManager.LoadScene(nextLevelIndex);
    }

    public void LevelFail()
    {
        Debug.Log("Level" + SceneManager.GetActiveScene().buildIndex + "Fail");
    }

    public void ReloadScene()
    {
        Storage.SyncRemoteSave();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}