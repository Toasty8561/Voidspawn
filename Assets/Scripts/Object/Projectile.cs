using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float speed;
    public GameObject critText;
    public GameObject target;
    private Rigidbody2D rb;
    public ProjectileType projectileType;

    public enum ProjectileType
    {
        LockOn,
        SkillShot
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (target != null)
        {
            // moves and rotates towards the target
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            RotateTowards(target.transform.position);
            // tracks the distance from the target
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance <= 0.1f)
            {
                // rolls for a crit and applies double damage
                bool isCrit = Random.Range(0, 100) < PlayerAttack.instance.critChance;
                damage = isCrit ? damage * 2f : damage;
                // deals damage and applies on hit effects
                target.GetComponent<Health>().TakeDamage(damage);

                if (isCrit)
                {
                    GameObject text = Instantiate(critText, target.transform.position, Quaternion.identity);
                    text.GetComponent<DamageNumberPopup>().Setup("Crit!");
                }

                ApplyEffects();
                Destroy(gameObject);
            }
        }
        // if there is no target, destroy object
        else if (projectileType == ProjectileType.LockOn)
        {
            Destroy(gameObject);
        }
    }

    void RotateTowards(Vector2 target)
    {
        Vector2 lookDirection = target - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    protected virtual void ApplyEffects() { }
}
