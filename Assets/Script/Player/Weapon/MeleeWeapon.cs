using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] BoxCollider meleeArea;
    [SerializeField] TrailRenderer trailEffact;
    private void Awake()
    {
        type = Type.Melee;
        damege = 30;
    }
    public override void Use()
    {
        if (type == Type.Melee)
        {
            StartCoroutine(Swing());
        }
    }
    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = true;
        trailEffact.enabled = true;
        yield return new WaitForSeconds(0.6f);
        meleeArea.enabled = false;
        yield return new WaitForSeconds(0.1f);
        trailEffact.enabled = false;
    }
}
