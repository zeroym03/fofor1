using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopText : MonoBehaviour
{
    void Awake()
    {
        GenericSinglngton<UIManager>.Instance.itemTalkText = gameObject.GetComponent<Text>();
    }
}