using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRock : Bullet
{
    Rigidbody body;
    float angularPawer; 
    float scalevalue;
    bool isShoot = false;
    private void Awake()
    { //컴퓨터 성능에따라 바뀌는듯함 확인필요
        body = GetComponent<Rigidbody>();
        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());
    }
    IEnumerator GainPowerTimer()
    {
yield return new WaitForSeconds(2.2f);
        isShoot = true;
    }
    IEnumerator GainPower()
    {
        while (!isShoot)
        {
            angularPawer += 0.02f;
            scalevalue += 0.005f;//0.002
            transform.localScale = Vector3.one * scalevalue;
            body.AddTorque(transform.right * angularPawer, ForceMode.Acceleration);
            yield return null;
        }
    }
}
