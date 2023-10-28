using System.Collections;
using UnityEngine;
using static UnityEditor.Progress;

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
        player.PlayerMove();
        StateSetMove();
    }
    public void StateSetMove()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { player.SetState(player.playerDodgeState); }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
        { player.SetState(player.swapState); }//무기바꾸기

        if (Input.GetKeyDown(KeyCode.E)) { player.SetState(player.interationState); }
        if (Input.GetKeyDown(KeyCode.R)) { player.SetState(player.ReloadState); }
        if (Input.GetKey(KeyCode.Mouse0)){ player.SetState(player.attackState); }
    }
}
public class PlayerDodgeState : PlayerState
{
    public override void StateStart(PlayerTestUnit playerUnit)
    {
        base.StateStart(playerUnit);
        player.Dodge();
    }
    public override void StateUpDate()
    {
        player.DodgeMove();
    }
}
public class InterationState : PlayerState
{
    public override void StateStart(PlayerTestUnit playerUnit)
    {
        base.StateStart(playerUnit);
        player.Interation();
    }
}
public class SwapState : PlayerState
{
    public override void StateStart(PlayerTestUnit playerUnit)
    {
        base.StateStart(playerUnit);
        player.Swap();
    }
}
public class AttackState : PlayerState
{
    public override void StateStart(PlayerTestUnit playerUnit)
    {
        base.StateStart(playerUnit);
    }
    public override void StateUpDate()
    {
        player.Attack();
    }
}
public class ReloadState : PlayerState
{
    public override void StateStart(PlayerTestUnit playerUnit)
    {
        base.StateStart(playerUnit);
        player.Reload();
    }
}
public class ByItemState : PlayerState
{
    public override void StateStart(PlayerTestUnit playerUnit)
    {
        base.StateStart(playerUnit);
    }
}
public class DieState : PlayerState
{
    public override void StateStart(PlayerTestUnit playerUnit)
    {
        base.StateStart(playerUnit);
        player.OnDie();
    }
}
