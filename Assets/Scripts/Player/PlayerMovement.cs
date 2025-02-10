using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    [SerializeField] private GameObject moveArrow;
    private Transform target;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Rigidbody2D rb;
    private PlayerAttack playerAttack;
    private Animator animator;

    void Start()
    {
        // setup
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = speed;
        rb = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        navMeshAgent.speed = speed;
        // the player can move freely
        // when no target is selected
        if (target == null)
        {
            navMeshAgent.isStopped = false; 
            animator.SetBool("Attacking", false);
        }        
        // gets mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Fire2"))
        {
            // moves towards the target
            navMeshAgent.SetDestination(mousePos);
            RotateTowards(navMeshAgent.destination);

            // move indicator
            moveArrow.transform.position = mousePos;
        }

        // check if the player is not moving
        if (navMeshAgent.velocity.magnitude == 0)
        {
            animator.SetBool("Walking", false);
        }
        else
        {
            animator.SetBool("Walking", true);
        }
    }

    public void RotateTowards(Vector2 target)
    {
        Vector2 lookDirection = target - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    // stop moving when attacking
    public void StopMoving()
    { 
        navMeshAgent.isStopped = true;
        animator.SetBool("Walking", false);
    }
}
