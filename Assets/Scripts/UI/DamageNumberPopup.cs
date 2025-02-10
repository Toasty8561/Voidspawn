using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumberPopup : MonoBehaviour
{
    private TMP_Text text;
    private Rigidbody2D rb;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 1f);
    }

    void Start()
    {
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.velocity = randomDirection.normalized * 0.5f;
    }

    public void Setup(string displayText)
    {
        text.text = displayText.ToString();
    }
}
