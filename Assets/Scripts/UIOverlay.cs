using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIOverlay : MonoBehaviour
{
    public Player player;

    public Banner banner;
    private float healthPercent;
    public Text UIText;
    private LevelManager levelManager;
    public EnemyManager enemyManager;
    private bool pauseUpdates;

    void Start()
    {
        levelManager = LevelManager.Instance;
        player.OnObjectDied += OnPlayerDeath;
        levelManager.OnWin += OnWin;
    }

    void Update()
    {
        healthPercent = 0;
        if (player != null)
        {
            healthPercent = 100 * player.health / player.totalHealth;
        }
        if (pauseUpdates)
        {
            return;
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

    public void OnPlayerDeath()
    {
        pauseUpdates = true;
        banner.SetText("You died!");
        banner.ShowBanner();

    }

    public void OnWin()
    {
        pauseUpdates = true;
        banner.SetText("You win!");
        banner.ShowBanner();
    }
}
