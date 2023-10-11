using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
  public  Transform target { get; set; }
     Vector3 offest;
    private void Awake()
    {
        target = GameObject.FindObjectOfType<PlayerUnit>().transform;
        offest =    new Vector3(0,30,-18);
    }
    void Update()
    {
        transform.position = target.position + offest;
    }
}
