using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float aggroRange;
    private NavMeshAgent navMeshAgent;
    private GameObject player;
    private EnemyHealth health;
    private Rigidbody2D rb;

    void Start()
    {
        // setup
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        player = GameObject.Find("Player");
        health = GetComponent<EnemyHealth>();
        navMeshAgent.speed = speed;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // move towards the player when attacked
        // or the player is close enough
        if (player != null)
        {
            if (PlayerIsInRange() || health.beingAttacked)
            {
                navMeshAgent.SetDestination(player.transform.position);

                // rotate towards the player
                Vector3 direction = player.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
                rb.rotation = angle;
            }
        }  
    }

    bool PlayerIsInRange()
    {
        if (player != null)
        {
            return Vector2.Distance(transform.position, player.transform.position) < aggroRange;
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
