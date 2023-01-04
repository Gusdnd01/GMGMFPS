using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class SaveData
{
    public bool stage1;
    public bool stage2;
    public bool stage3;
}

public class JSON : MonoBehaviour
{
    private string savePath;
    private string saveFileName = "/SaveFIle.txt";
    public SaveData saveData = new SaveData();

    private void Start()
    {
        savePath = Application.dataPath + "/SaveData/";

        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        Load();
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

    [ContextMenu("초기화")]
    public void Reset()
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath + saveFileName, "");
    }
}

