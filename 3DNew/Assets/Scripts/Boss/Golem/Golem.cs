using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour, IDamageable
{

    private Animator animator;

    private void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    public void EndAttack1()
    {
        animator.SetBool("attack1", false);
    }

    public void EndAttack2()
    {
        animator.SetBool("attack2", false);
    }

    public void Damage(float Damage)
    {
        
    }

}
