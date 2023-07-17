using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlay : MonoBehaviour
{
    public Player player;

    private float healthPercent;
    public Text UIText;
    private LevelManager levelManager;
    public EnemyManager enemyManager;
    private bool won;
    private bool lost;

    void Start()
    {
        levelManager = LevelManager.Instance;
    }

    void Update()
    {
        healthPercent = 0;
        if (player != null)
        {
            healthPercent = 100 * player.health / player.totalHealth;
        }
        won =
            levelManager.currentLevelIndex == levelManager.levels.Length - 1
            && player != null
            && player.health > 0
            && enemyManager.enemyKills == levelManager.currentLevel.totalNumberOfEnemies;
        lost = player == null;
        if (won)
        {
            UIText.text = "You win!";
        }
        else if (lost)
        {
            UIText.text = "You lose!";
        }
        else
        {
            UpdateText();
        }
    }

    void UpdateText()
    {
        UIText.text =
            "Health: "
            + healthPercent.ToString("0")
            + "%"
            + "\n"
            + "Weapon: "
            + player.weaponList[player.weaponIndex - 1].weaponName
            + "\n"
            + "Level: "
            + (levelManager.currentLevelIndex + 1)
            + "\n"
            + "Kills: "
            + enemyManager.enemyKills
            + "\n"
            + "Time: "
            + Time.time.ToString("0.00")
            + "\n";
    }
}
