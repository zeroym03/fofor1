using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    void Awake()
    {
        GenericSinglngton<UIManager>.Instance.menuPanel = gameObject;
    }
}
