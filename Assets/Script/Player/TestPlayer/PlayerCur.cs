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
        StateSetMove();
    }

    void PlayerMove()
    {
        player.unitData.moveVec = new Vector3(GenericSinglngton<GetKeyCodeManager>.Instance._axisHorx, 0, GenericSinglngton<GetKeyCodeManager>.Instance._axisVerz).normalized;
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
    void StateSetMove()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { player.SetState(new PlayerDodgeState()); }
        if (Input.GetKeyDown(KeyCode.Alpha1)||Input.GetKeyDown(KeyCode.Alpha2)||Input.GetKeyDown(KeyCode.Alpha3)) { player.SetState(new PlayerDodgeState()); }//무기바꾸기
    }
}
public class PlayerDodgeState : PlayerState
{
    public override void StateStart(PlayerTestUnit playerUnit)
    {
        base.StateStart(playerUnit);
        Dodge();
    }
    public override void StateUpDate()
    {
        player.transform.position += player.unitData.DodgeVec * player.unitData.speed * Time.deltaTime;

    }
    void Dodge()
    {
        player.unitData.speed *= 2;
        player.unitData.StartCoroutine(DodgeOut());
        player.playerAni.DoDodge();
        player.unitData.DodgeVec = new Vector3(GenericSinglngton<GetKeyCodeManager>.Instance._axisHorx, 0, GenericSinglngton<GetKeyCodeManager>.Instance._axisVerz).normalized;
    }
    IEnumerator DodgeOut()
    {
        yield return new WaitForSeconds(0.6f);
        player.unitData.speed *= 0.5f;
        player.SetState(new PlayerMoveState()); // 매번 new 를 안해야 좋을거같은데 어떻게 해야할까
    }
}

