using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] float force = 50f;
    [SerializeField] float maxVelocity = 5f;
    [SerializeField] Rigidbody2D enemyRb;
    [SerializeField] Animator animator;

    Transform player;
    Vector2 direction;
    bool isHit;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        direction = force * (player.position - transform.position).normalized;

        if (direction.magnitude == 0) enemyRb.drag = 100f;
        else enemyRb.drag = 50f;
        enemyRb.velocity = new Vector2(Mathf.Clamp(direction.x, -1, 1), Mathf.Clamp(direction.y, -1, 1)).normalized * maxVelocity;
    }

    private void FixedUpdate()
    {
        if (!isHit)
        {
            Move();
        }
        else enemyRb.velocity = Vector2.zero;
    }

    void Move()
    {
        enemyRb.AddForce(direction);
    }

    void Die()
    {
        isHit = true;   
        animator.SetTrigger("Hit");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            Debug.Log("touché");
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Die();
        }
    }

}
