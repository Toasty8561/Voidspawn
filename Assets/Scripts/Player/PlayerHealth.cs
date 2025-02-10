using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public float regeneration;

    void Start()
    {
        currentHealth = maxHealth;
        StartCoroutine(Regenerate());
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            TakeDamage(10);
        }
    }

    override protected void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator Regenerate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Heal(regeneration);
        }
    }
}
