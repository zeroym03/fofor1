using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{

   // GameCamera gameCamera;

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

    PlayerUnit playerUnit;
    public Text bestScoreText;

    public void UIGameStart()
    {
        menuCam.SetActive(false);//UI
        gameCam.SetActive(true);
   //     gameCamera = gameCam.GetComponent<GameCamera>();
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
    private void LateUpdate()
    {

        //playHealtText.text = GenericSinglngton<GameManager>.Instance.playerUnit.health + " / " + GenericSinglngton<GameManager>.Instance.playerUnit.maxhealth;
        //playCoinText.text = string.Format("{0:n0}", GenericSinglngton<GameManager>.Instance.playerUnit.coin);
        //if (GenericSinglngton<GameManager>.Instance.playerUnit.equipWeapon == null)
        //{
        //    playAmmoText.text = "- / " + GenericSinglngton<GameManager>.Instance.playerUnit.ammo;
        //}
        //else if (GenericSinglngton<GameManager>.Instance.playerUnit.equipWeapon.type == Weapon.Type.Melee)
        //{
        //    playAmmoText.text = "- / " + GenericSinglngton<GameManager>.Instance.playerUnit.ammo;
        //}
        //else
        //{
        //    playAmmoText.text = GenericSinglngton<GameManager>.Instance.playerUnit.equipWeapon.curAmmo + " / " + GenericSinglngton<GameManager>.Instance.playerUnit.ammo;
        //}
        //weaponImage1.color = new Color(1, 1, 1, GenericSinglngton<GameManager>.Instance.playerUnit.hasWeapons[0] ? 1 : 0);
        //weaponImage2.color = new Color(1, 1, 1, GenericSinglngton<GameManager>.Instance.playerUnit.hasWeapons[1] ? 1 : 0);
        //weaponImage3.color = new Color(1, 1, 1, GenericSinglngton<GameManager>.Instance.playerUnit.hasWeapons[2] ? 1 : 0);
        //weaponImageR.color = new Color(1, 1, 1, GenericSinglngton<GameManager>.Instance.playerUnit.hasGreandes > 0 ? 1 : 0);
        //enemyTextA.text = GenericSinglngton<GameManager>.Instance.enemyCntA.ToString();
        //enemyTextB.text = GenericSinglngton<GameManager>.Instance.enemyCntB.ToString();
        //enemyTextC.text = GenericSinglngton<GameManager>.Instance.enemyCntC.ToString();

        //if (GenericSinglngton<GameManager>.Instance.boss != null)
        //{
        //    BossHPGroup.anchoredPosition = Vector3.down * 30;
        //    BossHPBar.localScale = new Vector3((float)GenericSinglngton<GameManager>.Instance.boss.CurHP / (float)GenericSinglngton<GameManager>.Instance.boss.MaxHP, 1, 1);
        //}
        //else
        //{
        //    BossHPGroup.anchoredPosition = Vector3.up * 200;

        //}
    }
}
