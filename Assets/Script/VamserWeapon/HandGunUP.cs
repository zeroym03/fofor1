using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunUP : VAMSWeapon
{
    HandGun rangeWeapon;
    void Start()
    {
        rangeWeapon =  GetComponent<HandGun>();
        Debug.Log(rangeWeapon);
           
    }
    public override void VAMSAttack()
    {
        StartCoroutine(enumerator());
    }
     IEnumerator enumerator()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log(rangeWeapon.bullet.name);
        Debug.Log(rangeWeapon.bulletpos.position);
        Debug.Log(rangeWeapon.bulletpos.rotation);

        GameObject intantBullet = Instantiate(rangeWeapon.bullet, rangeWeapon.bulletpos.position, rangeWeapon.bulletpos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = rangeWeapon. bulletpos.forward * 50;
    }
}
