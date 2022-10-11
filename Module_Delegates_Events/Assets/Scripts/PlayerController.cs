using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float force = 100f;
    [SerializeField] float maxVelocity = 5f;
    [SerializeField] public float fireRate = 1f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] GameObject projectile;
    [SerializeField] KillCounting killCounting;


    float horizontalInput;
    float verticalInput;
    float timer;
    bool isDead;
    Vector2 direction;
    Rewards rewards;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        rewards = FindObjectOfType<Rewards>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInputs();
        FireProjectiles(projectileSpeed);
        timer += Time.deltaTime;

        if (timer > fireRate)
        {
            rewards.AfterPlayerAttack.Invoke();
        }

        if (isDead && Input.GetButtonDown("Jump"))
        {
            Time.timeScale = 1;
            killCounting.ResetCountKills();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void FireProjectiles(float projectileSpeed, float angleOffset = 0, bool bonus = false)
    {

        // (inconclusive) firing is done with Particle effect without timer (done with particleSysteme)
        // 
        if (timer > fireRate / 4)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
        if (timer > fireRate)
        {
            if (!bonus)
            {
                timer = 0;
            }

            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            for (int i = 0; i < 5; i++)
            {
                GameObject proj = Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, i * 90 + angleOffset));
                if (bonus) proj.GetComponent<SpriteRenderer>().color = Color.magenta;
                proj.TryGetComponent<MoveForward>(out MoveForward moveForwardspeed);
                moveForwardspeed.speed = projectileSpeed;
                
            }
        }

    }

    private void GetMovementInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // normalized direction vector
        direction = new Vector2(horizontalInput, verticalInput).normalized * force;

        // clamping velocity with maxVelocity parameter and modifying linear drag for better deceleration
        if (direction.magnitude == 0) playerRb.drag = 100f;
        else playerRb.drag = 50f;
        playerRb.velocity = new Vector2(Mathf.Clamp(direction.x, -1, 1), Mathf.Clamp(direction.y, -1, 1)).normalized * maxVelocity;

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // Addforce on Rigidbody, velocity is clamped and drag is modified
        playerRb.AddForce(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    void Die()
    {
        Time.timeScale = 0;
        isDead = true;
    }

}
