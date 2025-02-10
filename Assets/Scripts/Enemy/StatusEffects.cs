using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    private Health health;

    void Start()
    {
        health = GetComponent<Health>();
    }

    public void ApplyBurn(float damage, int ticks)
    {
        StartCoroutine(Burn(damage, ticks));
    }

    IEnumerator Burn(float damage, int ticks)
    {
        int currentTicks = ticks;
        while (currentTicks > 0)
        {
            health.TakeDamage(damage / 4f);
            yield return new WaitForSeconds(0.25f);
            currentTicks--;
        }
    }
}
