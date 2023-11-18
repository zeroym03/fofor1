

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int stage { get; set; } = 4;
    public int score { get; set; } = 0;
    public bool isBattel { get; set; } = false;
    public PlayerUnitData playerUnitData { get; set; }
    public PlayerTestUnit playerTestUnit{ get; set; }
    public GameObject player { get; set; }
    public BossMob boss { get; set; }
    public GameObject ItemShop { get; set; }
    public GameObject WeaponShop { get; set; }
    public Shop _itemShop { get; set; }
    public Shop _WeaponShop { get; set; }
    public GameObject StartZon { get; set; }




    public void BaseSet()
    {
          isBattel = false;
        stage = 0; // 0로 변경필요
        score = 0;
    }
    public void GameStart()
    {
        ShopSetting();
        GenericSinglngton<UIManager>.Instance.gameCam = Instantiate(Resources.Load("Camera/Game Camera") as GameObject);
        player = Instantiate(Resources.Load("Character/TestPlayer").GameObject());
        playerTestUnit = player.GetComponent<PlayerTestUnit>();
        playerUnitData = player.GetComponent<PlayerUnitData>();
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
        Debug.Log("StageStart");
        GenericSinglngton<MonsterManager>.Instance.stageEnd = false;
        stage++;
        ItemShop.SetActive(false);
        WeaponShop.SetActive(false);
        StartZon.SetActive(false);
        foreach (Transform zone in GenericSinglngton<MonsterManager>.Instance.enemyZones)
        {
            zone.gameObject.SetActive(true);
        }
        isBattel = true;
        GenericSinglngton<MonsterManager>.Instance.inBattelStart();
    }
  
    public void StageEnd()// game
    {
        Debug.Log("StageEnd");

        if (playerTestUnit!= null)  playerTestUnit.transform.position = Vector3.zero;
        isBattel = false;
        ItemShop.SetActive(true);
        WeaponShop.SetActive(true);
        StartZon.SetActive(true);
        foreach (Transform zone in GenericSinglngton<MonsterManager>.Instance.enemyZones)
        {
            zone.gameObject.SetActive(false);
        }
    }
}
