using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestUnit : MonoBehaviour
{
    PlayerState playerState;
  public  PlayerAni playerAni;
  public  PlayerUnitData unitData { get; set; }
    private void Awake()
    {
        playerAni = GetComponent<PlayerAni>();
        unitData = GetComponent<PlayerUnitData>();
    }
    private void Start()
    {
        SetState(new PlayerMoveState());
    }
    private void Update()
    {
        playerState.StateUpDate();
    }
    public void SetState(PlayerState state)
    {
        playerState = state;
        playerState.StateStart(this);
    }
}
