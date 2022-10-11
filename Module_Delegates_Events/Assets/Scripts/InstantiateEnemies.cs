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

    int nbEnemiesLvl = 1;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1/(spawnRate*maxEnemiesLvl))
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

        if (nbEnemiesLvl < maxEnemiesLvl)
        {
            Vector2 randomPos = Random.insideUnitCircle;
            Vector2 spawnpos = (randomPos - Vector2.zero).normalized * Random.Range(21f, 25f) + randomPos;

            Instantiate(enemies1, spawnpos, Quaternion.identity, transform);
            nbEnemiesLvl++;
        }
    }


}
