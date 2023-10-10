using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartSystem : MonoBehaviour //시작 생성 관리 시스템
{
    GameObject menuCamera { get; set; }
    GameObject canvas { get; set; }
    GameObject enemyZon { get; set; }
    GameObject gameCamera { get; set; }
    GameObject itemShop { get; set; }
    GameObject WeaponShop  { get; set; }
    GameObject zone { get; set; }
    void Awake()
    {

        canvas = Resources.Load("Canvas") as GameObject;
        Instantiate(canvas);
        menuCamera = Resources.Load("Camera/Menu Camera")as GameObject;
        Instantiate(menuCamera);

        enemyZon = Resources.Load("enemyZon") as GameObject;
        Instantiate(enemyZon);
        gameCamera = Resources.Load("Game Camera") as GameObject;
        Instantiate(gameCamera);
    WeaponShop = Resources.Load("Weapon Shop") as GameObject;
        Instantiate(WeaponShop);
        itemShop = Resources.Load("Item Shop") as GameObject;
        Instantiate(itemShop);
        zone = Resources.Load("Zone") as GameObject;
        Instantiate(zone);
    }

}
