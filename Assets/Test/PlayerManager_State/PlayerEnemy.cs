using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemy : PlayerBase
{
    PlayerCur state;

    public override void InitPlayer(string uid)
    {        
        base.InitPlayer(uid);
        Type = PlayerType.Enemy;
        //print("Enemy InitPlayer()");

        state = GetComponent<PlayerCur>();
        state.State_start(PlayerCur.eState.idle);
        state.target = GenericSinglngton<PlayerManager>.Instance.Hero.gameObject;                
    }

    public override void UpdatePlayer()
    {
        Move();
        Fire();
        
        state.State_update();
    }

    public override void Move() {
        //print("Enemy Move()");
    }

    public override void Fire() {
        //print("Enemy Fire()");
    }
}
