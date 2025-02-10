using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float distance;
    public string key;
    
    protected GameObject player;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    protected abstract void Interact();

    protected bool IsClose()
    {
        if (player != null)
        {
            return Vector3.Distance(player.transform.position, transform.position) <= distance;
        }
        return false;
    }
}
