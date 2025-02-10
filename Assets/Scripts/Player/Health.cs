using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public float currentHealth, maxHealth;
    [HideInInspector]public GameObject damageNumber;

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        GameObject number = Instantiate(damageNumber, transform.position, Quaternion.identity);
        number.GetComponent<DamageNumberPopup>().Setup(damage.ToString());
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
