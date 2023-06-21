using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Bat,
        Ball,
        Key,
    }
    public ItemType itemType;
    public int amount;

    public Sprite GetSprite() { 

        switch (itemType) {
        
            default:
            case ItemType.Bat:      return ItemAssets.Instance.batSprite;
            case ItemType.Ball:     return ItemAssets.Instance.ballSprite;
            case ItemType.Key:      return ItemAssets.Instance.keySprite;
        }
    }
    public Mesh GetMesh()
    {

        switch (itemType)
        {

            default:
            case ItemType.Bat: return ItemAssets.Instance.batMesh;
            case ItemType.Ball: return ItemAssets.Instance.ballMesh;
            case ItemType.Key: return ItemAssets.Instance.keyMesh;
        }
    }
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Bat:
            case ItemType.Ball:
            case ItemType.Key:
            return true;
        }
    }
}
