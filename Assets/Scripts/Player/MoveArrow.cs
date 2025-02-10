using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArrow : MonoBehaviour
{
    public static MoveArrow instance;
    private Vector3 originalPosition;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MoveOffScreen();
        }
    }

    public void MoveOffScreen()
    {
        transform.position = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z);
    }
}
