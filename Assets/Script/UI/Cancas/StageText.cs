using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageText : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        GenericSinglngton<UIManager>.Instance.TextCH(text,"�������� : "+GenericSinglngton<GameManager>.Instance.stage.ToString());  
    }
}
