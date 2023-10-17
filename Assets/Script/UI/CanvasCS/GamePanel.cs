using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    private void Awake()
    {
        GenericSinglngton<UIManager>.Instance.gamePanal = gameObject;
    }
}
