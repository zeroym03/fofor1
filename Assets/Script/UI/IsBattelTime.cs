using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsBattelTime : MonoBehaviour
{
     float PlayTime;

     Text playTimeText;
    int hour;
    int Min;
    int second;
    private void Awake()
    {
        playTimeText = GetComponent<Text>();
    }
    void Update()
    {
        if (GenericSinglngton<GameManager>.Instance.isBattel)//UI
        {
            PlayTime += Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
         hour= (int)(PlayTime / 3600);
         Min = (int)((PlayTime - hour * 3600) / 60);
         second= (int)PlayTime % 60;
        playTimeText.text =
                string.Format("{0:00}", hour) + ":" +
                string.Format("{0:00}", Min) + ":" +
                string.Format("{0:00}", second);

    }
}
