using System;
using Source.Scripts.SaveSystem;
using Source.Scripts.SceneManagement;
using UnityEngine;

public class TestCube : MonoBehaviour
{
    private const string CubePosition = nameof(CubePosition);
    private const string CubeQuaternion = nameof(CubeQuaternion);
    
    [SerializeField] private GameSceneManager _gameSceneManager;

    private IStorage _storage;

    private void Start()
    {
        _storage = _gameSceneManager.Storage;

        if (_storage.HasKeyVector3(CubePosition))
            transform.position = _storage.GetVector3(CubePosition);

        if (_storage.HasKeyQuaternion(CubeQuaternion))
            transform.rotation = _storage.GetQuaternion(CubeQuaternion);
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0) | Input.GetMouseButtonUp(1))
            SavePositionAndRotation();
    }

    private void SavePositionAndRotation()
    {
        _storage.SetVector3(CubePosition, transform.position);
        _storage.SetQuaternion(CubeQuaternion, transform.rotation);
    }
}
