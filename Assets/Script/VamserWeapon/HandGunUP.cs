using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunUP : MonoBehaviour
{
    RangeWeapon rangeWeapon;
    void Start()
    {
        rangeWeapon =  GetComponent<RangeWeapon>();
    }

  public void HandUp() 
    {
        GameObject intantBullet = Instantiate(rangeWeapon.bullet, rangeWeapon.bulletpos.position, rangeWeapon.bulletpos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = rangeWeapon. bulletpos.forward * 50;
    }
}
