using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{

    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject gameOverPanel;
    public GameObject WeaponPanel;

    public Text maxScore;
    public Text scoreText;
    public Text itemTalkText;
    public Text weaponTalkText;

    public RectTransform ItemShopUI;
    public RectTransform WeaponShopUI;
    public GameObject menuCam { get; set; }
    public GameObject gameCam { get; set; }

    public Text bestScoreText;

    public void UIGameStart()
    {
        menuCam.SetActive(false);//UI
        gameCam.SetActive(true);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }
    public void UIGameOver()//UI
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        int maxScore = PlayerPrefs.GetInt("MaxScore");
        bestScoreText.text = string.Format("{0:n0}", maxScore);

        if (GenericSinglngton<GameManager>.Instance.score > maxScore)
        {
            bestScoreText.gameObject.SetActive(true);
            PlayerPrefs.SetInt("MaxScore", GenericSinglngton<GameManager>.Instance.score);
        }
    }
    
    public void BaseSet()
    {
        maxScore.text = string.Format("{0:n0}", PlayerPrefs.GetInt("MaxScore"));
        if (PlayerPrefs.HasKey("MaxScore"))
        {
            PlayerPrefs.SetInt("MaxScore", 0);
        }
    }
    public void TextCH(Text CHText, string instring)
    {
        if(CHText!=null)
        CHText.text = instring;
    }
}
