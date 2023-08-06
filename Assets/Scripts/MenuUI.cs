using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject gameContainer;
    public AudioSource buttonClickSound;
    public void OnPlayButtonClicked () {
        buttonClickSound.Play();
        Invoke("SwitchToGame", 0.5f);
    }
    void SwitchToGame () {
        gameContainer.SetActive(true);
        LevelManager.Instance.LoadLevel(0);
        gameObject.SetActive(false);
    }

    public void OnQuitButtonClicked () {
        buttonClickSound.Play();
        Application.Quit();
    }
}
