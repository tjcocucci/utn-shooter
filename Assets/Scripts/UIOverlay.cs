using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlay : MonoBehaviour
{
    private Player player;
    private float healthPercent;
    public Text UIText;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        healthPercent = 0;
        if (player != null)
        {
            healthPercent = 100 * player.health / player.totalHealth;
        }
        UIText.text =
            "Health: " + healthPercent.ToString("0.00") + "%" + "\n" +
            "Weapon: " + player.weaponList[player.weaponIndex-1].weaponName + "\n" +
            "Time" + Time.time.ToString("0.00") + "\n";
    }
}
