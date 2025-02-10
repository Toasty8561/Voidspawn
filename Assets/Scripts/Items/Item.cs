using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea(15, 20)]
    public string description;
    public int goldCost;
    public Sprite icon;
    public Tier tier;

    public enum StatType
    {
        AttackDamage,
        AttackSpeed,
        MaxHealth,
        MovementSpeed,
        HealthRegeneration,
        CritChance
    }

    public enum Tier
    {
        Starter,
        Common,
        Rare,
        Legendary
    }

    [System.Serializable]
    public class StatBonus
    {
        public StatType statType;
        public float value;
    }

    public List<StatBonus> itemStats = new List<StatBonus>();

    // loops through each stat bonus and applies it to the player
    public void ApplyItem(GameObject player)
    {
        for (int i = 0; i < itemStats.Count; i++)
        {
            switch (itemStats[i].statType)
            {
                case StatType.AttackDamage:
                    player.GetComponent<PlayerAttack>().attackDamage += itemStats[i].value;
                    break;
                case StatType.AttackSpeed:
                    player.GetComponent<PlayerAttack>().attackSpeed += itemStats[i].value;
                    break;
                case StatType.MaxHealth:
                    player.GetComponent<PlayerHealth>().maxHealth += itemStats[i].value;
                    player.GetComponent<PlayerHealth>().currentHealth += itemStats[i].value;
                    break;
                case StatType.MovementSpeed:
                    player.GetComponent<PlayerMovement>().speed += itemStats[i].value;
                    break;
                case StatType.HealthRegeneration:
                    player.GetComponent<PlayerHealth>().regeneration += itemStats[i].value;
                    break;
                case StatType.CritChance:
                    player.GetComponent<PlayerAttack>().critChance += itemStats[i].value;
                    break;
                default:
                    break;
            }
        }
    }

    public void RemoveItem(GameObject player)
    {
        for (int i = 0; i < itemStats.Count; i++)
        {
            switch (itemStats[i].statType)
            {
                case StatType.AttackDamage:
                    player.GetComponent<PlayerAttack>().attackDamage -= itemStats[i].value;
                    break;
                case StatType.AttackSpeed:
                    player.GetComponent<PlayerAttack>().attackSpeed -= itemStats[i].value;
                    break;
                case StatType.MaxHealth:
                    player.GetComponent<PlayerHealth>().maxHealth -= itemStats[i].value;
                    player.GetComponent<PlayerHealth>().currentHealth -= itemStats[i].value;
                    break;
                case StatType.MovementSpeed:
                    player.GetComponent<PlayerMovement>().speed -= itemStats[i].value;
                    break;
                case StatType.HealthRegeneration:
                    player.GetComponent<PlayerHealth>().regeneration -= itemStats[i].value;
                    break;
                case StatType.CritChance:
                    player.GetComponent<PlayerAttack>().critChance -= itemStats[i].value;
                    break;
                default:
                    break;
            }
        }
    }
}
