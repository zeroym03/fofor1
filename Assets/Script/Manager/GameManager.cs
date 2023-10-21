

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerUnit playerUnit { get; set; }
    public GameObject player;
    public BossMob boss;
    public int stage { get; set; } = 0;
    public int score { get; set; } = 0;

    public bool isBattel = false;

    public int enemyCntA { get; set; } = 0;
    public int enemyCntB { get; set; } = 0;
    public int enemyCntC { get; set; } = 0;
    public int enemyCntD { get; set; } = 0;


    public GameObject ItemShop { get; set; }
    public GameObject WeaponShop;
    public Shop _itemShop;
    public Shop _WeaponShop;

    public GameObject StartZon;

    public Transform[] enemyZones { get; set; }
    public GameObject[] enemies = new GameObject[4];
    public List<int> enemyList;


    public void BaseSet()
    {
        enemyList = new List<int>();
        enemyCntA = 0;
        enemyCntB = 0;
        enemyCntC = 0;
        enemyCntD = 0;
        isBattel = false;
        stage = 0;
        score = 0;
    }
    public void GameStart()
    {
        ShopSetting();
        GenericSinglngton<UIManager>.Instance.gameCam = Instantiate(Resources.Load("Camera/Game Camera") as GameObject);
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
        stage++;
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
    public void ReStart()//UI
    {
        SceneManager.LoadScene(0);
    }
    IEnumerator inBattel() //game 
    {
        foreach (var enemy in enemyZones)
        {
            Debug.Log(enemy.gameObject.transform.position);
        }
        if (stage % 5 == 0)
        {
            enemyCntD++;
            GameObject instantenemy = Instantiate(enemies[3], enemyZones[1].position, enemyZones[1].rotation);
            Enemy enemy = instantenemy.GetComponent<Enemy>();
            enemy.target = playerUnit.transform;

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
                int ranZone = Random.Range(1, 5);
                GameObject instantenemy = Instantiate(enemies[enemyList[0]], enemyZones[ranZone].position, enemyZones[ranZone].rotation);
                Enemy enemy = instantenemy.GetComponent<Enemy>();
                enemy.target = playerUnit.transform;

                enemyList.RemoveAt(0);
                yield return new WaitForSeconds(4);
            }
        }
        while (enemyCntA + enemyCntB + enemyCntC + enemyCntD > 0)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2);
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
