using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/ItemData")]
public class ItemData : ScriptableObject
{
    public List<GameObject> items = new List<GameObject>();

    public int Data;
}
