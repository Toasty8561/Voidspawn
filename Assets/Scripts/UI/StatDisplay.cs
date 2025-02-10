using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatDisplay : MonoBehaviour
{
    public Image healthBarFill, xpBarFill;
    public TMP_Text healthText, damageText, attackSpeedText, moveSpeedText, goldText,
                                critChanceText, levelText;
    public PlayerHealth health;
    public PlayerAttack attack;
    public PlayerMovement playerMovement;
    public Inventory inventory;
    public PlayerXP playerXP;

    public Image[] itemSlots;

    void Update()
    {
        healthBarFill.fillAmount = health.currentHealth / health.maxHealth;
        xpBarFill.fillAmount = (float)playerXP.xp / playerXP.xpToLevel;
        healthText.text = health.currentHealth + " / " + health.maxHealth + " (+" + health.regeneration + ")";
        damageText.text = "Attack Damage: " + attack.attackDamage.ToString();
        attackSpeedText.text = "Attack Speed: " + attack.attackSpeed.ToString();
        moveSpeedText.text = "Move Speed: " + playerMovement.speed.ToString();
        critChanceText.text = "Crit Chance: " + attack.critChance.ToString() + "%";
        goldText.text = inventory.gold.ToString() + "g";
        levelText.text = playerXP.level.ToString();

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < inventory.items.Count && inventory.items[i].icon != null)
            {                
                itemSlots[i].sprite = inventory.items[i].icon;
            }
            else
            {
                itemSlots[i].sprite = null;
            }
        }
    }
}
