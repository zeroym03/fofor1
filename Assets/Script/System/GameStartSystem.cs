using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class GameStartSystem : MonoBehaviour //시작 생성 관리 시스템
{

    public GameObject canvas { get; set; }
    void Awake()
    {
            StopAllCoroutines();//스탑

        if (PlayerPrefs.HasKey("MaxScore"))
        {
            PlayerPrefs.SetInt("MaxScore", 0);
        }
        canvas = Instantiate(Resources.Load("Canvas/GameCanvas") as GameObject);
        GenericSinglngton<PlaySceneManager>.Instance.SceneSetMonsterManager();
        GenericSinglngton<PlaySceneManager>.Instance.SceneSetGameManager();
        GenericSinglngton<PlaySceneManager>.Instance.SceneSetUIManager();

    }

}
