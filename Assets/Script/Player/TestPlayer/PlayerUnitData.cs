using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitData : MonoBehaviour
{
    //스탯은 다른클래스로 
  public  float speed { get; set; } = 10;
   public GameObject[] weapons;
    public GameObject[] grenades;
    public GameObject granadeobj;
    public Camera follwouCamera;
    public bool[] hasWeapons { get; set; } = new bool[4];

    public int ammo { get; set; } = 100;//프로포티 초기화 필요
    public int coin { get; set; } = 10000;
    public int health { get; set; } = 10;
    public int hasGreandes { get; set; } = 0;
    public int maxammo { get; set; } = 300;
    public int maxhealth { get; set; } = 300;
    public int equipWeaponIndex { get; set; } = -1;
    public int weaponIndex { get; set; } = -1;

    public int maxcoin = int.MaxValue;
    public int maxhasGreandes = 4;


    public float fireDelay = 0f;

    //  public bool isShop { get; set; } = false;
    public bool isDodge { get; set; } = false;
    public bool isSwap { get; set; } = false;
    public bool isFireReady { get; set; } = true;
    public bool isBorder { get; set; } = false;
    public bool isDamege { get; set; } = false;
    public bool isDead { get; set; } = false;
    public bool isReload { get; set; } = false;

    public Vector3 moveVec { get; set; }
    public Vector3 DodgeVec { get; set; }

    public VAMSWeapon vAMSWeapon { get; set; } = null;
    public PlayerAni playerAni { get; set; }
    public Rigidbody plrigidbody { get; set; }
    public MeshRenderer[] meshes { get; set; }
    public GameObject nearobjeact { get; set; }
    public GameObject enterobjeact { get; set; }
    public Weapon equipWeapon { get; set; }


}
