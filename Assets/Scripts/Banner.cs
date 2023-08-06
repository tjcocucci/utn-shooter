using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Banner : MonoBehaviour
{
    public TextMeshProUGUI bannerText;
    public Player player;
    public AudioSource buttonClickSound;
    public GameObject gameContainer;
    public GameObject menuGameObject;

    public void ShowBanner()
    {
        LeanTween.moveLocalY(gameObject, 0, 0.5f).setEaseOutBounce();
    }

    public void SetText(string text)
    {
        bannerText.text = text;
    }

    public void HideBanner()
    {
        LeanTween.moveLocalY(gameObject, 400, 0.5f).setEaseInBounce();
    }

    public void OnPlayButtonClick()
    {
        buttonClickSound.Play();
        Invoke("SwitchToGame", 0.5f);
        HideBanner();
    }

    public void OnBackToMenuButtonClick()
    {
        buttonClickSound.Play();
        Invoke("SwitchToMenu", 0.5f);
        HideBanner();
    }

    void SwitchToMenu()
    {
        gameContainer.SetActive(false);
        menuGameObject.SetActive(true);
    }

    void SwitchToGame () {
        LevelManager.Instance.LoadLevel(0);
        HideBanner();
    }

}
