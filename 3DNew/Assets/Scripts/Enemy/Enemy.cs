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
            //�� ���� �ִϸ��̼� ��� 
            animator.SetTrigger("Die");
            GetComponent<Collider>().enabled = false;
            Invoke("Dissapear", 6.0f);
        }
        else
        {
            //�� �ǰ� �ִϸ��̼� ���
            animator.SetTrigger("Damage");
        }

    } 

    private void Dissapear()
    {
        Destroy(gameObject);
    }

}
