using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerScene : MonoBehaviour
{
    //public GameObject bulletPrefab; // 발사체

    void Start()
    {
        // PlayerManager 테스트 ----------------------------------

        //PlayerBase hero = PlayerManager.Instance.AddPlayer("0", PlayerType.Hero, "PlayerHero" );
        //hero.GO.transform.position = new Vector3(0, 0, 0);

        //PlayerBase p1   = PlayerManager.Instance.AddPlayer("1", PlayerType.Enemy, "PlayerEnemy");
        //p1.GO.transform.position = new Vector3(-3, 0, 3);
        //PlayerBase p2   = PlayerManager.Instance.AddPlayer("2", PlayerType.Enemy, "PlayerEnemy");
        //p2.GO.transform.position = new Vector3(3, 0, 3);
        //PlayerBase p3   = PlayerManager.Instance.AddPlayer("3", PlayerType.Enemy, "PlayerEnemy");
        //p3.GO.transform.position = new Vector3(0, 0, 3);
        //PlayerBase p4   = PlayerManager.Instance.AddPlayer("4", PlayerType.Enemy, "PlayerEnemy");
        //p4.GO.transform.position = new Vector3(3, 0, 0);


        // 발사 
        //GameObject go = Instantiate(bulletPrefab); var bullet = go.GetComponent<Bullet>();
        //var target = new Vector3(10,0,0);
        //bullet.Shoot(transform.position, target);
    }

    void Update()
    {


        //// ObjectPoolBullet 테스트 ----------------------------------
        //if (Input.GetMouseButton(0))
        //{
        //    RaycastHit hitResult;
        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitResult))
        //    {
        //        var bullet = ObjectPoolBullet.GetObject();                
        //        var target = new Vector3(hitResult.point.x, transform.position.y, hitResult.point.z);
        //        bullet.Shoot(transform.position, target);
        //    }
        //}
    }
}



