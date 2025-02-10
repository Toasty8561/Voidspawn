using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAbilities : MonoBehaviour
{
    public float QCooldown, WCooldown, ECooldown, RCooldown;
    private float QTimeLastUsed, WTimeLastUsed, ETimeLastUsed, RTimeLastUsed;

    void Update()
    {
        if (Input.GetKeyDown("q") && Time.time >= QTimeLastUsed + QCooldown)
        {
            QAbility();
            QTimeLastUsed = Time.time;
        }
        if (Input.GetKeyDown("w") && Time.time >= WTimeLastUsed + WCooldown)
        {
            WAbility();
            WTimeLastUsed = Time.time;
        }
        if (Input.GetKeyDown("e") && Time.time >= ETimeLastUsed + ECooldown)
        {
            EAbility();
            ETimeLastUsed = Time.time;
        }
        if (Input.GetKeyDown("r") && Time.time >= RTimeLastUsed + RCooldown)
        {
            RAbility();
            RTimeLastUsed = Time.time;
        }
    }

    protected abstract void QAbility();
    protected abstract void WAbility();
    protected abstract void EAbility();
    protected abstract void RAbility();
}
