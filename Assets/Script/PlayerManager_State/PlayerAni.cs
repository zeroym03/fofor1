using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAni : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void DoSwap()
    {
        animator.SetTrigger("DoSwap");
    }
    public void DoDodge()
    {
        animator.SetTrigger("doDodge");
    }
  public  void MoveAnime(Vector3 moveVec)
    {
        animator.SetBool("isRun", moveVec != Vector3.zero);
        animator.SetBool("isWalk", GenericSinglngton<GetKeyCodeManager>.Instance._walkDown);
    }
    public void WeaponTypeAttack(Weapon equipWeapon)
    {
        animator.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot");
    }
    public void DoReload()
    {
        animator.SetTrigger("doReload");
    }
    public void DoDie() 
    {
    animator.SetTrigger("doDie");
        }
}
