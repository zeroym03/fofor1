using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<int> enemyList;

    public GameObject[] enemies = new GameObject[4];
    public Transform[] enemyZones { get; set; }
  public  bool stageEnd { get; set; } = true;

    public int enemyCntA { get; set; } = 0;
    public int enemyCntB { get; set; } = 0;
    public int enemyCntC { get; set; } = 0;
    public int enemyCntD { get; set; } = 0;
    public void AllSet()
    {
        EnemySet();
        AddEnemy();
    }
    public void EnemySet()
    {
        enemyList = new List<int>();

        enemyCntA = 0;
        enemyCntB = 0;
        enemyCntC = 0;
        enemyCntD = 0;
    }
    public void AddEnemy()
    {
        enemies[0] = Resources.Load("Character/Enemy A").GameObject();
        enemies[1] = Resources.Load("Character/Enemy B").GameObject();
        enemies[2] = Resources.Load("Character/Enemy C").GameObject();
        enemies[3] = Resources.Load("Character/Enemy D").GameObject();
    }
    public void inBattelStart()
    {
        if (GenericSinglngton<GameManager>.Instance.stage % 5 == 0)
        {
            enemyCntD++;
            GameObject instantenemy = Instantiate(enemies[3], enemyZones[1].position, enemyZones[1].rotation);
            Enemy enemy = instantenemy.GetComponent<Enemy>();

            enemy.target = GenericSinglngton<GameManager>.Instance.playerTestUnit.transform;

            GenericSinglngton<GameManager>.Instance.boss = instantenemy.GetComponent<BossMob>();
        }
        else
        {
            for (int i = 0; i < GenericSinglngton<GameManager>.Instance.stage; i++)
            {
                int ran = Random.Range(0, 3);
                enemyList.Add(ran);
                switch (ran)
                {
                    case 0:
                        enemyCntA++;
                        break;
                    case 1:
                        enemyCntB++;
                        break;
                    case 2:
                        enemyCntC++;
                        break;
                }
            }
                Invoke("enemySpown", 4);
        }
    }
    void enemySpown()
    {
        int ranZone = Random.Range(1, 5);
        GameObject instantenemy = Instantiate(enemies[enemyList[0]], enemyZones[ranZone].position, enemyZones[ranZone].rotation);
        Enemy enemy = instantenemy.GetComponent<Enemy>();
        enemy.target = GenericSinglngton<GameManager>.Instance.playerTestUnit.transform;
        enemyList.RemoveAt(0);
        Invoke("enemySpown", 4);
    }
    private void Update()
    {
        StageEndCheck();
    }
    void StageEndCheck()
    {
        if (stageEnd == true) return;

        if (enemyCntA + enemyCntB + enemyCntC + enemyCntD <= 0&&stageEnd == false)
        {
            stageEnd = true;
            StageEnd();
        }
    }
 void  StageEnd()
    {
        GenericSinglngton<GameManager>.Instance.boss = null;
        Invoke("GenericSinglngton<GameManager>.Instance.StageEnd()",3);
       // GenericSinglngton<GameManager>.Instance.StageEnd(); //UIButtenManager에서 스탑코루틴을하는데 실행됨 ???
    }
}

