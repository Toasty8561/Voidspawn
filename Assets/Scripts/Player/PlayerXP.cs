using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    public int level = 1;
    const int MAX_LEVEL = 20;
    public int xp = 0;
    public int xpToLevel = 100;

    public enum StatType
    {
        AttackDamage,
        AttackSpeed,
        MaxHealth,
        MovementSpeed,
        HealthRegeneration,
        CritChance
    }

    [System.Serializable]
    public class StatsPerlevel
    {
        public StatType statType;
        public float value;
    }

    public List<StatsPerlevel> statsPerLevel = new List<StatsPerlevel>();

    void Update()
    {
        if (Input.GetKeyDown("2"))
        {
            AddXP(100);
        }
    }

    public void AddXP(int amount)
    {
        xp += amount;
        if (xp >= xpToLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        if (level >= MAX_LEVEL)
        {
            return;
        }
        ApplyStatBonus();
        level++;
        xp = 0;
        xpToLevel = 100 + (level * 50);
    }

    void ApplyStatBonus()
    {
        for (int i = 0; i < statsPerLevel.Count; i++)
        {
            switch (statsPerLevel[i].statType)
            {
                case StatType.AttackDamage:
                    GetComponent<PlayerAttack>().attackDamage += statsPerLevel[i].value;
                    break;
                case StatType.AttackSpeed:
                    GetComponent<PlayerAttack>().attackSpeed += statsPerLevel[i].value;
                    break;
                case StatType.MaxHealth:
                    GetComponent<PlayerHealth>().maxHealth += statsPerLevel[i].value;
                    GetComponent<PlayerHealth>().currentHealth += statsPerLevel[i].value;
                    break;
                case StatType.MovementSpeed:
                    GetComponent<PlayerMovement>().speed += statsPerLevel[i].value;
                    break;
                case StatType.HealthRegeneration:
                    GetComponent<PlayerHealth>().regeneration += statsPerLevel[i].value;
                    break;
                case StatType.CritChance:
                    GetComponent<PlayerAttack>().critChance += statsPerLevel[i].value;
                    break;
            }
        }
    }

}
