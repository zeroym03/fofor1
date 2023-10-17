using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour  //상속 오버라이드
{
    //스탯은 다른클래스로 
    float speed = 10;
    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject[] grenades;
    [SerializeField] GameObject granadeobj;
    Camera follwouCamera;
   public  bool[] hasWeapons { get; set; } = new bool[4];

    public int ammo { get; set; } = 100;//프로포티 초기화 필요
    public int coin { get; set; } = 100;
    public int health { get; set; } = 300;
    public int hasGreandes { get; set; } = 0;
    public int maxammo { get; set; } = 300;
    public int maxhealth { get; set; } = 300;
    int maxcoin = int.MaxValue;
    int maxhasGreandes = 4;


    float fireDelay = 0f;

    public bool isShop { get; set; } = false;
    bool isJump;
    bool isDodge = false;
    bool isSwap = false;
    bool isFireReady = true;
    bool isBorder = false;
    bool isDamege = false;
    bool isDead = false;
    bool isReload = false;

    Vector3 moveVec;
    Vector3 DodgeVec;
    PlayerAni playerAni;
    Rigidbody plrigidbody;
    MeshRenderer[] meshes;
    GameObject nearobjeact;
    public Weapon equipWeapon;
    int equipWeaponIndex = -1;

    private void Awake()
    {
        PlayerGetComponent();
        print("Hero InitPlayer()");
    }
    private void Update()
    {
        if (!isDead)
        {
            PlayerMove();
            FreezeRotatoin();
            StopToWall();
            Dodge();
            playerAni.MoveAnime(moveVec);
            Attack();
            Reload();
            Swap();
            Interation();
            Granade();
        }
    }

    void PlayerGetComponent()
    {
        follwouCamera = GenericSinglngton<UIManager>.Instance.gameCam.GetComponent<Camera>();
        playerAni = GetComponent<PlayerAni>();
        plrigidbody = GetComponent<Rigidbody>();
        meshes = GetComponentsInChildren<MeshRenderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon" || other.tag == "Shop")
        {
            nearobjeact = other.gameObject;
            Debug.Log(isShop);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            nearobjeact = null;
        }
        else if (other.tag == "Shop")
        {
            Shop shop = other.gameObject.GetComponent<Shop>();
            shop.Exit();
            nearobjeact = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            PlayerByItem(other);
        }
        else if (other.tag == "EnemyBullet")
        {
            DamegeStart(other);
        }
    }
    void PlayerByItem(Collider other)
    {
        Item item = other.GetComponent<Item>();
        switch (item.type)
        {
            case Item.Type.Ammo:
                ammo += item.value;
                if (ammo > maxammo) { ammo = maxammo; }
                break;
            case Item.Type.Coin:
                coin += item.value;
                if (coin > maxcoin) { coin = maxcoin; }
                break;
            case Item.Type.Heart:
                health += item.value;
                if (health > maxhealth) { health = maxhealth; }
                break;
            case Item.Type.Grenade:
                grenades[hasGreandes].SetActive(true);
                hasGreandes += item.value;
                if (hasGreandes > maxhasGreandes) { hasGreandes = maxhasGreandes; }
                break;
        }
        Destroy(other.gameObject);
    }
    void DamegeStart(Collider other)
    {
        if (isDamege == false)
        {
            Debug.Log("EnemyBullet");
            Bullet enemyBullet = other.GetComponent<Bullet>();
            health -= enemyBullet.damage;
            bool isBossAtk = other.name == "Boss Melee Alea";
            StartCoroutine(OnDamege(isBossAtk));
        }
        if (other.GetComponent<Rigidbody>() != null) { Destroy(other.gameObject); }
    }
    IEnumerator OnDamege(bool isBossAtk)
    {
        if (health <= 0 && !isDead)
        {
            OnDie();
        }
        isDamege = true;
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.material.color = Color.red;
        }
        if (isBossAtk)
        {
            plrigidbody.AddForce(transform.forward * -25, ForceMode.Impulse);
        }
        yield return new WaitForSeconds(1);
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.material.color = Color.white;
        }
        isDamege = false;
        if (isBossAtk)
        {
            plrigidbody.velocity = Vector3.zero;
        }

    }
    void OnDie()
    {
        playerAni.DoDie();
        isDead = true;
        GenericSinglngton<GameManager>.Instance.GameOver();
    }
    void FreezeRotatoin()
    {
        plrigidbody.angularVelocity = Vector3.zero;
    }
    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.red);
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }

    void Granade()
    {
        if (hasGreandes == 0)
        { return; }
        if (GenericSinglngton<GetKeyCodeManager>.Instance._gDown && !isReload && !isSwap)
        {
            Ray ray = follwouCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Vector3 nextvec = hit.point - transform.position;
                nextvec.y = 13;
                GameObject instantGranade = Instantiate(granadeobj, transform.position, transform.rotation);
                Rigidbody rigidGranade = instantGranade.GetComponent<Rigidbody>();
                rigidGranade.AddForce(nextvec, ForceMode.Impulse);
                rigidGranade.AddTorque(Vector3.back * 10, ForceMode.Impulse);

                hasGreandes--;
                grenades[hasGreandes].SetActive(false);
            }
        }
    }
    void Reload()
    {
        if (equipWeapon == null) { return; }
        if (equipWeapon.type == Weapon.Type.Melee) { return; }
        if (ammo <= 0) { return; }
        if (isReload) { return; }
        if (equipWeapon.curAmmo == equipWeapon.maxAmmo) { return; }
        if (GenericSinglngton<GetKeyCodeManager>.Instance._rDown && isFireReady && isDodge == false && isSwap == false && isJump == false)
        {
            StartCoroutine(ReloadOut());
        }
    }
    IEnumerator ReloadOut()//리로딩 실질시스템
    {
        playerAni.DoReload();
        isReload = true;
        yield return new WaitForSeconds(1f);
        int reAmmo = ammo < equipWeapon.maxAmmo ? ammo : equipWeapon.maxAmmo;

        equipWeapon.curAmmo += reAmmo;
        if (equipWeapon.curAmmo > equipWeapon.maxAmmo)
        {
            ammo += equipWeapon.curAmmo - equipWeapon.maxAmmo;
            equipWeapon.curAmmo = equipWeapon.maxAmmo;
        }
        ammo -= reAmmo;
        isReload = false;
    }

    void Attack()
    {

        if (equipWeapon == null) return;
        Turn();
        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;
        if (GenericSinglngton<GetKeyCodeManager>.Instance._fDown && isFireReady && isDodge == false && isSwap == false && !isShop)
        {
            equipWeapon.Use();
            playerAni.WeaponTypeAttack(equipWeapon);
            fireDelay = 0;
        }
    }
    void PlayerMove()
    {if(isShop == false)
        {
            moveVec = new Vector3(GenericSinglngton<GetKeyCodeManager>.Instance._axisHorx, 0, GenericSinglngton<GetKeyCodeManager>.Instance._axisVerz).normalized;
            if (isDodge == true) { moveVec = DodgeVec; }
            if (isSwap == true || isReload || isFireReady == false) { moveVec = Vector3.zero; }
            if (isBorder == false) { transform.position += moveVec * speed * Time.deltaTime; }

            Turn();
        }
    }
    void Turn()
    {
        if (GenericSinglngton<GetKeyCodeManager>.Instance._fDown)
        {
            Ray ray = follwouCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Vector3 nex = hit.point - transform.position;
                nex.y = 0;
                transform.LookAt(transform.position + nex);
            }
        }
        transform.LookAt(transform.position + moveVec);
    }

    void Dodge()
    {
        if (GenericSinglngton<GetKeyCodeManager>.Instance._JumpDown && moveVec != Vector3.zero && isJump == false && !isSwap&& isDodge == false)
        {
            DodgeVec = moveVec;
            speed *= 2;
            playerAni.DoDodge();
            isDodge = true;
            StartCoroutine(DodgeOut());
        }
    }
    IEnumerator DodgeOut()
    {
        yield return new WaitForSeconds(0.6f);
        speed *= 0.5f;
        isDodge = false;
    }
    int weaponIndex = -1;

    void Swap()
    {
        if (GenericSinglngton<GetKeyCodeManager>.Instance._sDown1 && (hasWeapons[0] == false || equipWeaponIndex == 0)) { return; }
        if (GenericSinglngton<GetKeyCodeManager>.Instance._sDown2 && (hasWeapons[1] == false || equipWeaponIndex == 1)) { return; }
        if (GenericSinglngton<GetKeyCodeManager>.Instance._sDown3 && (hasWeapons[2] == false || equipWeaponIndex == 2)) { return; }

        if (GenericSinglngton<GetKeyCodeManager>.Instance._sDown1) { weaponIndex = 0; }
        if (GenericSinglngton<GetKeyCodeManager>.Instance._sDown2) { weaponIndex = 1; }
        if (GenericSinglngton<GetKeyCodeManager>.Instance._sDown3) { weaponIndex = 2; }

        if ((GenericSinglngton<GetKeyCodeManager>.Instance._sDown1 || GenericSinglngton<GetKeyCodeManager>.Instance._sDown2 || GenericSinglngton<GetKeyCodeManager>.Instance._sDown3)  && isDodge == false)//
        {
            if (equipWeapon != null) { equipWeapon.gameObject.SetActive(false); }
            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
           Debug.Log( equipWeapon.name);
            weapons[weaponIndex].SetActive(true);

            playerAni.DoSwap();
            isSwap = true;
            StartCoroutine(SwapOut());
        }
    }
    IEnumerator SwapOut()
    {
        yield return new WaitForSeconds(1);
        isSwap = false;
    }
    void Interation()
    {
        if (GenericSinglngton<GetKeyCodeManager>.Instance._iDown == true && nearobjeact != null && isJump == false && isDodge == false)
        {
            if (nearobjeact.tag == "Weapon")
            {
                Item item = nearobjeact.GetComponent<Item>();
                int weaponindex = item.value;
                Debug.Log(item.value);
                hasWeapons[weaponindex] = true;
                Destroy(nearobjeact);

            }
            else if (nearobjeact.tag == "Shop")
            {
                Shop shop = nearobjeact.GetComponent<Shop>();
                shop.Enter(this);
                //isShop = false;
                nearobjeact = null;
            }
        }
    }
}


//Quaternion deltaRot = Quaternion.Euler(Vector3.up * SpinSpeed * Time.deltaTime));
//Vector3 deltaPos = transform.forward * MoveSpeed * Time.deltaTime;
//rb.MoveRotation(rb.rotation * deltaRot);
//rb.MovePosition(transform.position + deltaPos);