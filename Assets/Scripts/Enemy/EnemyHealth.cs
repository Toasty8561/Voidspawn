using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [HideInInspector] public bool selected, beingAttacked;
    [HideInInspector]public SpriteRenderer spriteRenderer;
    [HideInInspector] public Sprite target;
    public int goldDrop, xpDrop;
    [HideInInspector] public GameObject goldTextPopup;

    private Collider2D objectCollider;
    private GameObject player;
    private PlayerAttack playerAttack;
    private Inventory inventory;
    private PlayerXP playerXP;

    void Start()
    {
        // setup
        objectCollider = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerAttack = player.GetComponent<PlayerAttack>();
            playerXP = player.GetComponent<PlayerXP>();
            inventory = player.GetComponent<Inventory>();
        }
       
        currentHealth = maxHealth;
    }

    void Update()
    {
        // marks enemy as selected if right clicked
        if (Input.GetButtonDown("Fire2"))
        {
            selected = IsMouseOver();
        }

        // set the target to the selected enemy
        // handles target ui for selected enemy
        if (selected)
        {
            spriteRenderer.sprite = target;
            playerAttack.target = gameObject;
            MoveArrow.instance.MoveOffScreen();
        }
        else
        {
            spriteRenderer.sprite = null;
        }

        // if no enemies are selected, set the target to null
        if (!EnemyIsSelected())
        {
            if (playerAttack != null)
            {
                playerAttack.target = null;
                beingAttacked = false;
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // marks the enemy as being attacked when it takes damage
    public override void TakeDamage(float amount)
    {
        beingAttacked = true;
        if (currentHealth <= 0)
        {
            Die();
        }
        currentHealth -= amount;
        GameObject number = Instantiate(damageNumber, transform.position, Quaternion.identity);
        number.GetComponent<DamageNumberPopup>().Setup(amount.ToString());
    }

    protected override void Die()
    {
        Destroy(gameObject);
        inventory.gold += goldDrop;
        playerXP.AddXP(xpDrop);
        GameObject goldText = Instantiate(goldTextPopup, transform.position, Quaternion.identity);
        goldText.GetComponent<DamageNumberPopup>().Setup("+" + goldDrop.ToString() + "g");
    }

    bool EnemyIsSelected()
    {
        // check if any enemy is selected
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemy in enemies)
        {
            if (enemy.selected)
            {
                return true;
            }
        }
        return false;
    }

    bool IsMouseOver()
    {
        // check if the mouse is over the enemy
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (objectCollider != null && objectCollider.OverlapPoint(mousePosition))
        {
            return true;
        }
        return false;
    }

}
