using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventoryItemData")]
public class InventoryItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite Icon;
    public GameObject prefab;
}
