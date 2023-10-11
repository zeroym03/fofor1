using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    void Awake()
    {
        GenericSinglngton<UIManager>.Instance.menuCam = gameObject;
    }

}
