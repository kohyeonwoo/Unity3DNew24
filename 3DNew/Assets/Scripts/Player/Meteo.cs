using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : Weapon
{
    public override void InitSetting()
    {
        data.attackPoint = 13.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
