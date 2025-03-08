using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //------------- 기본 사항 관련 변수 ----------------//
    public int routine;
    public float timer;
    public float time_Routine;
    public Animator anim;
    public Quaternion angle;
    public float grade;
    public GameObject target;
    public bool bAttack;
    public Boss2 boss2;
    public float speed;
    public GameObject[] hit;
    public int hit_Select;
    //-----------------------------//

    //==== 첫 번째 패턴 관련 변수 모음 (화염 공격)========//
    public bool bFireAttack;
    public List<GameObject> pool = new List<GameObject>();
    public GameObject fire;
    public GameObject attackPosition;
    private float timer2;
    //-----------------------------//

    //==== 두 번째 패턴 관련 변수 모음(점프 공격)===///
    public float jumpDistance;
    public bool directionSkill;
    //=============================================///

    // 화염구 부분 ////////
    public GameObject fireBall;
    public GameObject point;
    public List<GameObject> pool2 = new List<GameObject>();
    //===================///

    //==== 기타 부분 =====//

    public int fase = 1;
    public float hp_Min;
    public float hp_Max;
    public Image hpBar;
    public AudioSource music;
    public bool bDead;

    private void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        hpBar.fillAmount = hp_Min / hp_Max;

        if(hp_Min > 0)
        {
            BossFSM();
        }
        else
        {
            if(!bDead)
            {
                anim.SetTrigger("Dead");
                music.enabled = false;
                bDead = true;
            }
        }
    }

    public void FSM_Boss()
    {
        if(Vector3.Distance(transform.position, target.transform.position) < 15)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;

            var rotation = Quaternion.LookRotation(lookPos);
            point.transform.LookAt(target.transform.position);
            music.enabled = true;

            if(Vector3.Distance(transform.position, target.transform.position) > 1 && !bAttack)
            {
                switch(routine)
                {
                    case 0:
                        //걷기 부분 
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        anim.SetBool("Walk", true);
                        anim.SetBool("Run", false);

                        if(transform.rotation == rotation)
                        {
                            transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        }

                        anim.SetBool("bAttack", false);

                        timer += 1 * Time.deltaTime;

                        if(timer > time_Routine)
                        {
                            routine = Random.Range(0, 5);
                            timer = 0;
                        }
                        break;

                    case 1:
                        //달리기 부분 

                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        anim.SetBool("Walk", false);
                        anim.SetBool("Run", true);

                        if(transform.rotation == rotation)
                        {
                            transform.Translate(Vector3.forward * speed * 2 * Time.deltaTime);
                        }

                        anim.SetBool("Attack", false);
                        break;

                    case 2:
                        //불뿜기 공격 부분(화염방사)
                        anim.SetBool("Walk", false);
                        anim.SetBool("Run", false);
                        anim.SetBool("Attack", true);
                        anim.SetFloat("Skills", 0);

                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        boss2.GetComponent<CapsuleCollider>().enabled = false;

                        break;

                    case 3:
                        //점프 공격 부분 
                        if(fase == 2)
                        {
                            jumpDistance += 1 * Time.deltaTime;
                            anim.SetBool("Walk", false);
                            anim.SetBool("Run", false);
                            anim.SetBool("Attack", true);
                            anim.SetFloat("skills", 0);
                            hit_Select = 3;
                            boss2.GetComponent<CapsuleCollider>().enabled = false;

                            if(directionSkill)
                            {
                                if(jumpDistance < 1.0f)
                                {
                                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                                }

                                transform.Translate(Vector3.forward * 8 * Time.deltaTime);
                            }
                        }
                        else
                        {
                            routine = 0;
                            timer = 0;
                        }
                        
                        break;

                    case 4:
                        //화염구 공격 관련 부분 
                        if (fase == 2)
                        {
                            anim.SetBool("Walk", false);
                            anim.SetBool("Run", false);
                            anim.SetBool("Attack", true);
                            anim.SetBool("Walk", false);
                            anim.SetFloat("Skills", 0);
                            boss2.GetComponent<CapsuleCollider>().enabled = false;
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 0.5f);
                        }
                        else
                        {
                            routine = 0;
                            timer = 0;
                        }

                        break;

                }
            }
        }
    }

    //최후 관련 애니메이션
    public void FinalAnimation()
    {
        routine = 0;
        anim.SetBool("Attack", false);
        bAttack = false;
        boss2.GetComponent<CapsuleCollider>().enabled = true;
        bFireAttack = false;
        jumpDistance = 0;
        directionSkill = false;
    }

    //한방향 공격 시작

    public void DirectionAttackStart()
    {
        directionSkill = true;
    }

    //한방향 공격 끝
    public void DirectionAttackFinal()
    {
        directionSkill = false;
    }

    //근접 공격 부분 

    public void ColliderWeaponTrue()
    {
        hit[hit_Select].GetComponent<SphereCollider>().enabled = true;
    }

    public void ColliderWeaponFalse()
    {
        hit[hit_Select].GetComponent<SphereCollider>().enabled = false;
    }

    //화염 공격 부분 (범위 공격 부분 )

    public GameObject GetFireAttack()
    {
        for(int i = 0; i < pool.Count; i++)
        {
            if(!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        GameObject obj = Instantiate(fire, attackPosition.transform.position, attackPosition.transform.rotation) as GameObject;
        pool.Add(obj);
        return obj;
    }


    public void FireAttack_Skill()
    {
        timer2 += 1 * Time.deltaTime;

        if(timer2 > 0.1f)
        {
            GameObject obj = GetFireAttack();

            obj.transform.position = attackPosition.transform.position;
            obj.transform.rotation = attackPosition.transform.rotation;

            timer2 = 0;
        }
    }

    public void StartFire()
    {
        bFireAttack = true;
    }

    public void StopFire()
    {
        bFireAttack = false;
    }

    //화염구 공격 부분 

    public GameObject GetFireBall()
    {
        for(int i = 0; i < pool2.Count; i++)
        {
            if(!pool2[i].activeInHierarchy)
            {
                pool2[i].SetActive(true);
                return pool2[i];
            }
        }

        GameObject obj = Instantiate(fireBall, point.transform.position, point.transform.rotation) as GameObject;
        pool2.Add(obj);
        return obj;
    }

    public void FireBallSkill()
    {
        GameObject obj = GetFireBall();
        obj.transform.position = point.transform.position;
        obj.transform.rotation = point.transform.rotation;
    }
    /////
    
    //=======보스가 살아있는 경우========///
    public void BossFSM() //Vivo
    {
        if(hp_Min < 500)
        {
            fase = 2;
            time_Routine = 1;
        }

        FSM_Boss();

        if(bFireAttack)
        {
            FireAttack_Skill();
        }
    }

}
