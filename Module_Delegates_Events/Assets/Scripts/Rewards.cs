using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField] Button[] bonusButton;

    public MyEventWithParameter AfterEnemyDeath;
    public UnityEvent AfterPlayerAttack;

    string[] bonusText = {

            "Double Attack -/- +5 % chance of extra bullet",
            "Snowball effect -/- Enemy fire bullet after death"
        };

    public delegate void bonusButtonMethods();
    public UnityAction[] AddButtonListener = new UnityAction[2];

    private void Start()
    {
        AddButtonListener = new UnityAction[2] { SelectedDoubleAttack, SelectedBonusBullet };
    }

    void BonusBullet(Vector3 enemyPos)
    {
        GameObject proj = Instantiate(projectile, enemyPos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        proj.TryGetComponent<ProjectileMoveForward>(out ProjectileMoveForward moveForwardspeed);
        moveForwardspeed.ChangeColor(Color.yellow);
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
        int x = 0;
        int y = 0;
        while (x == y)
        {
            x = Random.Range(0, bonusText.Length);
            y = Random.Range(0, bonusText.Length);
        }

        bonusButton[0].onClick.RemoveAllListeners();
        bonusButton[0].GetComponentInChildren<TextMeshProUGUI>().text = bonusText[x];
        bonusButton[0].onClick.AddListener(AddButtonListener[x]);
        bonusButton[1].onClick.RemoveAllListeners();
        bonusButton[1].GetComponentInChildren<TextMeshProUGUI>().text = bonusText[y];
        bonusButton[1].onClick.AddListener(AddButtonListener[y]);

    }
}
