using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvaseItemUpButten : MonoBehaviour
{
    [SerializeField]  Text text1;
    [SerializeField] Text text2;
    [SerializeField] Text text3;
    public void WeaponUp()
    {
        if (GenericSinglngton<GameManager>.Instance.playerTestUnit.GetComponentInChildren<HandGun>().gameObject.activeSelf == false)
        GenericSinglngton<GameManager>.Instance.playerTestUnit.GetComponentInChildren<HandGun>().gameObject.SetActive(true);
          GenericSinglngton<GameManager>.Instance.playerTestUnit.GetComponentInChildren<HandGunUP>().enabled = true;
        this.gameObject.SetActive(false);
    }
}
