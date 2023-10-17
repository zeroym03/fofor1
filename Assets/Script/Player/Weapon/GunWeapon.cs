using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : Weapon
{
    [SerializeField] Transform bulletpos;
    [SerializeField] Transform bulletCasePos;

    GameObject bullet;
    GameObject bulletCase;
    int _damege = 17;
    private void Awake()
    {
        bullet = Resources.Load("Weapon/SubMachineGun Bullet") as GameObject;
        bulletCase = Resources.Load("Weapon/Bullet Case") as GameObject;
        type = Type.Gun;
            damege = _damege;
    }
    public override void Use()
    {
        if (type == Type.Gun && curAmmo > 0)
        {
            curAmmo--;
            StartCoroutine(Shot());
        }
    }

    IEnumerator Shot()
    {
        GameObject intantBullet = Instantiate(bullet, bulletpos.position, bulletpos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletpos.forward * 50;

        yield return null;
        GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody CaseRigid = intantCase.GetComponent<Rigidbody>();
        Vector3 casevec =
            bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(-3, -2);
        CaseRigid.AddForce(casevec, ForceMode.Impulse);
        CaseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);

    }
}
