using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class GameStartSystem : MonoBehaviour //시작 생성 관리 시스템
{

    public GameObject canvas { get; set; }
    public GameObject enemyZon { get; set; }
    public GameObject itemShop { get; set; }
    public GameObject weaponShop { get; set; }
    public GameObject zone { get; set; }

    void Awake()
    {
        
        if (PlayerPrefs.HasKey("MaxScore"))
        {
            PlayerPrefs.SetInt("MaxScore", 0);
        }
        canvas = Instantiate(Resources.Load("Canvas/GameCanvas") as GameObject);

        GenericSinglngton<MonsterManager>.Instance.enemyZones    = Instantiate(Resources.Load("enemyZon") as GameObject).GetComponentsInChildren<Transform>();
        GenericSinglngton<MonsterManager>.Instance.AllSet();

        GenericSinglngton<GameManager>.Instance.BaseSet();
        GenericSinglngton<GameManager>.Instance.WeaponShop = Instantiate(Resources.Load("Shop/Weapon Shop") as GameObject);
        GenericSinglngton<GameManager>.Instance.ItemShop = Instantiate(Resources.Load("Shop/Item Shop") as GameObject);
        GenericSinglngton<GameManager>.Instance.StartZon = Instantiate(Resources.Load("Zone") as GameObject);


        GenericSinglngton<UIManager>.Instance.menuCam = Instantiate(Resources.Load("Camera/Menu Camera") as GameObject);
        GenericSinglngton<UIManager>.Instance.gamePanel.SetActive(false);
        GenericSinglngton<UIManager>.Instance.gameOverPanel.SetActive(false);

     //   Instantiate(Resources.Load("Canvas/WeaponUpCanvas"));  //test


   
    }

}
