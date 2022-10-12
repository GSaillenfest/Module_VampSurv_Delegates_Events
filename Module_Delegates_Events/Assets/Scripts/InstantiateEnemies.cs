using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InstantiateEnemies : MonoBehaviour
{
    [SerializeField] int maxEnemiesLvl;
    [SerializeField] GameObject enemies1;
    [SerializeField] float spawnRate = 1f;
    [SerializeField] BoxCollider2D upBox;
    [SerializeField] BoxCollider2D downBox;
    [SerializeField] BoxCollider2D leftBox;
    [SerializeField] BoxCollider2D rightBox;

    int nbEnemiesLvl = 1;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1 / (spawnRate * maxEnemiesLvl))
        {
            timer = 0;
            Spawn();
        }
        if (transform.childCount == 0 && nbEnemiesLvl != 0)
        {
            maxEnemiesLvl++;
            nbEnemiesLvl = 0;
        }
    }

    private void OnEnable()
    {
        timer = 0;
    }

    public void Spawn()
    {
        // instantiating in box colliders (from each edges of the screen)
        if (nbEnemiesLvl < maxEnemiesLvl)
        {
            int i = Random.Range(0, 4);
            Vector2 randomPos;

            switch (i)
            {
                case 0:
                    randomPos = RandomInsideBoxCollider(upBox);
                    break;
                case 1:
                    randomPos = RandomInsideBoxCollider(downBox);
                    break;
                case 2:
                    randomPos = RandomInsideBoxCollider(leftBox);
                    break;
                case 3:
                    randomPos = RandomInsideBoxCollider(rightBox);
                    break;
                default:
                    randomPos = Vector2.one * 25f;
                    break;
            }


            Instantiate(enemies1, randomPos, Quaternion.identity, transform);
            nbEnemiesLvl++;
        }
    }

    // random position inside a box collider
    Vector3 RandomInsideBoxCollider(BoxCollider2D boxCollider2D)
    {
        Vector3 randomPoint = new Vector3
            (
                Random.Range(-boxCollider2D.size.x / 2, boxCollider2D.size.x / 2),
                Random.Range(-boxCollider2D.size.y / 2, boxCollider2D.size.y / 2)
            )
            + boxCollider2D.transform.position;
        return randomPoint;

    }


}
