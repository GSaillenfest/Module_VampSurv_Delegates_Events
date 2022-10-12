using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "KillCount", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]

// scriptable object
public class KillCounting : ScriptableObject
{
    public AnimationCurve rewardCurve;
    TextMeshProUGUI killCountTMP;
    int killCount;
    int threshold;
    int level;

    // killcount can only be incremented by this script
    public int KillCount { get => killCount; private set => killCount = value; }

    void OnEnable()
    {
        level = 0;
        killCount = 0;
        threshold = 5;
    }

    public void CountKills()
    {
        threshold = Mathf.RoundToInt(rewardCurve.Evaluate(level));
        KillCount++;
        killCountTMP = GameObject.Find("KillCount").GetComponent<TextMeshProUGUI>();
        killCountTMP.text = "Kills : " + KillCount.ToString();

        if (killCount % threshold == 0)
        {
            FindObjectOfType<Rewards>().BonusMenu();
            level++;
        }
    }

    public void ResetCountKills()
    {
        KillCount = 0;
        level = 0;
    }
}
