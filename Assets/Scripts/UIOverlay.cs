using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIOverlay : MonoBehaviour
{
    private Player player;

    public Banner banner;
    private float healthPercent;
    public Text UIText;
    private LevelManager levelManager;
    private bool pauseUpdates;

    void Start()
    {
        player = FindObjectOfType<Player>();
        levelManager = LevelManager.Instance;
        player.OnObjectDied += OnPlayerDeath;
        levelManager.OnWin += OnWin;
    }

    void OnEnable()
    {
        pauseUpdates = false;
        player = FindObjectOfType<Player>();
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
            + EnemyManager.Instance.enemyKills
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
