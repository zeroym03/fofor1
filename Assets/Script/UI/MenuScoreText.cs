using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScoreText : MonoBehaviour
{
    Text menuScoreText;
    private void Awake()
    {
        menuScoreText = GetComponent<Text>();
    }
    void Start()
    {
        menuScoreText.text = string.Format("{0:n0}", GenericSinglngton<GameManager>.Instance.score);

    }
}
