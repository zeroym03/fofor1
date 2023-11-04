using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerTestUnit : MonoBehaviour
{
    public PlayerMoveState playerMoveState { get; } = new PlayerMoveState();
    public PlayerDodgeState playerDodgeState { get; } = new PlayerDodgeState();
    public InterationState interationState { get; } = new InterationState();
    public AttackState attackState { get; } = new AttackState();
    public SwapState swapState { get; } = new SwapState();
    public ReloadState ReloadState { get; } = new ReloadState();
    public DieState dieState { get; } = new DieState();
    public GranadeState granadeState { get; } = new GranadeState();




    PlayerState playerState;
    public PlayerAni playerAni { get; set; }
    PlayerUnitData unitData { get; set; }
    private void Awake()
    {
        playerAni = GetComponent<PlayerAni>();
        unitData = GetComponent<PlayerUnitData>();
        unitData.follwouCamera = GenericSinglngton<UIManager>.Instance.gameCam.GetComponent<Camera>();
    }
    private void Start()
    {
        unitData.meshes = this.gameObject.GetComponentsInChildren<MeshRenderer>();
        SetState(playerMoveState);
    }
    private void Update()
    {
        unitData.fireDelay += Time.deltaTime;//attack 타이머
        Debug.Log(playerState);
        playerState.StateUpDate();
    }
    public void SetState(PlayerState state)
    {
        playerState = state;
        playerState.StateStart(this);
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
            unitData.nearobjeact = null;
        }
    }
    private void OnTriggerEnter(Collider other)//아이템에 부딛치면 그아이템획득,효과발생,적공격이면 데미지입는함수실행
    {
        if (other.tag == "Item")
        {
            PlayerByItem(other);//
        }
        else if (other.tag == "EnemyBullet")
        {
            DamegeStart(other);// 우야냐
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
    public void PlayerByItem(Collider other)
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
        if (Input.GetKey(KeyCode.Mouse0) == false)
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
        if (unitData.DodgeVec == Vector3.zero)
        {
            transform.position += this.gameObject.transform.forward * unitData.speed * Time.deltaTime;
        }
        transform.position += unitData.DodgeVec * unitData.speed * Time.deltaTime;
    }
    IEnumerator DodgeOut()
    {
        yield return new WaitForSeconds(0.6f);
        unitData.speed *= 0.5f;
        if (unitData.isDead == false)
            SetState(playerMoveState); // 매번 new 를 안해야 좋을거같은데 어떻게 해야할까
    }
    public void Interation()
    {
        if (unitData.nearobjeact == null) { SetState(playerMoveState); return; }
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
            shop.Enter(this);
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
        yield return new WaitForSeconds(0.7f);
        SetState(playerMoveState);

    }
    public void Attack()
    {
        if (unitData.equipWeapon == null) { SetState(playerMoveState); return; }
        unitData.isFireReady = unitData.equipWeapon.rate < unitData.fireDelay;
        Turn();
        Debug.Log("asd");
        if (unitData.isFireReady)
        {
            unitData.equipWeapon.Use();
            if (unitData.vAMSWeapon != null)
                unitData.vAMSWeapon.VAMSAttack();
            playerAni.WeaponTypeAttack(unitData.equipWeapon);
            unitData.fireDelay = 0;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0)) SetState(playerMoveState);

    }
    public void Reload()//리로딩 조건
    {
        if (unitData.equipWeapon == null)                                   { SetState(playerMoveState); return; } //무기를 들고있는냐
        if (unitData.equipWeapon.type == Weapon.Type.Melee)                 { SetState(playerMoveState); return; } //근거리무기냐
        if (unitData.ammo <= 0)                                             { SetState(playerMoveState); return; } //현재 가지고있는 총알개수가 0개미만이냐
        if (unitData.isReload)                                              { SetState(playerMoveState); return; } //리로딩중이냐
        if (unitData.equipWeapon.curAmmo == unitData.equipWeapon.maxAmmo)   { SetState(playerMoveState); return; } //현재 장전된 탄환이 최대 장전량과 같으냐
        StartCoroutine(ReloadOut());
    }
    IEnumerator ReloadOut()//리로딩 실질시스템
    {
        playerAni.DoReload();
        //isReload = true;
        yield return new WaitForSeconds(2f);
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

    public void OnDie()
    {
        if (unitData.isDead == false)
        {
            playerAni.DoDie();
            unitData.isDead = true;
            GenericSinglngton<GameManager>.Instance.GameOver();
        }
    }
   
    public void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.red);
        unitData.isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }
    public void Granade()
    {
        if (unitData.hasGreandes == 0)
        { SetState(playerMoveState); return; }

        Ray ray = unitData.follwouCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Vector3 nextvec = hit.point - transform.position;
            nextvec.y = 13;
            GameObject instantGranade = Instantiate(unitData.granadeobj, transform.position, transform.rotation);
            Rigidbody rigidGranade = instantGranade.GetComponent<Rigidbody>();
            rigidGranade.AddForce(nextvec, ForceMode.Impulse);
            rigidGranade.AddTorque(Vector3.back * 10, ForceMode.Impulse);

            unitData.hasGreandes--;
            unitData.grenades[unitData.hasGreandes].SetActive(false);
        }
        SetState(playerMoveState);
    }
}
