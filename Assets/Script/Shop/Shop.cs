using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    PlayerUnit enterPlayer;
    public RectTransform UIGroup { get; set; }
    [SerializeField] Animator animator;
    public Text talkText { get; set; }
    public int[] itemPrice;//
    public Transform[] itemPos;
    public GameObject[] itemObj;
    public string[] talkData;
    int price;
    public void Enter(PlayerUnit playerMob)
    {
        enterPlayer = playerMob;
       // enterPlayer.isShop = true;
        UIGroup.anchoredPosition = Vector3.zero;
    }
    public void Exit()
    {
        UIGroup.anchoredPosition = Vector3.down * 1000;
        animator.SetTrigger("doHello");
       // enterPlayer.isShop = false;
    }
    public void ItemBuy(int index)
    {
        price = itemPrice[index];
        if (price > enterPlayer.coin)
        {
            StopCoroutine(Talk());
            StartCoroutine(Talk());
            return;
        }
        enterPlayer.coin -= price;
        Vector3 renVec = Vector3.right * Random.Range(-3, 3) + Vector3.forward * Random.Range(-3, 3);
        Instantiate(itemObj[index], itemPos[index].position + renVec, itemPos[index].rotation);
    }
    IEnumerator Talk()
    {
        GenericSinglngton<UIManager>.Instance.weaponTalkText.text = talkData[1];
        GenericSinglngton<UIManager>.Instance.itemTalkText.text = talkData[1];
        yield return new WaitForSeconds(1f);
        GenericSinglngton<UIManager>.Instance.weaponTalkText.text = talkData[0];
        GenericSinglngton<UIManager>.Instance.itemTalkText.text = talkData[0];
    }
}
