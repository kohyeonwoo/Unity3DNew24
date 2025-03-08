using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{

    public Animator anim;
    public Boss boss;
    public int melee;

    private void OnTriggerEnter(Collider other)
    {
        melee = Random.Range(0, 4);

        switch(melee)
        {
            case 0:
                //1번째 패턴
                anim.SetFloat("skills", 0);
                boss.hit_Select = 0;
                break;
            case 1:
                //2번째 패턴
                anim.SetFloat("skills", 0);
                boss.hit_Select = 1;
                break;
            case 2:
                //점프 패턴
                anim.SetFloat("skills", 0);
                boss.hit_Select = 2;
                break;
            case 3:
                //화염구 패턴
                if(boss.fase == 2)
                {
                    anim.SetFloat("skills", 0);
                }
                else
                {
                    melee = 0;
                }
                break;
        }
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);
        anim.SetBool("Attack", true);
        boss.bAttack = true;
        GetComponent<CapsuleCollider>().enabled = false;
    }

}
