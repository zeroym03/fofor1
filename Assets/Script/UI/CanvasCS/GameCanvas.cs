using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] Text scoreText;//메뉴패널

    [SerializeField] Text currentStageText;//게임패널
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

    [SerializeField] RectTransform bossHPGroup;
    [SerializeField] RectTransform bossHPBar;
    [SerializeField] RectTransform itemShopUI;
    [SerializeField] RectTransform weaponShopUI;

    [SerializeField] Text lastScoreText;


    float PlayTime;
    int hour;
    int Min;
    int second;
    void Awake()
    {
        GenericSinglngton<UIManager>.Instance.TextCH(scoreText, GenericSinglngton<GameManager>.Instance.score.ToString());
        GenericSinglngton<UIManager>.Instance.ItemShopUI = itemShopUI;
        GenericSinglngton<UIManager>.Instance.WeaponShopUI = weaponShopUI;
        GenericSinglngton<UIManager>.Instance.bestScoreText = lastScoreText;
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
        if (GenericSinglngton<UIManager>.Instance.gamePanel.gameObject.activeSelf == true)
        {
            CurrentScoreText();
            playTimeTextCH();
            PlayerHPText();
            CurrentCoinText();
            EnemyTextCH();
            weaponImage();
            BossHPBarCH();
            StageText();
        }

    }
    void CurrentScoreText()
    {
        currentScoreText.text = GenericSinglngton<GameManager>.Instance.score.ToString();
    }//GameMain
    void StageText()
    {
        GenericSinglngton<UIManager>.Instance.TextCH(currentStageText, "스테이지 : " + GenericSinglngton<GameManager>.Instance.stage.ToString());
    }//GameMain
    void playTimeTextCH()
    {
        hour = (int)(PlayTime / 3600);
        Min = (int)((PlayTime - hour * 3600) / 60);
        second = (int)PlayTime % 60;
        playTimeText.text =
                string.Format("{0:00}", hour) + ":" +
                string.Format("{0:00}", Min) + ":" +
                string.Format("{0:00}", second);
    }//GameMain
    void PlayerHPText()
    {
        currentHpText.text = GenericSinglngton<GameManager>.Instance.playerUnit.health + " / " + GenericSinglngton<GameManager>.Instance.playerUnit.maxhealth;
    }//GameMain
    void CurrentCoinText()
    {
        currentCoinText.text = string.Format("{0:n0}", GenericSinglngton<GameManager>.Instance.playerUnit.coin);
    }//GameMain
    void EnemyTextCH()
    {
        enumyAText.text = GenericSinglngton<GameManager>.Instance.enemyCntA.ToString();
        enumyBText.text = GenericSinglngton<GameManager>.Instance.enemyCntB.ToString();
        enumyCText.text = GenericSinglngton<GameManager>.Instance.enemyCntC.ToString();

    }//GameMain
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

    }//GameMain
    void BossHPBarCH()
    {
        if (GenericSinglngton<GameManager>.Instance.boss != null)
        {
            bossHPGroup.anchoredPosition = Vector3.down * 30;
            bossHPBar.localScale = new Vector3((float)GenericSinglngton<GameManager>.Instance.boss.CurHP / (float)GenericSinglngton<GameManager>.Instance.boss.MaxHP, 1, 1);
        }
        else
        {
            bossHPGroup.anchoredPosition = Vector3.up * 200;
        }
    }//GameMain
}
