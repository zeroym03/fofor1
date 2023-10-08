using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour   // Player AI - FSM
{
    public GameObject target;

    public enum eState                            //1. 상태 정의 
    {
        idle,
        walk,
        attack,
        hit,
        die,
        trace,
    }

    eState state_cur = eState.idle;               //2. 현재 상태
    float state_time;                             //상태 시간

    //public void State_start(eState state)       //3. 상태 변경 하는 함수 - 이동속도, 에니메이션 동작

    //public void State_update(eState state)      //4.상태 체크 하는 함수 - 프레임별로 계속 체크할 코드( 키입력, 이동 ) 

    /*  void Start()    
        { 
            State_start(eState.idle);           // 상태 변경
            state_time = Time.time;
        }
        void Update()
        {
            State_update();                     // 상태 프로세서
        }
    */

    //------------------------------------------------------------------------------------
    public void State_start(eState state)       //상태 변경 하는 함수
    {
        state_cur = state;                      //현재 상태 변경 

        state_time = Time.time + 1.0f;          //상태 시간

        switch (state_cur)
        {
            case eState.idle:
                //동작변경 // movespeed = 0;
                break;
            case eState.walk:
                //동작변경 // movespeed = 2;
                break;
            case eState.trace:
                //동작변경 // movespeed = 2;
                break;
            case eState.attack:
                state_time = Time.time + 0.5f;
                //movespeed = 0;
                //동작변경
                //target.GetComponent<GameBase>().Damage(  );
                break;
            case eState.hit:
                state_time = Time.time + 0.3f;
                break;
            case eState.die:
                state_time = Time.time + 1.0f;
                break;
            default:
                break;
        }
    }

    //------------------------------------------------------------------------------------
    public void State_update()
    {
        switch (state_cur)
        {
            case eState.idle:
                if (target != null && Dist(target) < 4.0f) { State_start(eState.trace); break; }

                // rotate random
                transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
                State_start(eState.walk);
                break;
            case eState.walk:
                if (target == null) { State_start(eState.idle); break; }
                if (Dist(target) < 4.0f) { State_start(eState.trace); break; }
                if (Time.time > state_time) { State_start(eState.idle); break; }
                // move forward
                transform.position += transform.forward * 0.5f * Time.deltaTime;                
                break;
            case eState.trace:
                if (target == null) { State_start(eState.idle); break; }
                if (Dist(target) > 6.0f) { State_start(eState.idle); break; }
                if (Dist(target) < 2.0f) { State_start(eState.attack); break; }  //attack
                // move trace
                transform.LookAt(target.transform);
                transform.position += transform.forward * 1.0f * Time.deltaTime;
                break;
            case eState.attack:
                if (Time.time > state_time) { State_start(eState.idle); break; }
                break;
            case eState.hit:
                if (Time.time > state_time) { State_start(eState.idle); break; }
                break;
        }
    }

    float Dist(GameObject targrt ) 
    {
        return Vector3.Distance(target.transform.position, transform.position);
    } 


    // 일정한 경로로 돌아다니는 패턴 만들어 보기
    // 가까이오면 도망가는 패턴 만들어보기
    // 공격 패턴 아이디어 구현해 보기

}
