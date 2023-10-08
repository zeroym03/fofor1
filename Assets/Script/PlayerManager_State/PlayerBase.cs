using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType  //타입
{
    Hero,
    Enemy,
    Base
}

public class PlayerBase : MonoBehaviour
{
    public GameObject GO;

    public string UID;
    public string Nickname { get; set; }
    public PlayerType Type { get; set; }
    public int Level { get; set; } = 0;
    public int Hp_max { get; set; } = 1000;
    public int Hp_cur { get; set; } = 1000;
    public int Ap { get; set; } = 50;
    public int Dp { get; set; } = 0;

    public float MoveSpeed = 2.0f;
    public float SpinSpeed = 180.0f;

    GameObject target;

    //void Start() { }
    //void Update() { }

    public virtual void InitPlayer(string uid)  
    {        
        UID = uid;
        GO = gameObject;
        Type = PlayerType.Base;
        print("Base InitPlayer()");
    }
    public virtual void UpdatePlayer()
    {
    }
    public virtual void Move()
    {
        //print("Player Move()");
    }
    public virtual void Fire()
    {       

        //print("Player Fire()");
    }

    /*    public void Attack() {
            PlayerBase other;
            other = GameObject.Find("Enemy01").GetComponent<PlayerBase>();
            other.Damage(this);
        }*/
    public void Damage(PlayerBase enemy) {
        Hp_cur -= (enemy.Ap - Dp);
    }

}
