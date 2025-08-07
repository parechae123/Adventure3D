using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Parsing
{

    public class JsonUtilityExample : MonoBehaviour
    {
        public UserData userData;
        void Start()
        {
            Save();
        }

        void Save()
        {
            var saveData = JsonUtility.ToJson(userData);

            File.WriteAllText(Application.persistentDataPath + "/UserData.txt", saveData);

            Debug.Log(Application.persistentDataPath);
        }
        void Load()
        {
            string loadData = File.ReadAllText(Application.persistentDataPath + "/UserData.txt");
            var saveData = JsonUtility.FromJson<UserData>(loadData);
            Debug.Log(Application.persistentDataPath);
        }
    }
    [System.Serializable]
    public class UserData
    {
        public int a;
        public int b;
        public string n;

        public List<string> friends = new List<string>();
        public Inventory inven;
    }
    [System.Serializable]
    public class Inventory
    {
        public int size;
    }
    [System.Serializable]
    public class Item
    {
        public string name;
        public int id;
        public int cost;
    }
}