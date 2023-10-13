using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtenManager : MonoBehaviour
{
 public   void GameStartButten()
    {
       GenericSinglngton<GameManager>.Instance.GameStart();
    }
    public void ItemButten(int ItemType)
    {
        GenericSinglngton<GameManager>.Instance._itemShop.ItemBuy(ItemType);
    }
    public void WeaponButten(int ItemType)
    {
        GenericSinglngton<GameManager>.Instance._WeaponShop.ItemBuy(ItemType);
    }
}
