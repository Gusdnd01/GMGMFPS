using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngineInternal;

public class MyGizmo : MonoBehaviour
{
#if UNITY_EDITOR
    [Tooltip("OnSelected : 오브젝트가 선택되었을떄 그려줌\nAlways : 항상 그려줌")]
    [SerializeField] private GizmoDrawMode drawMode;
    [Tooltip("Wire(선) 형식으로 그릴지\nMesh(꽉찬) 형식으로 그릴지")]
    [SerializeField] private GizmoMode gizmoMode;
    [Tooltip("어떤 모양으로 그릴지")]
    [SerializeField] private GizmoMeshType meshType;

    [Space(15)]
    [SerializeField] private Color color = Color.red;
    [SerializeField] private Vector3 center = Vector3.zero;
    [SerializeField] private float size = 1;

    public void OnDrawGizmos()
    {
        if(drawMode == GizmoDrawMode.OnSelected)
        {
            if (Selection.activeTransform != transform)
                return;
        }

        Gizmos.color = color;

        switch (gizmoMode)
        {
            case GizmoMode.Wire:
                switch (meshType)
                {
                    case GizmoMeshType.Circle:
                        Gizmos.DrawWireSphere(center, size);
                        break;
                    case GizmoMeshType.Box:
                        Gizmos.DrawWireCube(center, Vector3.one * size);
                        break;
                }
                break;
            case GizmoMode.Mesh:
                switch (meshType)
                {
                    case GizmoMeshType.Circle:
                        Gizmos.DrawSphere(center, size);
                        break;
                    case GizmoMeshType.Box:
                        Gizmos.DrawCube(center, Vector3.one * size);
                        break;
                }
                break;
        }
    }

    enum GizmoMeshType
    {
        Circle,
        Box,
    }
    enum GizmoMode
    {
        Wire,
        Mesh
    }
    enum GizmoDrawMode
    {
        OnSelected,
        Always
    }
#endif
}
