using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int startingGold;
    public int goldPerSecond;
    public int gold;
    public List<Item> items = new List<Item>();
    private const int MAX_ITEMS = 6;
    public bool[] itemsBought;

    private bool hasDice;

    private void Start()
    {
        gold = startingGold;
        StartCoroutine(PassiveGold());
    }

    public void AddItem(Item item)
    {
        if (items.Count < MAX_ITEMS)
        {
            items.Add(item);
            item.ApplyItem(gameObject);
            if (item.name == "Lucky Dice")
            {
                hasDice = true;
            }
        }
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        item.RemoveItem(gameObject);
        if (item.name == "Lucky Dice")
        {
            hasDice = false;
        }
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gold += 1000;
        }
    }

    private IEnumerator PassiveGold()
    {
        while (true)
        {
            gold += hasDice ? Random.Range(1,6) : goldPerSecond;
            yield return new WaitForSeconds(1f);
        }
    }
}
