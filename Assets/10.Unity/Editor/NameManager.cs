using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class NameManager : EditorWindow
{
    public GameObject[] targetObjects;
    string replaceName, removeKey, replaceKey, targetReplaceKey, linerKeyR, linerKeyL;
    Transform trn;
    NameSettingPreset preset;

    [MenuItem("Win/CustonWindow/NameManager")]
    static void open()
    {
        var window = GetWindow<NameManager>();
    }

    private void OnGUI()
    {
        GUILayout.Space(10);
        GUILayout.Label("NameManger", EditorStyles.boldLabel);
        GUILayout.Space(10);
        preset = (NameSettingPreset)EditorGUILayout.EnumPopup("Primitive to create:", preset);
        GUILayout.Space(5);
        switch (preset)
        {
            case NameSettingPreset.JustSetNameDirection:
            case NameSettingPreset.SetNameByIndex:
            case NameSettingPreset.SetNameByParentChild:
                replaceName = EditorGUILayout.TextField("Name : ", replaceName);
                break;
            case NameSettingPreset.Replace:
                replaceKey = EditorGUILayout.TextField("ReplaceKey : ", replaceKey);
                targetReplaceKey = EditorGUILayout.TextField("To : ", targetReplaceKey);
                break;
            case NameSettingPreset.Remove:
                removeKey = EditorGUILayout.TextField("RemoveKey : ", removeKey);
                break;
            case NameSettingPreset.AddLiner:
                linerKeyL = EditorGUILayout.TextField("LinerKeyLeft  : ", linerKeyL);
                linerKeyL = EditorGUILayout.TextField("LinerKeyRight : ", linerKeyR);
                break;
            default:
                break;
        }
        if (GUILayout.Button("Generate"))
        {
            int index = 1;
            foreach (GameObject obj in targetObjects)
            {
                switch (preset)
                {
                    case NameSettingPreset.JustSetNameDirection:
                        obj.name = replaceName;
                        break;
                    case NameSettingPreset.SetNameByIndex:
                        obj.name = $"{replaceName} {index}";
                        break;
                    case NameSettingPreset.SetNameByParentChild:
                        if (obj.transform.parent != null)
                        {
                            if (obj.transform.parent != trn)
                            {
                                index = 1;
                            }
                            trn = obj.transform.parent;
                            obj.name = $"Child : {obj.transform.parent.name} {index}";
                        }
                        else
                        {
                            obj.name = replaceName;
                        }
                        break;
                    case NameSettingPreset.Replace:
                        obj.name = obj.name.Replace(replaceKey, targetReplaceKey);
                        break;
                    case NameSettingPreset.Remove:
                        obj.name = obj.name.Replace(removeKey, "");
                        break;
                    case NameSettingPreset.Up:
                        obj.name = obj.name.ToUpper();
                        break;
                    case NameSettingPreset.AddLiner:
                        if (linerKeyR == "")
                        {
                            linerKeyR = linerKeyL;
                        }
                        obj.name = $"{linerKeyL}{obj.name}{linerKeyR}";
                        break;
                    default:
                        break;
                }
                index++;
            }
        }
        if (GUILayout.Button("Reset")){
            targetObjects = new GameObject[0];
        }
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("targetObjects");

        EditorGUILayout.PropertyField(stringsProperty, true);
        so.ApplyModifiedProperties();
    }

    enum NameSettingPreset
    {
        JustSetNameDirection,
        SetNameByIndex,
        SetNameByParentChild,
        Replace,
        Remove,
        Up,
        AddLiner,
    }
}
