using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoveForward : MonoBehaviour
{
    [SerializeField] float lifeTime;
    public float speed;
    float timer;

    private void Awake()
    {
        timer = 0;
    }

    void Update()
    {
        // linear movement
        timer += Time.deltaTime;
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (timer > lifeTime) Destroy(gameObject);
    }

    public void ProjectileSettings(Color color, float projSpeed = 10f, float projLifeTime = 5f)
    {
        GetComponent<SpriteRenderer>().color = color;
        var main = GetComponent<ParticleSystem>().main;
        main.startColor = GetComponent<SpriteRenderer>().color;
        lifeTime = projLifeTime;
        speed = projSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
