using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackPart : MonoBehaviour
{
  
    private int attackPoint;

    private void Start()
    {
        attackPoint = 5;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(attackPoint);
                Debug.Log("적이 맞았습니다");
            }
        }
    }
}
