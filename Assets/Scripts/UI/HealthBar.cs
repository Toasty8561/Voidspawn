using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill, healthBar;
    public Health health;
    public Transform target;
    public Vector3 offset;
    private Camera cam;
    

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        healthBar.gameObject.transform.rotation = cam.transform.rotation;
        healthBar.gameObject.transform.position = target.position + offset;
        fill.gameObject.transform.position = target.position + offset;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (fill != null) { fill.fillAmount = health.currentHealth / health.maxHealth; }
    }
}
