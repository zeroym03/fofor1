using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentScoreText : MonoBehaviour
{
    Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
    }
    private void FixedUpdate()
    {
        GenericSinglngton<UIManager>.Instance.TextCH(text, GenericSinglngton<GameManager>.Instance.score.ToString()); 
    }
}
