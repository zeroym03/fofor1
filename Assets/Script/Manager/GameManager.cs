

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerUnit playerUnit { get; set; }
    public GameObject player;
    public BossMob boss;
    public int stage { get; set; } = 1;
    public int score = 0;

    public bool isBattel;

    public int enemyCntA { get; set; }
    public int enemyCntB { get; set; }
    public int enemyCntC { get; set; }
    public int enemyCntD { get; set; }


    public GameObject ItemShop { get; set; }
    public GameObject WeaponShop;
   public Shop _itemShop;
   public Shop _WeaponShop;

    public GameObject StartZon;

    public Transform[] enemyZones;
    public GameObject[] enemies = new GameObject[4];
    public List<int> enemyList;

    private void Awake()
    {
        BaseSet();
    }
    void BaseSet()
    {
        enemyList = new List<int>();

    }
    public void GameStart()
    {
        ShopSetting();
        GenericSinglngton<UIManager>.Instance.gameCam = Instantiate(Resources.Load("Game Camera") as GameObject);
        player = Instantiate(Resources.Load("Character/Player").GameObject());
        playerUnit = player.GetComponent<PlayerUnit>();
        GenericSinglngton<UIManager>.Instance.gameCam.GetComponent<GameCamera>().Set();
        GenericSinglngton<UIManager>.Instance.UIGameStart();
    }
    void ShopSetting()
    {
        _itemShop = ItemShop.GetComponentInChildren<Shop>();
        _itemShop.UIGroup = GenericSinglngton<UIManager>.Instance.ItemShopUI;

        _WeaponShop = WeaponShop.GetComponentInChildren<Shop>();
        _WeaponShop.UIGroup = GenericSinglngton<UIManager>.Instance.WeaponShopUI;
    }
    public void GameOver()
    {
        GenericSinglngton<UIManager>.Instance.UIGameOver();
    }
    public void StageStart()//game
    {
        ItemShop.SetActive(false);
        WeaponShop.SetActive(false);
        StartZon.SetActive(false);
        foreach (Transform zone in enemyZones)
        {
            zone.gameObject.SetActive(true);
        }
        isBattel = true;
        StartCoroutine(inBattel());
    }
    IEnumerator inBattel() //game 
    {
        if (stage % 5 == 0)
        {
            enemyCntD++;
            GameObject instantenemy = Instantiate(enemies[3], enemyZones[0].position, enemyZones[0].rotation);
            Enemy enemy = instantenemy.GetComponent<Enemy>();
            enemy.target = playerUnit.transform;
            enemy.manager = this;
            boss = instantenemy.GetComponent<BossMob>();
        }
        else
        {
            for (int i = 0; i < stage; i++)
            {
                int ran = Random.Range(0, 3);
                enemyList.Add(ran);
                switch (ran)
                {
                    case 0:
                        enemyCntA++;
                        break;
                    case 1:
                        enemyCntB++;
                        break;
                    case 2:
                        enemyCntC++;
                        break;
                }
            }
            while (enemyList.Count > 0)
            {
                int ranZone = Random.Range(0, 4);
                GameObject instantenemy = Instantiate(enemies[enemyList[0]], enemyZones[ranZone].position, enemyZones[ranZone].rotation);
                Enemy enemy = instantenemy.GetComponent<Enemy>();
                enemy.target = playerUnit.transform;
                enemy.manager = this;
                enemyList.RemoveAt(0);
                yield return new WaitForSeconds(5);
            }
        }
        while (enemyCntA + enemyCntB + enemyCntC + enemyCntD > 0)
        {
            yield return null;
        }
        yield return new WaitForSeconds(5);
        boss = null;
        StageEnd();
    }
    public void AddEnemy()
    {
        enemies[0] = Resources.Load("Character/Enemy A").GameObject();
        enemies[1] = Resources.Load("Character/Enemy B").GameObject();
        enemies[2] = Resources.Load("Character/Enemy C").GameObject();
        enemies[3] = Resources.Load("Character/Enemy D").GameObject();
    }
    public void StageEnd()// game
    {
        playerUnit.transform.position = Vector3.zero;
        stage++;

        isBattel = false;
        ItemShop.SetActive(true);
        WeaponShop.SetActive(true);
        StartZon.SetActive(true);
        foreach (Transform zone in enemyZones)
        {
            zone.gameObject.SetActive(false);
        }
    }
}
