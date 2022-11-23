using Source.Scripts;
using Source.Scripts.SceneManagement;
using TMPro;
using UnityEngine;

public class TestCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text _displayedLevelText;
    [SerializeField] private GameSceneManager _gameSceneManager;
    
    private void Start()
    {
        _displayedLevelText.text = "Level " + _gameSceneManager.Storage.GetDisplayedLevelNumber();
    }

    public void OnSyncRemoteDataButtonClicked()
    {
        StartCoroutine(_gameSceneManager.Storage.SyncRemoteSave());
    }

    public void OnClearDataButtonClicked()
    {
        StartCoroutine(_gameSceneManager.Storage.ClearData());
    }
}