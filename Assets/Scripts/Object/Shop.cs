using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : Interactable
{
    [HideInInspector] public TMP_Text text;
    [HideInInspector] public GameObject shopUI;
    public enum ShopType { Random, Selected }
    public ShopType shopType;

    // Every item in the game
    public List<Item> allItems;
    // Items in the shop
    public List<Item> availableItems;

    public Image[] itemIcons;
    public TMP_Text[] itemNames;
    public TMP_Text[] itemDescriptions;
    public TMP_Text[] goldText;
    public Button[] buyButtons;

    private bool shopOpened;
    private Inventory inventory;


    void Start()
    {
        inventory = player.GetComponent<Inventory>();

        if (shopType == ShopType.Random)
        {
            StockItems();
        }
    }

    void Update()
    {
        text.text = IsClose() ? "Open Shop [" + key + "]" : "";

        if (IsClose())
        {
            if (Input.GetKeyDown(key) || Input.GetKeyDown(KeyCode.Escape))
            {
                Interact();
            }
        }
        
        if (shopOpened && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseShop();
        }
    }

    protected override void Interact()
    {
        if (shopOpened)
            CloseShop();
        else
            OpenShop();
    }

    public void OpenShop()
    {
        for (int i = 0; i < 3; i++)
        {
            itemIcons[i].sprite = availableItems[i].icon;
            itemNames[i].text = availableItems[i].itemName;
            itemDescriptions[i].text = availableItems[i].description;
            goldText[i].text = availableItems[i].goldCost.ToString() + "g";
            buyButtons[i].gameObject.GetComponent<ItemShopButton>().itemToBuy = availableItems[i];
        }
        shopOpened = true;
        shopUI.SetActive(true);
    }

    public void CloseShop()
    {
        shopOpened = false;
        shopUI.SetActive(false);
    }

    void StockItems()
    {
        // Clear the available items list
        availableItems.Clear();

        // Generate 3 random unique items
        while (availableItems.Count < 3)
        {
            // Get a random item from all items
            Item randomItem = allItems[Random.Range(0, allItems.Count)];
            // If the item is not already in the shop and the player doesn't have it, add it to the shop
            if (!availableItems.Contains(randomItem) && !inventory.HasItem(randomItem))
            {
                availableItems.Add(randomItem);
            }
        }
    }
}
