using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerState
{
    protected PlayerTestUnit player;
    public virtual void StateStart(PlayerTestUnit playerUnit)
    {
        player = playerUnit;
    }
    public virtual void StateUpDate() { }
}
public class PlayerMoveState : PlayerState
{
    public override void StateUpDate()
    {
        PlayerMove();
        player.playerAni.MoveAnime(player.unitData.moveVec);
    }

    void PlayerMove()
    {
        player.unitData.moveVec = new Vector3(GenericSinglngton<GetKeyCodeManager>.Instance._axisHorx, 0, GenericSinglngton<GetKeyCodeManager>.Instance._axisVerz).normalized;
        if (player.unitData.isDodge == true) { player.unitData.moveVec = player.unitData.DodgeVec; }
        if (player.unitData.isBorder == false)
        { player.transform.position += player.unitData.moveVec * player.unitData.speed * Time.deltaTime; }

        Turn();
    }
    void Turn()
    {
        if (GenericSinglngton<GetKeyCodeManager>.Instance._fDown)
        {
            Ray ray = player.unitData.follwouCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Vector3 nex = hit.point - player.unitData.transform.position;
                nex.y = 0;
                player.transform.LookAt(player.transform.position + nex);
            }
        }
        if (GenericSinglngton<GetKeyCodeManager>.Instance._fDown == false)
            player.transform.LookAt(player.transform.position + player.unitData.moveVec);
    }
}

