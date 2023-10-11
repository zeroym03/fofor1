using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtenManager : MonoBehaviour
{
 public   void GameStartButten()
    {
       GenericSinglngton<GameManager>.Instance.GameStart();
    }
}
