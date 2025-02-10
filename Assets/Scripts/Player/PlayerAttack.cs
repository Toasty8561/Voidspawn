using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;
    public float attackDamage, attackSpeed, attackRange, critChance;
    public enum AttackType { Melee, Ranged }
    public AttackType attackType;
    public GameObject attackProjectile;
    public Transform rangedFirePoint;
    public GameObject target;
    
    private bool attacking;
    private PlayerMovement playerMovement;
    private Animator animator;



    void Awake()
    {
        instance = this;
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null)
        {
            if (TargetInRange())
            {
                playerMovement.StopMoving();
                if (!attacking)
                {
                    attacking = true;
                    //Attack();
                    StartCoroutine(StartAttacking());
                }
            }
        }
    }

    void Attack()
    {
        if (target != null && TargetInRange())
        {
            playerMovement.RotateTowards(target.transform.position);
            if (attackType == AttackType.Melee)
            {
                // melee attack
            }
            else if (attackType == AttackType.Ranged && attackProjectile != null)
            {
                // cancel walking animation
                animator.SetBool("Walking", false);
                animator.Play("Attack");

                // create projectile
                Projectile proj = Instantiate(attackProjectile, rangedFirePoint.position, rangedFirePoint.rotation).GetComponent<Projectile>();
                proj.target = target;
                proj.damage = attackDamage;
                
            }
        }
    }

    IEnumerator StartAttacking()
    {
        while (attacking)
        {
            Attack();
            yield return new WaitForSeconds(1f / attackSpeed);
            attacking = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    bool TargetInRange()
    {
        // 0.65 is added to the range to account for the enemy's collider radius
        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange + 0.65f)
        {
            return true;
        }
        return false;

    }
}
