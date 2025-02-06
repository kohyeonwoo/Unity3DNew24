using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    public GameObject attackCollision;

    public float health = 10.0f;
    
    public Animator animator;
    public Rigidbody rigid;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    public void ActiveAttackCollision()
    {
        attackCollision.SetActive(true);
    }

    public void DeActiveAttackCollision()
    {
        attackCollision.SetActive(false);
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
        this.gameObject.SetActive(false);
        GetComponent<Collider>().enabled = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "AttackPart")
        {
            Debug.Log("플레이어의 공격에 맞았습니다.");
        }
    }

}
