using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[AddComponentMenu("ColorHierarchy/ColorHierarchy")]
public class ColorHierarchy : MonoBehaviour
{
    //기본값 : 회색
    //빨간색 : 별로 안건들이는거
    //파란색 : Player
    //초록색 : 배치형 오브젝트
    //빛 관련 : 노란색

    private static Dictionary<object, ColorHierarchy> coloredObjects = new Dictionary<object, ColorHierarchy>();

    public string prefix = "#";
    public Color backColor = new Color(100, 100, 100, 255);
    public Color fontColor = new Color(255, 255, 255, 255);

    static ColorHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleDraw;
    }

    private static void HandleDraw(int id, Rect selectionRect)
    {
        Object obj = EditorUtility.InstanceIDToObject(id);

        if(obj != null && coloredObjects.ContainsKey(obj))
        {
            GameObject gObj = obj as GameObject;
            ColorHierarchy ch = gObj.GetComponent<ColorHierarchy>();
            if(ch != null)
            {
                PaintObject(obj, selectionRect, ch);
            }
            else
            {
                coloredObjects.Remove(obj);
            }
        }
    }

    private static void PaintObject(Object obj, Rect selectionRect, ColorHierarchy ch)
    {
        Rect bgRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width + 50, selectionRect.height);

        if(Selection.activeGameObject != obj)
        {
            EditorGUI.DrawRect(bgRect, ch.backColor);
            string name = $"{ch.prefix} {obj.name}";

            EditorGUI.LabelField(bgRect, name, new GUIStyle()
            {
                normal = new GUIStyleState() { textColor = ch.fontColor },
                fontStyle = FontStyle.Bold
            });
        }
    }

    private void Reset()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        if(coloredObjects.ContainsKey(this.gameObject) == false)
        {
            coloredObjects.Add(this.gameObject, this);
        }
    }
}
