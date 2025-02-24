using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    private int attackPoint;

    public GameObject particleEffect;

    private void Start()
    {
        attackPoint = 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(attackPoint);
                Debug.Log("적이 맞았습니다");
                GameObject obj = Instantiate(particleEffect, transform.position,Quaternion.identity);
                Destroy(obj, 2.0f);
            }
        }
    }

}
