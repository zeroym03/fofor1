using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtenManager : MonoBehaviour
{
    public void GameReStart()
    {
        GenericSinglngton<GameManager>.Instance.ReStart();
    }
    public void GameStartButten()
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

    public void ShopExit(RectTransform rectTransform)
    {
        rectTransform.position = Vector3.down *1000;
        GenericSinglngton<GameManager>.Instance.playerUnit.isShop = false;
        GenericSinglngton<GameManager>.Instance._itemShop.Exit();
    }
}
