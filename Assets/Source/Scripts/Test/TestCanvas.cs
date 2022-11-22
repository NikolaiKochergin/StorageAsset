using SaveSystem;
using TMPro;
using UnityEngine;

public class TestCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text _displayedLevelText;
    
    private void Start()
    {
        _displayedLevelText.text = "Level " + Storage.GetDisplayedLevelNumber();
    }

    public void OnSyncRemoteDataButtonClicked()
    {
        StartCoroutine(Storage.SyncRemoteSave());
    }

    public void OnClearDataButtonClicked()
    {
        StartCoroutine(Storage.ClearData());
    }
}