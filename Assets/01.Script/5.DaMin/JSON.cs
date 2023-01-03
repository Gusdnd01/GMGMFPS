using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSON : MonoBehaviour
{
    private string savePath;
    private string saveFileName = "/SaveFIle.txt";
    private SaveData saveData = new SaveData();

    private void Start()
    {
        savePath = Application.dataPath + "/SaveData/";

        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);
    }

    [ContextMenu("저장")]
    public void Save()
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath + saveFileName, json);
    }

    [ContextMenu("로드")]
    public void Load()
    {
        if (File.Exists(savePath + saveFileName))
        {
            string json = File.ReadAllText(savePath + saveFileName);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
    }
}

[System.Serializable]
public class SaveData
{
    public bool stage1;
    public bool stage2;
    public bool stage3;
}