using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Json_StageData : MonoSingleTon<Json_StageData>
{
    StageClearData stg;


    public void Loading(string s)
    {
        string path = Path.Combine(Application.dataPath, $"{s}_StageData.json");
        string jsonData = File.ReadAllText(path);
        stg = JsonUtility.FromJson<StageClearData>(jsonData);

    }

    public void Saveing(string stagename, float cleartime)
    {
        try
        {
            Loading(stagename);
            if (stg.ClearTime < cleartime)
                stg.ClearTime = cleartime;
        }
        catch
        {
            Debug.LogError("json 없으니 생성하겠음");
            stg = new StageClearData();
            stg.ClearTime = cleartime;
        }

        string jsonData = JsonUtility.ToJson(stg, true);
        string path = Path.Combine(Application.dataPath, $"{stagename}_StageData.json");
        File.WriteAllText(path, jsonData);
    }

    public StageClearData ReturnStageData(string stagename)
    {

        Loading(stagename);
        return stg;
    }

}


public class Json_Stage : EditorWindow
{

    string _stageName = "";
    string _cleartime = "";


    [MenuItem("StageJson/SaveStage")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(Json_Stage));
    }
    void OnGUI()
    {
        GUILayout.Label("저장", EditorStyles.boldLabel);
        _stageName = EditorGUILayout.TextField("스테이지 이름", _stageName);
        _cleartime = EditorGUILayout.TextField("클리어 시간(임의)", _cleartime);

        if (GUILayout.Button("저장"))
        {
            if(_cleartime != "" && _stageName != "")
            {
                Makeing();
            }
            else
            {
                Debug.LogError("값 지정 해주세요");
            }
        }
    }

    public void Makeing()
    {

        Saveing();

    }

    void Saveing()
    {
        string jsonData = JsonUtility.ToJson(_stageName, true);
        string path = Path.Combine(Application.dataPath, $"{_stageName}_StageData.json");
        File.WriteAllText(path, jsonData);
    }
}

public class StageClearData
{

    public float ClearTime;

}