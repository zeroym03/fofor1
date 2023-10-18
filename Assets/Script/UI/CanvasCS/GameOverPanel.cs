using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    private void Awake()
    {
        GenericSinglngton<UIManager>.Instance.gameOverPanel = this.gameObject;
    }
}
