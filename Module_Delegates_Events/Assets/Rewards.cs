using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]   
public class MyEventWithParameter : UnityEvent<Vector3>
{

}

public class Rewards : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] PlayerController playerController;
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject bonusMenu;

    public MyEventWithParameter AfterEnemyDeath;
    public UnityEvent AfterPlayerAttack;


    // Start is called before the first frame update
    void Start()
    {
    }

    void BonusBullet(Vector3 enemyPos)
    {
        GameObject proj = Instantiate(projectile, enemyPos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        proj.TryGetComponent<MoveForward>(out MoveForward moveForwardspeed);
        moveForwardspeed.speed = projectileSpeed;
    }

    void DoubleAttack()
    {
        bool tOrF = Random.Range(0f, 10f) <= 0.5f;
        if (tOrF)
        {
            Debug.Log("doubleAttack");
            playerController.FireProjectiles(10f, 45f, true);
        }
    }

    public void SelectedBonusBullet()
    {
        AfterEnemyDeath.AddListener(BonusBullet);
        Time.timeScale = 1;
        bonusMenu.SetActive(false);
    }

    public void SelectedDoubleAttack()
    {
        AfterPlayerAttack.AddListener(DoubleAttack);
        Time.timeScale = 1;
        bonusMenu.SetActive(false);
    }

    public void BonusMenu()
    {
        Time.timeScale = 0.01f;
        bonusMenu.SetActive(true);
    }
}
