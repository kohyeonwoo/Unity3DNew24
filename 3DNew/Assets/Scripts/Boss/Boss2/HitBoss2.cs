using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoss2 : MonoBehaviour
{

    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(damage);
            }
        }    
    }
}
