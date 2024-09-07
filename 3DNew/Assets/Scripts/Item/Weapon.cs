using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Projectile, Sword, Meteo,}

public struct Data
{
    public float attackPoint;
}

public abstract class Weapon : MonoBehaviour
{
    public WeaponType weaponTypes;
    public Data data;

    public abstract void InitSetting();

}
