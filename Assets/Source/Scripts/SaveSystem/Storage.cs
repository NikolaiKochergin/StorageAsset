using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if YANDEX_GAMES
using Agava.YandexGames;
#endif

namespace SaveSystem
{
    public class Storage : IStorage
    {
        private static readonly string DataName = nameof(DataName);

        private readonly SaveMode _mode;
        private Data _data;

        public Storage(SaveMode mode = SaveMode.Immediately)
        {
            _mode = mode;
            _data = Load();
        }

        private Data Load()
        {
            if (PlayerPrefs.HasKey(DataName))
            {
                var serialized = PlayerPrefs.GetString(DataName);
                return JsonUtility.FromJson<Data>(serialized);
            }
            return new Data();
        }

        public void Save()
        {
            _data.SaveTime = DateTime.Now.ToString();
            var serialized = JsonUtility.ToJson(_data);
            PlayerPrefs.SetString(DataName, serialized);
        }

#if UNITY_WEBGL

        private void LoadRemote(Action<Data> onDataLoadedCallback)
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            Debug.Log("Loaded from remote storage");
            onDataLoadedCallback?.Invoke(null);
            return;
#endif
#if YANDEX_GAMES
            PlayerAccount.GetPlayerData(data =>
            {
                if (data == "")
                {
                    onDataLoadedCallback?.Invoke(null);
                }
                else
                {
                    var remoteData = JsonUtility.FromJson<Data>(data);
                    onDataLoadedCallback?.Invoke(remoteData);
                }
            });
#endif
        }

        public void SaveRemote()
        {
            Save();
#if !UNITY_WEBGL || UNITY_EDITOR
            Debug.Log("Saved to remote storage");
            return;
#endif
#if YANDEX_GAMES
            var serialized = JsonUtility.ToJson(_data);
            PlayerAccount.SetPlayerData(serialized);
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
#endif
        public IEnumerator ClearData(Action onRemoteDataCleared = null)
        {
            _data = null;
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
            if(_mode == SaveMode.Immediately) Save();
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
            if(_mode == SaveMode.Immediately) Save();
        }

        public float GetFloat(string key, float defaultValue = 0f)
        {
            return _data.Floats.ContainsKey(key) ? _data.Floats[key] : defaultValue;
        }

        public void SetInt(string key, int value)
        {
            if (_data.Ints.ContainsKey(key))
                _data.Ints[key] = value;
            else
                _data.Ints.Add(key, value);
            if(_mode == SaveMode.Immediately) Save();
        }

        public int GetInt(string key, int defaultValue = 0)
        {
            return _data.Ints.ContainsKey(key) ? _data.Ints[key] : defaultValue;
        }

        public void SetString(string key, string value)
        {
            if (_data.Strings.ContainsKey(key))
                _data.Strings[key] = value;
            else
                _data.Strings.Add(key, value);
            if(_mode == SaveMode.Immediately) Save();
        }

        public string GetString(string key, string defaultValue = "")
        {
            return _data.Strings.ContainsKey(key) ? _data.Strings[key] : defaultValue;
        }

        public void AddDisplayedLevelNumber()
        {
            _data.DisplayedLevelNumber++;
            if(_mode == SaveMode.Immediately) Save();
        }

        public int GetDisplayedLevelNumber()
        {
            return _data.DisplayedLevelNumber;
        }

        public void AddSession()
        {
            _data.SessionCount++;
            if(_mode == SaveMode.Immediately) Save();
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
            if(_mode == SaveMode.Immediately) Save();
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
            if(_mode == SaveMode.Immediately) Save();
        }

        public int GetSoft()
        {
            return _data.Soft;
        }
    }
    
    public enum SaveMode
    {
        Immediately,
        Delayed
    }

    [Serializable]
    public class Data
    {
        public int LevelNumber = 1;
        public int DisplayedLevelNumber = 1;
        public int SessionCount;
        public string SaveTime = DateTime.MinValue.ToString();
        public string RegistrationDate = DateTime.Now.ToString();
        public string LastLoginDate = DateTime.Now.ToString();
        public SerializedDictionary<string, float> Floats = new();
        public SerializedDictionary<string, int> Ints = new();
        public SerializedDictionary<string, string> Strings = new();
        public int Soft;
    }

    [Serializable]
    public class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> _keys = new();

        [SerializeField] private List<TValue> _values = new();

        public void OnBeforeSerialize()
        {
            _keys.Clear();
            _values.Clear();
            foreach (var kvp in this)
            {
                _keys.Add(kvp.Key);
                _values.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            Clear();
            for (var i = 0; i != Math.Min(_keys.Count, _values.Count); i++) Add(_keys[i], _values[i]);
        }
    }
}