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

    public void LoadNextLevel()
    {
        var nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevelIndex >= SceneManager.sceneCountInBuildSettings)
            nextLevelIndex = _repeatFromLevel;
        
        Storage.SetLevel(nextLevelIndex);
        Storage.AddDisplayedLevelNumber();

        SceneManager.LoadScene(nextLevelIndex);
    }
}