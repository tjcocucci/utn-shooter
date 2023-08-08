using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject gameContainer;
    public GameObject tutorialUI;
    public GameObject mainMenuItems;
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
        Invoke("QuitGame", 0.5f);
    }
    
    public void QuitGame() {
        Application.Quit();
    }

    public void OnTutorialButtonClicked () {
        buttonClickSound.Play();
        Invoke("SwitchToTutorial", 0.5f);
    }

    public void SwitchToTutorial () {
        tutorialUI.SetActive(true);
        mainMenuItems.SetActive(false);
    }

}
