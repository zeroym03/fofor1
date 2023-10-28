using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerTestUnit : MonoBehaviour
{
   public  PlayerMoveState playerMoveState;
    public PlayerDodgeState playerDodgeState;
    public InterationState interationState;
    public AttackState attackState;
    public SwapState swapState;
   public ReloadState ReloadState;
   public DieState dieState;
    PlayerState playerState;
    public PlayerAni playerAni { get; set; }
    public PlayerUnitData unitData { get; set; }
    private void Awake()
    {
        playerAni = GetComponent<PlayerAni>();
        unitData = GetComponent<PlayerUnitData>();
    }
    private void Start()
    {
        SetState(new PlayerMoveState());
    }
    private void Update()
    {
        unitData.fireDelay += Time.deltaTime;//attack 타이머

        playerState.StateUpDate();
    }
    public void SetState(PlayerState state)
    {
        playerState = state;
        playerState.StateStart(this);
        if (playerMoveState == null) { playerMoveState = new PlayerMoveState(); }
        if (playerDodgeState == null) playerDodgeState = new PlayerDodgeState();
        if (interationState == null) interationState = new InterationState();
        if (attackState == null) attackState = new AttackState();
        if (swapState == null) swapState = new SwapState();
        if (ReloadState == null) ReloadState = new ReloadState ();
        if (dieState == null) dieState = new DieState();
    }

    private void OnTriggerStay(Collider other) // 샵에 들어가있냐 무기를 얻을수있냐
    {
        if (other.tag == "Weapon" || other.tag == "Shop")
        {
            unitData.nearobjeact = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)//무기,샵에서 나갔으니 nearobjeact를 널로
    {
        if (other.tag == "Weapon")
        {
            unitData.nearobjeact = null;
        }
        else if (other.tag == "Shop")
        {
            //Shop shop = other.gameObject.GetComponent<Shop>();
            // shop.Exit();
            unitData. nearobjeact = null;
        }
    }

    public void DestroyGMOB(GameObject gameObject)
    {
        Destroy(gameObject);
    }
    public void PlayerMove()
    {
        unitData.moveVec = new Vector3(GenericSinglngton<GetKeyCodeManager>.Instance._axisHorx, 0, GenericSinglngton<GetKeyCodeManager>.Instance._axisVerz).normalized;
        if (unitData.isBorder == false)
        {
            transform.position += unitData.moveVec * unitData.speed * Time.deltaTime;
            playerAni.MoveAnime(unitData.moveVec);
        }

        Turn();
    }//Move
    void Turn()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log(unitData.follwouCamera);
            Ray ray = unitData.follwouCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Vector3 nex = hit.point - unitData.transform.position;
                nex.y = 0;
                transform.LookAt(transform.position + nex);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) == false)
            transform.LookAt(transform.position + unitData.moveVec);
    }//Move
  
    public void Dodge()//Dodge
    {
        unitData.speed *= 2;
        unitData.StartCoroutine(DodgeOut());
        playerAni.DoDodge();
        unitData.DodgeVec = new Vector3(GenericSinglngton<GetKeyCodeManager>.Instance._axisHorx, 0, GenericSinglngton<GetKeyCodeManager>.Instance._axisVerz).normalized;
    }
    public void DodgeMove()//Dodge
    {
        transform.position += unitData.DodgeVec * unitData.speed * Time.deltaTime;
    }
    IEnumerator DodgeOut()
    {
        yield return new WaitForSeconds(0.6f);
        unitData.speed *= 0.5f;
        SetState(playerMoveState); // 매번 new 를 안해야 좋을거같은데 어떻게 해야할까
    }
    public void Interation()
    {
         if(unitData.nearobjeact == null) { SetState(playerMoveState);return; }
        if (unitData.nearobjeact.tag == "Weapon")
        {
            Item item = unitData.nearobjeact.GetComponent<Item>();
            int weaponindex = item.value;
            unitData.hasWeapons[weaponindex] = true;
            DestroyGMOB(unitData.nearobjeact);
        }
        else if (unitData.nearobjeact.tag == "Shop")
        {
            Shop shop = unitData.nearobjeact.GetComponent<Shop>();
            //shop.Enter(this);
            unitData.nearobjeact = null;
        }
        SetState(playerMoveState);
    }
    public void Swap()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && (unitData.hasWeapons[0] == false || unitData.equipWeaponIndex == 0)) { SetState(playerMoveState); return; }
        if (Input.GetKeyDown(KeyCode.Alpha2) && (unitData.hasWeapons[1] == false || unitData.equipWeaponIndex == 1)) { SetState(playerMoveState); return; }
        if (Input.GetKeyDown(KeyCode.Alpha3) && (unitData.hasWeapons[2] == false || unitData.equipWeaponIndex == 2)) { SetState(playerMoveState); return; }

        if (Input.GetKeyDown(KeyCode.Alpha1)) { unitData.weaponIndex = 0; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { unitData.weaponIndex = 1; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { unitData.weaponIndex = 2; }


        if (unitData.equipWeapon != null) { unitData.equipWeapon.gameObject.SetActive(false); }
        unitData.equipWeaponIndex = unitData.weaponIndex;

        unitData.equipWeapon = unitData.weapons[unitData.weaponIndex].GetComponent<Weapon>();
        if (unitData.weapons[unitData.weaponIndex].GetComponent<HandGunUP>() != null)
        {
            unitData.vAMSWeapon = unitData.weapons[unitData.weaponIndex].GetComponent<HandGunUP>();
        }
        else if (unitData.weapons[unitData.weaponIndex].GetComponent<HandGunUP>() == null)
        {
            unitData.vAMSWeapon = null;
        }
        unitData.weapons[unitData.weaponIndex].SetActive(true);
        playerAni.DoSwap();
        StartCoroutine(SwapOut());
    }
    IEnumerator SwapOut()
    {
        yield return new WaitForSeconds(0.4f);
        SetState(playerMoveState);

    }
    public void Attack()
    {

        if (unitData.equipWeapon == null) { SetState(playerMoveState);  return; }
        unitData.isFireReady = unitData.equipWeapon.rate < unitData.fireDelay;
        if (unitData.isFireReady)
        {
            Turn();
            unitData.equipWeapon.Use();
            if (unitData.vAMSWeapon != null)
                unitData.vAMSWeapon.VAMSAttack();
            playerAni.WeaponTypeAttack(unitData.equipWeapon);
            unitData.fireDelay = 0;
        }
      if(Input.GetKeyUp(KeyCode.Mouse0))  SetState(playerMoveState);
    }
   public void Reload()
    {
        if (unitData.equipWeapon == null) { SetState(playerMoveState); return; }
        if (unitData.equipWeapon.type == Weapon.Type.Melee) { SetState(playerMoveState); return; }
        if (unitData.ammo <= 0) { SetState(playerMoveState); return; }
        if (unitData.isReload) { SetState(playerMoveState); return; }
        if (unitData.equipWeapon.curAmmo == unitData.equipWeapon.maxAmmo) {  SetState(playerMoveState); return; }
        StartCoroutine(ReloadOut());
    }
    IEnumerator ReloadOut()//리로딩 실질시스템
    {
        playerAni.DoReload();
        //isReload = true;
        yield return new WaitForSeconds(1f);
        int reAmmo = unitData.ammo < unitData.equipWeapon.maxAmmo ? unitData.ammo : unitData.equipWeapon.maxAmmo;

        unitData.equipWeapon.curAmmo += reAmmo;
        if (unitData.equipWeapon.curAmmo > unitData.equipWeapon.maxAmmo)
        {
            unitData.ammo += unitData.equipWeapon.curAmmo - unitData.equipWeapon.maxAmmo;
            unitData.equipWeapon.curAmmo = unitData.equipWeapon.maxAmmo;
        }
        unitData.ammo -= reAmmo;
        SetState(playerMoveState);
    }
    private void OnTriggerEnter(Collider other)//아이템에부디치면 그아이템획득,효과발생,적공격이면 데미지입는함수실행
    {
        if (other.tag == "Item")
        {
                 PlayerByItem(other);
        }
        else if (other.tag == "EnemyBullet")
        {
                  DamegeStart(other);
            Debug.Log("DoDie2");
        }
    }
    void DamegeStart(Collider other)
    {
        if (unitData.isDamege == false)
        {
            Debug.Log("EnemyBullet");
            Bullet enemyBullet = other.GetComponent<Bullet>();
            unitData.health -= enemyBullet.damage;
            bool isBossAtk = other.name == "Boss Melee Alea";
            Debug.Log(isBossAtk);
            StartCoroutine(OnDamege(isBossAtk));
        }
        if (other.GetComponent<Rigidbody>() != null) { Destroy(other.gameObject); }
    }
    IEnumerator OnDamege(bool isBossAtk)
    {
        if (unitData.health <= 0)
        {
            Debug.Log("DoDie1");
            SetState(dieState);
        }
        unitData.isDamege = true;
        foreach (MeshRenderer mesh in unitData.meshes)
        {
            mesh.material.color = Color.red;
        }
        if (isBossAtk)
        {
            unitData.plrigidbody.AddForce(transform.forward * -25, ForceMode.Impulse);
        }
        yield return new WaitForSeconds(1);
        foreach (MeshRenderer mesh in unitData.meshes)
        {
            mesh.material.color = Color.white;
        }
        unitData.isDamege = false;
        if (isBossAtk)
        {
            unitData.plrigidbody.velocity = Vector3.zero;
        }

    }
 public   void OnDie()
    {
        if (unitData.isDead == false)
        {
            Debug.Log("DoDie");
            playerAni.DoDie();
            unitData.isDead = true;
            GenericSinglngton<GameManager>.Instance.GameOver();
        }
    }
    void PlayerByItem(Collider other)
    {
        Item item = other.GetComponent<Item>();
        switch (item.type)
        {
            case Item.Type.Ammo:
                unitData.ammo += item.value;
                if (unitData.ammo > unitData.maxammo) { unitData.ammo = unitData.maxammo; }
                break;
            case Item.Type.Coin:
                unitData.coin += item.value;
                if (unitData.coin > unitData.maxcoin) { unitData.coin = unitData.maxcoin; }
                break;
            case Item.Type.Heart:
                unitData.health += item.value;
                if (unitData.health > unitData.maxhealth) { unitData.health = unitData.maxhealth; }
                break;
            case Item.Type.Grenade:
                unitData.grenades[unitData.hasGreandes].SetActive(true);
                unitData.hasGreandes += item.value;
                if (unitData.hasGreandes > unitData.maxhasGreandes) { unitData.hasGreandes = unitData.maxhasGreandes; }
                break;
        }
        Destroy(other.gameObject);
    }
}
