using System;
using System.Collections;

namespace Source.Scripts.SaveSystem
{
    public interface IStorage
    {
        void Save();
        IEnumerator ClearData(Action onRemoteDataCleared = null);
        DateTime GetSaveTime();
        void SetLevel(int index);
        int GetLevel();
        void SetFloat(string key, float value);
        float GetFloat(string key);
        bool HasKeyFloat(string key);
        void SetInt(string key, int value);
        int GetInt(string key);
        bool HasKeyInt(string key);
        void SetString(string key, string value);
        string GetString(string key);
        bool HasKeyString(string key);
        void AddDisplayedLevelNumber();
        int GetDisplayedLevelNumber();
        void AddSession();
        int GetSessionCount();
        DateTime GetRegistrationDate();
        void SetLastLoginDate();
        DateTime GetLastLoginDate();
        int GetNumberDaysAfterRegistration();
        void SetSoft(int value);
        int GetSoft();
        void SaveRemote();
        IEnumerator SyncRemoteSave(Action onDataIsSynchronizedCallback = null);
    }
}