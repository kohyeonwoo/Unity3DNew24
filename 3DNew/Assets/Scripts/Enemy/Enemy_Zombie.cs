using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Zombie : MonoBehaviour
{
    public GameObject deadBody;

    private Rigidbody rigid;
    private Animator anim;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        deadBody.transform.position = this.transform.position;
    }

    private void Dead()
    {
        deadBody.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "AttackPart")
        {
            Dead();
        }
    }

}
