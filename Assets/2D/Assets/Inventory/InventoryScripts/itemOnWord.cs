using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOnWord : MonoBehaviour
{
    public Item thisItem;
    public Inventory PlayerInventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }

    }
    private void AddNewItem()
    {
        if (!PlayerInventory.itemList.Contains(thisItem))
        {
            PlayerInventory.itemList.Add(thisItem);
            //InventoryManager.CreateNewItem(thisItem);
        }
        else
        {
            thisItem.itemHeld++;
        }
        InventoryManager.RefreshItem();
    }
}
