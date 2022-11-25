using System;
using System.Collections;
using UnityEngine;
#if YANDEX_GAMES && !UNITY_EDITOR
using Agava.YandexGames;
#endif

namespace Source.Scripts.SaveSystem
{
    public class Storage : IStorage
    {
        private static readonly string DataName = nameof(DataName);

        private readonly SaveMode _mode;
        private Data _data;

        public Storage(SaveMode mode = SaveMode.Delayed)
        {
            _mode = mode;
            _data = Load();
        }

        public void Save()
        {
            _data.SaveTime = DateTime.Now.ToString();
            PlayerPrefs.SetString(DataName, _data.ToJson());
        }

        public IEnumerator ClearData(Action onRemoteDataCleared = null)
        {
            _data = new Data();
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
#if !UNITY_WEBGL || UNITY_EDITOR
            Debug.Log("Data cleared");
            onRemoteDataCleared?.Invoke();
            yield return true;
#endif
#if YANDEX_GAMES && !UNITY_EDITOR
            var isRemoteDateCleared = false;
            PlayerAccount.SetPlayerData("", () =>
            {
                isRemoteDateCleared = true;
                onRemoteDataCleared?.Invoke();
            });

            while (isRemoteDateCleared == false)
                yield return null;
#endif
        }

        public DateTime GetSaveTime()
        {
            return DateTime.Parse(_data.SaveTime);
        }

        public void SetLevel(int index)
        {
            _data.LevelNumber = index;
            if (_mode == SaveMode.Immediately) Save();
        }

        public int GetLevel()
        {
            return _data.LevelNumber;
        }

        public void SetFloat(string key, float value)
        {
            if (_data.Floats.ContainsKey(key))
                _data.Floats[key] = value;
            else
                _data.Floats.Add(key, value);
            if (_mode == SaveMode.Immediately) Save();
        }

        public float GetFloat(string key)
        {
            return _data.Floats.ContainsKey(key)
                ? _data.Floats[key]
                : throw new ArgumentException($"Floats doesn't contain Key: {key}");
        }

        public bool HasKeyFloat(string key)
        {
            return _data.Floats.ContainsKey(key);
        }

        public void SetInt(string key, int value)
        {
            if (_data.Ints.ContainsKey(key))
                _data.Ints[key] = value;
            else
                _data.Ints.Add(key, value);
            if (_mode == SaveMode.Immediately) Save();
        }

        public int GetInt(string key)
        {
            return _data.Ints.ContainsKey(key) 
                ? _data.Ints[key] 
                : throw new ArgumentException($"Ints doesn't contain Key: {key}");
        }

        public bool HasKeyInt(string key)
        {
            return _data.Ints.ContainsKey(key);
        }

        public void SetString(string key, string value)
        {
            if (_data.Strings.ContainsKey(key))
                _data.Strings[key] = value;
            else
                _data.Strings.Add(key, value);
            if (_mode == SaveMode.Immediately) Save();
        }

        public string GetString(string key)
        {
            return _data.Strings.ContainsKey(key) 
                ? _data.Strings[key] 
                : throw new ArgumentException($"Strings doesn't contain Key: {key}");
        }

        public bool HasKeyString(string key)
        {
            return _data.Strings.ContainsKey(key);
        }

        public void SetVector3(string key, Vector3 value)
        {
            if (_data.Vectors.ContainsKey(key))
                _data.Vectors[key] = value.AsVectorData();
            else
                _data.Vectors.Add(key, value.AsVectorData());
            if (_mode == SaveMode.Immediately) Save();
        }

        public Vector3 GetVector3(string key)
        {
            return _data.Vectors.ContainsKey(key)
                ? _data.Vectors[key].AsUnityVector()
                : throw new ArgumentException($"Vectors doesn't contain Key: {key}");
        }

        public bool HasKeyVector3(string key)
        {
            return _data.Vectors.ContainsKey(key);
        }

        public void SetQuaternion(string key, Quaternion value)
        {
            if (_data.Quaternions.ContainsKey(key))
                _data.Quaternions[key] = value.AsQuaternionData();
            else
                _data.Quaternions.Add(key, value.AsQuaternionData());
            if (_mode == SaveMode.Immediately) Save();
        }

        public Quaternion GetQuaternion(string key)
        {
            return _data.Quaternions.ContainsKey(key)
                ? _data.Quaternions[key].AsUnityQuaternion()
                : throw new ArgumentException($"Quaternions doesn't contain Key: {key}");
        }

        public bool HasKeyQuaternion(string key)
        {
            return _data.Quaternions.ContainsKey(key);
        }

        public void AddDisplayedLevelNumber()
        {
            _data.DisplayedLevelNumber++;
            if (_mode == SaveMode.Immediately) Save();
        }

        public int GetDisplayedLevelNumber()
        {
            return _data.DisplayedLevelNumber;
        }

        public void AddSession()
        {
            _data.SessionCount++;
            if (_mode == SaveMode.Immediately) Save();
        }

        public int GetSessionCount()
        {
            return _data.SessionCount;
        }

        public DateTime GetRegistrationDate()
        {
            return DateTime.Parse(_data.RegistrationDate);
        }

        public void SetLastLoginDate()
        {
            _data.LastLoginDate = DateTime.Now.ToString();
            if (_mode == SaveMode.Immediately) Save();
        }

        public DateTime GetLastLoginDate()
        {
            return DateTime.Parse(_data.LastLoginDate);
        }

        public int GetNumberDaysAfterRegistration()
        {
            return GetLastLoginDate().Day - GetRegistrationDate().Day;
        }

        public void SetSoft(int value)
        {
            _data.Soft = value;
            if (_mode == SaveMode.Immediately) Save();
        }

        public int GetSoft()
        {
            return _data.Soft;
        }

#if UNITY_WEBGL
        public void SaveRemote()
        {
            Save();
#if !UNITY_WEBGL || UNITY_EDITOR
            Debug.Log("Saved to remote storage");
#endif
#if YANDEX_GAMES && !UNITY_EDITOR
            PlayerAccount.SetPlayerData(_data.ToJson());
#endif
        }

        public IEnumerator SyncRemoteSave(Action onDataIsSynchronizedCallback = null)
        {
            var isRemoteDataLoaded = false;
            LoadRemote(remoteData =>
            {
                remoteData ??= new Data();

                var localDataSaveTime = DateTime.Parse(_data.SaveTime);
                var remoteDataSaveTime = DateTime.Parse(remoteData.SaveTime);

                if (remoteDataSaveTime > localDataSaveTime)
                {
                    _data = remoteData;
                    Save();
                }
                else
                {
                    SaveRemote();
                }

                isRemoteDataLoaded = true;
                onDataIsSynchronizedCallback?.Invoke();
            });

            while (isRemoteDataLoaded == false)
                yield return null;
        }

        private void LoadRemote(Action<Data> onDataLoadedCallback)
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            Debug.Log("Loaded from remote storage");
            onDataLoadedCallback?.Invoke(null);
#endif
#if YANDEX_GAMES && !UNITY_EDITOR
            PlayerAccount.GetPlayerData(data =>
            {
                onDataLoadedCallback?.Invoke(data == "" ? null : data.ToDeserialized<Data>());
            });
#endif
        }
#endif
        private Data Load()
        {
            return PlayerPrefs.GetString(DataName)?.ToDeserialized<Data>() ?? new Data();
        }
    }
}