using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type {
        Melee, Range , Gun
    };
    public Type type { get; set; }
    public int damege { get; set; }
    public int maxAmmo;
    public int curAmmo;
    public float rate;
 


    public virtual void Use()
    {
        Debug.Log("Null");
    }
}
