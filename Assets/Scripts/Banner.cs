using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Banner : MonoBehaviour
{
    public TextMeshProUGUI bannerText;

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

}
