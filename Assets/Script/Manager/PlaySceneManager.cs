using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneManager : MonoBehaviour
{
    public void SceneSetMonsterManager()
    {
        GenericSinglngton<MonsterManager>.Instance.enemyZones = Instantiate(Resources.Load("enemyZon") as GameObject).GetComponentsInChildren<Transform>();
        GenericSinglngton<MonsterManager>.Instance.AllSet();
    } 
    public void SceneSetGameManager()
    {
        GenericSinglngton<GameManager>.Instance.BaseSet();
        GenericSinglngton<GameManager>.Instance.WeaponShop = Instantiate(Resources.Load("Shop/Weapon Shop") as GameObject);
        GenericSinglngton<GameManager>.Instance.ItemShop = Instantiate(Resources.Load("Shop/Item Shop") as GameObject);
        GenericSinglngton<GameManager>.Instance.StartZon = Instantiate(Resources.Load("Zone") as GameObject);
    }
    public void SceneSetUIManager()
    {
        GenericSinglngton<UIManager>.Instance.menuCam = Instantiate(Resources.Load("Camera/Menu Camera") as GameObject);
        GenericSinglngton<UIManager>.Instance.gamePanel.SetActive(false);
        GenericSinglngton<UIManager>.Instance.gameOverPanel.SetActive(false);
        GenericSinglngton<UIManager>.Instance.WeaponPanel = Resources.Load("Canvas/WeaponUpCanvas") as GameObject;
    }
    public void SceneEnd()//UIButtenManager 에서 버튼으로 사용
    {
        Debug.Log("GameReStart");
        StopAllCoroutines();//스탑
        SceneManager.LoadScene(0);
    }
}
