using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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
       
    //------------------------------------------------------------------------------------
    public virtual void InitPlayer(string uid)          //초기화
    {
        UID = uid;
        GO = gameObject;
        Type = PlayerType.Base;

        State_start(eState.idle);                       //상태 시작
        state_time = Time.time;
    }

    public virtual void UpdatePlayer()                  //프로세서
    {
        State_update();                                 //상태 프로세서
    }

    //------------------------------------------------------------------------------------
    public enum eState                                  //상태 정의
    {
        idle,
        walk,
        trace,
        attack,
        hit,
        die,
    }
    eState state_cur = eState.idle;         // 현재 상태
    float state_time;                       // 상태 시간

    //------------------------------------------------------------------------------------
    public virtual void State_start(eState state) {     //상태 변경 
        state_cur = state;              
        state_time = Time.time + 1.0f;

        switch (state_cur) {       
            case eState.idle:
                break;
            case eState.walk:
                break;
            case eState.trace:
                break;
            case eState.attack:
                break;
            case eState.hit:
                break;
            case eState.die:
                break;
        }
    }

    //------------------------------------------------------------------------------------
    public virtual void State_update() {                //상태 감시
        switch (state_cur) {
			case eState.idle:
				break;
			case eState.walk:
				break;
			case eState.trace:
				break;
			case eState.attack:
				break;
			case eState.hit:
				break;
			case eState.die:
				break;
		}
	}

	float Dist(GameObject targrt) {
        return Vector3.Distance(target.transform.position, transform.position);
    }
}
