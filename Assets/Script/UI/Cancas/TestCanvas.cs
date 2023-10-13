using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TestCanvas : MonoBehaviour
{
    [SerializeField] Text currentScoreText;
    [SerializeField] Text playTimeText;
    [SerializeField] Text currentHpText;
    [SerializeField] Text currentAmmoText;
    [SerializeField] Text currentCoinText;
    [SerializeField] Text enumyAText;
    [SerializeField] Text enumyBText;
    [SerializeField] Text enumyCText;
    [SerializeField] Image hammerImege;
    [SerializeField] Image handgunImege;
    [SerializeField] Image machineGunImege;
    [SerializeField] Image controlRImege;
    [SerializeField] RectTransform BossHPGroup;
    [SerializeField] RectTransform BossHPBar;
    [SerializeField] RectTransform ItemShopUI;
    [SerializeField] RectTransform WeaponShopUI;

    float PlayTime;
    int hour;
    int Min;
    int second;
    void Awake()
    {
        GenericSinglngton<UIManager>.Instance.TextCH(currentScoreText, GenericSinglngton<GameManager>.Instance.score.ToString());
        GenericSinglngton<UIManager>.Instance.ItemShopUI = ItemShopUI; 
        GenericSinglngton<UIManager>.Instance.WeaponShopUI = WeaponShopUI;
    }

    void Update()
    {
        if (GenericSinglngton<GameManager>.Instance.isBattel)//UI
        {
            PlayTime += Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        if (GenericSinglngton<UIManager>.Instance.gamePanal.active == true)
        {
            playTimeTextCH();
            PlayerHPText();
            CurrentCoinText();
            EnemyTextCH();
            weaponImage();
            BossHPBarCH();
        }
     
    }
    void playTimeTextCH()
    {
        hour = (int)(PlayTime / 3600);
        Min = (int)((PlayTime - hour * 3600) / 60);
        second = (int)PlayTime % 60;
        playTimeText.text =
                string.Format("{0:00}", hour) + ":" +
                string.Format("{0:00}", Min) + ":" +
                string.Format("{0:00}", second);
    }
    void PlayerHPText()
    {
        currentHpText.text = GenericSinglngton<GameManager>.Instance.playerUnit.health + " / " + GenericSinglngton<GameManager>.Instance.playerUnit.maxhealth;
    }
    void CurrentCoinText()
    {
        currentCoinText.text = string.Format("{0:n0}", GenericSinglngton<GameManager>.Instance.playerUnit.coin);
    }
    void EnemyTextCH()
    {
        enumyAText.text = GenericSinglngton<GameManager>.Instance.enemyCntA.ToString();
        enumyBText.text = GenericSinglngton<GameManager>.Instance.enemyCntB.ToString();
        enumyCText.text = GenericSinglngton<GameManager>.Instance.enemyCntC.ToString();

    }
    void weaponImage()
    {
        if (GenericSinglngton<GameManager>.Instance.playerUnit.equipWeapon == null)
        {
            currentAmmoText.text = "- / " + GenericSinglngton<GameManager>.Instance.playerUnit.ammo;
        }
        else if (GenericSinglngton<GameManager>.Instance.playerUnit.equipWeapon.type == Weapon.Type.Melee)
        {
            currentAmmoText.text = "- / " + GenericSinglngton<GameManager>.Instance.playerUnit.ammo;
        }
        else
        {
            currentAmmoText.text = GenericSinglngton<GameManager>.Instance.playerUnit.equipWeapon.curAmmo + " / " + GenericSinglngton<GameManager>.Instance.playerUnit.ammo;
        }
        hammerImege.color = new Color(1, 1, 1, GenericSinglngton<GameManager>.Instance.playerUnit.hasWeapons[0] ? 1 : 0);
        handgunImege.color = new Color(1, 1, 1, GenericSinglngton<GameManager>.Instance.playerUnit.hasWeapons[1] ? 1 : 0);
        machineGunImege.color = new Color(1, 1, 1, GenericSinglngton<GameManager>.Instance.playerUnit.hasWeapons[2] ? 1 : 0);
        controlRImege.color = new Color(1, 1, 1, GenericSinglngton<GameManager>.Instance.playerUnit.hasGreandes > 0 ? 1 : 0);

    }
    void BossHPBarCH()
    {
        if (GenericSinglngton<GameManager>.Instance.boss != null)
        {
            BossHPGroup.anchoredPosition = Vector3.down * 30;
            BossHPBar.localScale = new Vector3((float)GenericSinglngton<GameManager>.Instance.boss.CurHP / (float)GenericSinglngton<GameManager>.Instance.boss.MaxHP, 1, 1);
        }
        else
        {
            BossHPGroup.anchoredPosition = Vector3.up * 200;

        }
    }
}
