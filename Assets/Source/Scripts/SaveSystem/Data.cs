using System;

namespace Source.Scripts.SaveSystem
{
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
        public SerializedDictionary<string, Vector3Data> Vectors = new();
        public SerializedDictionary<string, QuaternionData> Quaternions = new();
        public int Soft;
    }
}