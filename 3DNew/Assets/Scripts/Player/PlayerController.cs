using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public VariableJoystick joystick;
    public float speed;

    public float maxHealth;
    public float health;

    public GameObject humanAttackCollision;
    public List<GameObject> stingAttackCollision = new List<GameObject>();
    public List<GameObject> bearAttackCollision = new List<GameObject>();
    public List<GameObject> whaleAttackCollision = new List<GameObject>();

    public List<GameObject> changeList = new List<GameObject>();

    [SerializeField]
    private Animator animator;
    private Rigidbody rigidBody;
    private Vector3 moveVector;

    public float attack1Duration = 5.0f;
    public Projector t;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        maxHealth = 100.0f;
        health = maxHealth;

    }

    private void FixedUpdate()
    {

        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        moveVector = new Vector3(x, 0, z) * speed * Time.fixedDeltaTime;
        rigidBody.MovePosition(rigidBody.position + moveVector);

        if(moveVector.sqrMagnitude == 0)
        {
            return;
        }

        Quaternion dirQuat = Quaternion.LookRotation(moveVector);
        Quaternion moveQuat = Quaternion.Slerp(rigidBody.rotation, dirQuat, 0.3f);
        rigidBody.MoveRotation(moveQuat);

    }

    private void LateUpdate()
    {
       animator.SetFloat("moveFloat", moveVector.sqrMagnitude);
    }

    public void Damage(float Damage)
    {
        health -= Damage;

        if (health <= 0)
        {
            Dead();
        }

    }

    //플레이어 ----------> 사람 형태 관련 내용 

    public void HumanTypeAttack()
    {
        animator.SetTrigger("Attack");
        Debug.Log("플레이어의 인간 상태일 때의 공격입니다!");
        
    }

    public void HumanTypeAttackCollisionActive()
    {
        humanAttackCollision.SetActive(true);
        AudioManager.Instance.PlaySFX("HumanTypeAttackSound1");
    }

    public void HumanTypeAttackCollisionDeActive()
    {
        humanAttackCollision.SetActive(false);
    }

    public void HumanSingleFootStepSound()
    {
        AudioManager.Instance.PlaySFX("FootStepSound_Grass");
    }

    //////////////////////////////////////////////////////


    /////// 말벌 형태일 때의 내용///////////////////////////
    public void StingTypeAttack()
    {
        animator.SetTrigger("Attack");
        Debug.Log("플레이어의 말벌 상태일 때의 공격입니다!");   
    }

    public void StingTypeAttackCollisionActive()
    {
        stingAttackCollision[2].SetActive(true);
    }

    public void StingTypeAttackCollisionDeActive()
    {
        stingAttackCollision[2].SetActive(false);
    }

    public void StingTypeAttack2CollisionActive()
    {
        stingAttackCollision[2].SetActive(true);
    }

    public void StingTypeAttack2CollisionDeActive()
    {
        stingAttackCollision[2].SetActive(false);
    }

    public void StingTypeAttack3CollisionActive()
    {
        stingAttackCollision[2].SetActive(true);
    }

    public void StingTypeAttack3CollisionDeActive()
    {
        stingAttackCollision[2].SetActive(false);
    }

    //////////////////////////////////////////////////////

    /////// 곰 형태일 때의 내용///////////////////////////
    public void BearTypeAttack()
    {
        animator.SetTrigger("Attack");
        Debug.Log("플레이어의 곰 상태일 때의 공격입니다!");
    }

    public void BearTypeAttackCollisionActive()
    {

    }

    public void BearTypeAttackCollisionDeActive()
    {

    }

    //////////////////////////////////////////////////////

    /////// 고래 형태일 때의 내용///////////////////////////
    public void WhaleTypeAttack()
    {
        animator.SetTrigger("Attack");
        Debug.Log("플레이어의 고래 상태일 때의 공격입니다!");
    }

    public void WhaleTypeAttackCollisionActive()
    {

    }

    public void WhaleTypeAttackCollisionDeActive()
    {

    }

    //////////////////////////////////////////////////////


    /////// 형태 변환 관련 내용 ////////////////////////////

  
    ////////////////////////////////////////////////////

    public void Dead()
    {

    }


    //public void ActivePowerUp()
    //{
    //    animator.SetBool("bPowerUp", true);
    //    buffObject.SetActive(true);
    //}

    //public void DeActivePowerUp()
    //{
    //    animator.SetBool("bPowerUp", false);
    //    buffObject.SetActive(false);
    //}

    //public void ActiveTurnAttack()
    //{
    //   // animator.SetBool("bTurnAttack", true);
    //    Fire();
    //}

    //public void DeActiveTurnAttack()
    //{
    //   // animator.SetBool("bTurnAttack", false);
    //}

    //public void ActiveAreaAttack()
    //{
    //    areaAttackObject.SetActive(true);
    //    Invoke("DeActiveAreaAttack", 4.95f);
    //}

    //public void DeActiveAreaAttack()
    //{
    //    areaAttackObject.SetActive(false);
    //}


    //public void Fire()
    //{
    //    GameObject bulletObject = (GameObject)Instantiate(projectileObject, muzzleObject.position, muzzleObject.rotation);
    //    Projectile projectile = bulletObject.GetComponent<Projectile>();
    //    StartCoroutine(AttackFirst());
    //}


    //private IEnumerator AttackFirst()
    //{
    //    float runningTime = attack1Duration;

    //    t.gameObject.SetActive(true);
    //    t.orthographicSize = 0.1f;

    //    while(runningTime > 0.0f)
    //    {
    //        runningTime -= Time.deltaTime;
    //        t.orthographicSize += 3.2f * Time.deltaTime;
    //        yield return null;
    //    }

    //    t.gameObject.SetActive(false);

    //}


}
