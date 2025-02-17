using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
 
    private Animator animator;
    private Rigidbody rigid;

    private float maxHealth;
    private float health;

    public float moveSpeed;

    private float horizontal;
    private float vertical;
    private bool moveInput;

    Vector3 v;
    Vector3 h;
    Vector3 movement;
   

    private void Awake()
    {
        maxHealth = 50.0f;

        health = maxHealth;

        rigid = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();   
    }

    private void Update()
    {
        GetInput();
        Move();
    }

    private void GetInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        v = vertical * Camera.main.transform.up;
        h = horizontal * Camera.main.transform.right;

        v.y = 0;
        h.y = 0;

        movement = (h + v).normalized;
    }
   
    private void Move()
    {
        rigid.MovePosition(transform.position + movement * moveSpeed);
    }

    public void Damage(float Damage)
    {
        health -= Damage;

        if(health <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Destroy(this.gameObject);
    }

}
