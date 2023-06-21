using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
   public static ItemAssets Instance {  get; private set; }
   private void Awake()
    {
        Instance = this;
    }
    public Transform pfItemWorld;
    public Sprite batSprite;
    public Sprite ballSprite;
    public Sprite keySprite;
    public Mesh batMesh;
    public Mesh ballMesh;
    public Mesh keyMesh;
}
