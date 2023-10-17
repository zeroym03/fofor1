using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopText: MonoBehaviour
{
    
    void Awake()
    {
        GenericSinglngton<UIManager>.Instance.weaponTalkText = gameObject.GetComponent<Text>();
    }
}
