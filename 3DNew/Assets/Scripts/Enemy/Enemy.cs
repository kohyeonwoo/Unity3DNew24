using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    public float health = 50.0f;
    public Animator animator;
    public void Damage(float Damage)
    {

        health -= Damage;

        if(health <= 0)
        {
            //적 죽음 애니메이션 출력 
            animator.SetTrigger("Die");
            GetComponent<Collider>().enabled = false;
            Invoke("Dissapear", 6.0f);
        }
        else
        {
            //적 피격 애니메이션 출력
            animator.SetTrigger("Damage");
        }

    } 

    private void Dissapear()
    {
        Destroy(gameObject);
    }

}
