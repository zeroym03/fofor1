using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartSystem : MonoBehaviour //시작 생성 관리 시스템
{

    public GameObject canvas { get; set; }
    public GameObject enemyZon { get; set; }
    public GameObject itemShop { get; set; }
    public GameObject WeaponShop  { get; set; }
    public GameObject zone { get; set; }
   
    void Awake()
    {
        if (PlayerPrefs.HasKey("MaxScore"))
        {
            PlayerPrefs.SetInt("MaxScore", 0);
        }
        canvas = Resources.Load("Canvas") as GameObject;
        Instantiate(canvas);


        enemyZon = Resources.Load("enemyZon") as GameObject;
        Instantiate(enemyZon);
    
        WeaponShop = Resources.Load("Weapon Shop") as GameObject;
        Instantiate(WeaponShop);
        itemShop = Resources.Load("Item Shop") as GameObject;
        Instantiate(itemShop);
        zone = Resources.Load("Zone") as GameObject;
        Instantiate(zone);
        GenericSinglngton<UIManager>.Instance.menuCam = Resources.Load("Camera/Menu Camera") as GameObject;
        Instantiate(GenericSinglngton<UIManager>.Instance.menuCam);
      
    }

}
