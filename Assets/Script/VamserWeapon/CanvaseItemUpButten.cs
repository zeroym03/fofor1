using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvaseItemUpButten : MonoBehaviour
{
    [SerializeField]  Text text1;
    [SerializeField] Text text2;
    [SerializeField] Text text3;
    public void WeaponUp()
    {
      if(GenericSinglngton<GameManager>.Instance.playerTestUnit.GetComponentInChildren<HandGun>() )
        {
            GenericSinglngton<GameManager>.Instance.playerTestUnit.AddComponent<HandGunUP>();
        }
    }
}
