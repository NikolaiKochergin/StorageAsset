using System;
using System.Collections;

namespace SaveSystem
{
    public interface IStorage
    {
        void Save();
#if UNITY_WEBGL
        IEnumerator SyncRemoteSave(Action onDataIsSynchronizedCallback = null);
#endif
        IEnumerator ClearData(Action onRemoteDataCleared = null);
        DateTime GetSaveTime();
        void SetLevel(int index);
        int GetLevel();
        void SetFloat(string key, float value);
        float GetFloat(string key, float defaultValue = 0f);
        void SetInt(string key, int value);
        int GetInt(string key, int defaultValue = 0);
        void SetString(string key, string value);
        string GetString(string key, string defaultValue = "");
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
    }
}