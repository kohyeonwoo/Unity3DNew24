using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    public float health = 50.0f;
    
    public Animator animator;
    public Rigidbody rigid;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    public void Damage(float Damage)
    {

        health -= Damage;
        Debug.Log("한대 맞았음!");

        if(health <= 0)
        {
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

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "AttackPart")
        {
            Debug.Log("플레이어의 공격에 맞았습니다.");
        }
    }

}
