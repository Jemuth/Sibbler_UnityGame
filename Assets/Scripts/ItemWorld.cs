using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ItemWorld : MonoBehaviour
{
    
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }
    private Item item;
    private MeshFilter meshRenderer;
    private TextMeshPro textMeshPro;

    public void Awake()
    {
        meshRenderer = GetComponent<MeshFilter>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }
    public void SetItem(Item item)
    {
        this.item = item;
        meshRenderer.mesh = item.GetMesh();
        if (item.amount > 1) 
        {
            textMeshPro.SetText(item.amount.ToString());
        } else
        {
            textMeshPro.SetText(""); 
        }
          
    }  
    public Item GetItem()
    {
        return item;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
