using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] float lifeTime;
    public float speed;
    float timer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (timer > lifeTime) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
