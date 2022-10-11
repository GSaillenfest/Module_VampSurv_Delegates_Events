using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "KillCount", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]

public class KillCounting : ScriptableObject
{
    TextMeshProUGUI killCountTMP;
    int killCount;

    public int KillCount { get => killCount; private set => killCount = value; }

    void OnEnable()
    {
        killCount = 0;
    }

    public void CountKills()
    {
        KillCount++;
        killCountTMP = GameObject.Find("KillCount").GetComponent<TextMeshProUGUI>();
        killCountTMP.text = "Kills : " + KillCount.ToString();

        if (killCount % 5 == 0)
        {
            FindObjectOfType<Rewards>().BonusMenu();
        }
    }

    public void ResetCountKills()
    {
        KillCount = 0;
    }
}
