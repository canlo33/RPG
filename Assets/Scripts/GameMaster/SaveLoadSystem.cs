using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadSystem : MonoBehaviour
{
    public static void Save(SavedData objectToSave, string key)
    {
        string path = Application.persistentDataPath + "/saves/";
        Directory.CreateDirectory(path);
        path += key + ".txt";
        File.WriteAllText(path, JsonUtility.ToJson(objectToSave));       
        Debug.Log("Progress Saved!");
    }

    public static SavedData Load(string key)
    {
        string path = Application.persistentDataPath + "/saves/";
        path += key + ".txt";
        SavedData returnValue;
        returnValue = JsonUtility.FromJson<SavedData>(File.ReadAllText(path));
        Debug.Log("Progress Loaded");
        return returnValue;
    }
}
[System.Serializable]
public class SavedData
{
    //Player Related Variables
    public string playerName;
    public int playerLevel;
    public Vector3 playerPosition;
    public int currentPlayerExp;
    //Inventory Related Variables
    public int playerGold;
    public List<string> items = new List<string>();
    public List<int> itemAmount = new List<int>();
    public List<string> equipments = new List<string>();
    //Quest Related Variables
    public List<string> npcNames = new List<string>();
    public List<bool> isQuestComplete = new List<bool>();
    public List<bool> isQuestAssigned = new List<bool>();
}
