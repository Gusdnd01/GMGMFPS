using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ExampleWindow : EditorWindow
{
    Color color;

    [MenuItem("Window/Colorizer")]
    public static void ShowWindow()
    {
        GetWindow<ExampleWindow>("Colorizer");
    }

    private void OnGUI()
    {
        GUILayout.Label("coloring object", EditorStyles.boldLabel);
        color = EditorGUILayout.ColorField("Color", color);

        if (GUILayout.Button("Button"))
        {
            Colorize();
        }
    }

    void Colorize()
    {
        foreach (GameObject ga in Selection.gameObjects)
        {
            ga.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
