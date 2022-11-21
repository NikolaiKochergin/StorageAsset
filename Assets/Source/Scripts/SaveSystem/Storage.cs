using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
#if YANDEX_GAMES
using Agava.YandexGames;
#endif
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace SaveSystem
{
    public static class Storage
    {
        private static readonly string DataName = PlayerSettings.productName;

        private static Data _data;

        public static void Load()
        {
            _data ??= new Data();

            if (PlayerPrefs.HasKey(DataName))
            {
                var serialized = PlayerPrefs.GetString(DataName);
                _data = JsonUtility.FromJson<Data>(serialized);
                Save();
            }
            else
            {
                SaveWithDefaultDate();
            }
        }

        public static void Save()
        {
            _data.SaveTime = DateTime.Now.ToString();
            SaveWithDefaultDate();
        }

        private static void SaveWithDefaultDate()
        {
            var serialized = JsonUtility.ToJson(_data);
            PlayerPrefs.SetString(DataName, serialized);
        }

        private static void SaveRemote()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            return;
#endif
#if YANDEX_GAMES
            var serialized = JsonUtility.ToJson(_data);
            PlayerAccount.SetPlayerData(serialized);
#endif
        }

        private static void LoadRemote(Action<Data> onDataLoadedCallback)
        {
#if !UNITY_WEBGL || UNITY_EDITOR
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

        public static IEnumerator SyncRemoteSave(Action onDataIsSynchronizedCallback = null)
        {
            Load();
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

        private static void CheckData()
        {
            if(_data == null) Load();
        }
        
        public static IEnumerator ClearData(Action onRemoteDataCleared = null)
        {
            PlayerPrefs.DeleteAll();
#if !UNITY_WEBGL || UNITY_EDITOR
            onRemoteDataCleared?.Invoke();
            yield return true;
#endif
#if  YANDEX_GAMES && !UNITY_EDITOR
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

        public static void PrintFloatKeys()
        {
            CheckData();
            var keys = _data.Floats.Keys;
            foreach (var key in keys)
            {
                Debug.Log(key);
            }
        }

        public static DateTime GetSaveTime()
        {
            CheckData();
            return DateTime.Parse(_data.SaveTime);
        }
        
        public static void SetLevel(int index)
        {
            CheckData();
            _data.LevelNumber = index;
            Save();
        }

        public static int GetLevel()
        {
            CheckData();
            return _data.LevelNumber;
        }

        public static void SetFloat(string key, float value)
        {
            CheckData();
            if (_data.Floats.ContainsKey(key))
                _data.Floats[key] = value;
            else
                _data.Floats.Add(key, value);
            Save();
        }

        public static float GetFloat(string key, float defaultValue = 0f)
        {
            CheckData();
            return _data.Floats.ContainsKey(key) ? _data.Floats[key] : defaultValue;
        }

        public static void SetInt(string key, int value)
        {
            CheckData();
            if (_data.Ints.ContainsKey(key))
                _data.Ints[key] = value;
            else
                _data.Ints.Add(key, value);
            Save();
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
            CheckData();
            return _data.Ints.ContainsKey(key) ? _data.Ints[key] : defaultValue;
        }

        public static void SetString(string key, string value)
        {
            CheckData();
            if (_data.Strings.ContainsKey(key))
                _data.Strings[key] = value;
            else
                _data.Strings.Add(key, value);
            Save();
        }

        public static string GetString(string key, string defaultValue = "")
        {
            CheckData();
            return _data.Strings.ContainsKey(key) ? _data.Strings[key] : defaultValue;
        }

        public static void AddDisplayedLevelNumber()
        {
            CheckData();
            _data.DisplayedLevelNumber++;
            Save();
        }

        public static int GetDisplayedLevelNumber()
        {
            CheckData();
            return _data.DisplayedLevelNumber;
        }

        public static void AddSession()
        {
            CheckData();
            _data.SessionCount++;
            Save();
        }

        public static int GetSessionCount()
        {
            CheckData();
            return _data.SessionCount;
        }

        public static DateTime GetRegistrationDate()
        {
            CheckData();
            return DateTime.Parse(_data.RegistrationDate);
        }

        public static void SetLastLoginDate()
        {
            CheckData();
            _data.LastLoginDate = DateTime.Now.ToString();
            Save();
        }

        public static DateTime GetLastLoginDate()
        {
            CheckData();
            return DateTime.Parse(_data.LastLoginDate);
        }

        public static int GetNumberDaysAfterRegistration()
        {
            return GetLastLoginDate().Day - GetRegistrationDate().Day;
        }

        public static void SetSoft(int value)
        {
            CheckData();
            _data.Soft = value;
            Save();
        }

        public static int GetSoft()
        {
            CheckData();
            return _data.Soft;
        }
    }

    [Serializable]
    public class Data
    {
        public int LevelNumber = 1;
        public int DisplayedLevelNumber = 1;
        public int SessionCount = 0;
        public string SaveTime = DateTime.MinValue.ToString();
        public string RegistrationDate = DateTime.Now.ToString();
        public string LastLoginDate = DateTime.Now.ToString();
        public SerializedDictionary<string, float> Floats = new();
        public SerializedDictionary<string, int> Ints = new();
        public SerializedDictionary<string, string> Strings = new();
        public int Soft = 0;
    }
    
    [Serializable]
    public class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<TKey> _keys = new();
        [SerializeField]
        private List<TValue> _values = new();
    
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
            for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            {
                Add(_keys[i], _values[i]);
            }
        }
    }
}