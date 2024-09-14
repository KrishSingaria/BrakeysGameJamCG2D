using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableItem : MonoBehaviour
{
    public Item item;
    BoxCollider BoxCollider;
    Rigidbody Rigidbody;
    int destroyed = 0;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && destroyed == 0)
        {
            destroyed = 1;
            InventryManagement.instance.AddItemInInventry(item);
            Destroy(gameObject);
        }
    }
}
