using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UIElements;

public enum EUnitState
{
    none,
    Idle,
    Move,
    Die,
    Attack,
    Max,
}
public class Unit : MonoBehaviour
{
    UnitState _unitState;
    MeshRenderer _rendderer;
    [SerializeField] int _speed;
    public Vector3 _target = Vector3.zero;
    private void Awake()
    {
        _rendderer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        SetState(new IdleState());
    }
    private void Update()
    {
        _unitState.PlayState();
    }
    public void SetState(UnitState state)
    {
        if(_unitState != null)_unitState.OnExit();
        _unitState = state;
        _unitState.OnEnter(this);
    }
    public void MoveTo(Vector3 target)
    {
        _target = target;
       // StartCoroutine(CoMoveTo(target));
    }
    //IEnumerator CoMoveTo(Vector3 target)
    //{
    //    while(Mathf.Abs(Vector3.Distance(transform.position, target)) > 0.1f)
    //    {
    //        transform.Translate((target - transform.position).normalized * Time.deltaTime * _speed);
    //        yield return null;
    //    }
    //    SetState(new IdleState());
    //}
    public void ChangeColor(Color color)
    {
        _rendderer.material.color = color;
    }
}
public class UnitState
{
    protected Unit _unit;
    public virtual void PlayState()
    {

    }
    public virtual void OnEnter(Unit unit)
    {
        _unit = unit;
    }
    public virtual void OnExit()
    {

    }
}
public class IdleState : UnitState
{
    public override void OnEnter(Unit unit)
    {
        base.OnEnter(unit);
        unit.ChangeColor(Color.green);
    }
    public override void PlayState()
    {
        if (Input.GetMouseButtonDown(0))//마우스 좌클릭이 눌렷을때 그위치 좌표를 저장
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit))
            {
                _unit.SetState(new MoveState());
                _unit.MoveTo(hit.point);
            }
        }
    }
}
public class MoveState: UnitState
{
    public override void OnEnter(Unit unit)
    {
        base.OnEnter(unit);
        unit.ChangeColor(Color.red);
    }
    public override void PlayState()
    {
        if (Input.GetKeyDown(KeyCode.S))//이동정지
        {
            _unit.SetState(new IdleState());
        }
        if(Mathf.Abs(Vector3.Distance(_unit.transform.position, _unit._target))/*현좌표, 입력좌표 비교*/ > 0.1f/*0.1f보다 멀면 실행*/)//눌린 좌표로 이동
        {
            _unit.transform.Translate((_unit._target - _unit.transform.position).normalized * Time.deltaTime *10);

        }
        else
        {
            _unit.SetState(new IdleState());
        }
    }
    public override void OnExit()
    {
        Debug.Log("이동이 끝남");
    }
}

