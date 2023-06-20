using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    Player1 player1;

    private void Start()
    {
        player1 = GetComponent<Player1>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //if(other.CompareTag("P1Collider"))
        //{
        //    // Hay colision Debug.Log("In the item");
            
        //    if(player1.inventory.HasItem(item))
        //    {
        //        item.quantity++;
        //        Destroy(gameObject);
        //    } else
        //    {
        //        player1.inventory.AddItem(item);
        //        Destroy(gameObject);
        //        Debug.Log("In the item");
        //    }
            
        //}
    }
}
