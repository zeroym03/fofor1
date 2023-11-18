using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartZone : MonoBehaviour
{
  
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("OnTriggerEnter");
          GenericSinglngton<GameManager>.Instance.StageStart();
        }
    }
}

