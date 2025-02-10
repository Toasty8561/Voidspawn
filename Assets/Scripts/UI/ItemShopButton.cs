using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopButton : MonoBehaviour
{
    public Item itemToBuy;
    public Color green, grey;
    private GameObject player;
    private Inventory inventory;

    void Start()
    {
        player = GameObject.Find("Player");
        inventory = player.GetComponent<Inventory>();
    }

    public void BuyItem()
    {
        if (inventory.gold >= itemToBuy.goldCost && !inventory.HasItem(itemToBuy))
        {
            inventory.gold -= itemToBuy.goldCost;
            inventory.AddItem(itemToBuy);
        }
    }

    void Update()
    {
        if (inventory.gold <= itemToBuy.goldCost)
        {
            GetComponent<UnityEngine.UI.Image>().color = grey;
            return;
        }
        if (inventory.HasItem(itemToBuy))
        {
            GetComponent<UnityEngine.UI.Image>().color = grey;
            return;
        }
        GetComponent<UnityEngine.UI.Image>().color = green;

    }
}
