using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //private static PlayerManager s_Instance = null;  //싱글톤
    //public static PlayerManager Instance {
    //    get {
    //        if (s_Instance == null) {
    //            s_Instance = FindObjectOfType(typeof(PlayerManager)) as PlayerManager; //= new PlayerManager();
    //        }
    //        return s_Instance;
    //    }
    //}

    List<PlayerBase> PlayerList = new List<PlayerBase>();   //List

    public PlayerUnit Hero;

    //void Awake()
    //{
    //    if (s_Instance != null) return;
    //    s_Instance = this;
    //    //DontDestroyOnLoad(this);
    //    //Debug.Log("PlayerManager Awake");
    //}

    //void Start() { }

    void Update()
    {
        foreach (var p in PlayerList)
        {
            p.UpdatePlayer();
        }
    }

    public PlayerBase FindPlayer(string uid) // id 에따라 유닛에 종류찾기 
    {
        foreach (var item in PlayerList)
        {
            if (item.UID == uid)
                return item;
        }
        return null;
    }
    public void RemovePlayer(PlayerBase player)
    {
        PlayerBase p = FindPlayer(player.UID);
        PlayerList.Remove(p);

        Destroy(p.GO); //Destroy
    }

    //Player p1 = PlayerManager.Instance.AddPlayer("0", "Hero", "PlayerHero", );
    //p1.GO.transform.position = new Vector3(2, 0, 0);

    public PlayerBase AddPlayer(string uid, PlayerType type, string prefab)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>(prefab)); //Instantiate
        PlayerBase player = null;
        switch (type)
        {
            case PlayerType.Hero:
                player = go.GetComponent<PlayerUnit>();
                player.InitPlayer(uid);
                Hero = player as PlayerUnit;  // hero fix
                Debug.Log(Hero);
                break;
            case PlayerType.Enemy:
                player = go.GetComponent<PlayerEnemy>();
                player.InitPlayer(uid);
                break;

            default: // PlayerType.Base
                player = go.GetComponent<PlayerBase>();
                player.InitPlayer(uid);
                break;
        }
        PlayerList.Add(player);
        return player;
    }

}
