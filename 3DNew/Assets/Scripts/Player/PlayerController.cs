using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
 
    private Animator animator;
    private Rigidbody rigid;

    public Image healthBar;

    public GameObject rightFistCollision;
    public GameObject leftFistCollision;

    public GameObject rightLegCollision;
    public GameObject leftLegCollision;

    public GameObject playerHitParticle;
    public Transform  playerHitParticleLocation;

    private float maxHealth;
    private float health;

    public float speed = 4.0f;

    private Vector3 lookPosition;

    private Transform camera;

    private Vector3 cameraForward;
    private Vector3 move;
    private Vector3 moveInput;

    private float forwardAmount;
    private float turnAmount;

    private bool bMove;

    //콤보 공격 파트

    
    private void Start()
    {
        maxHealth = 50.0f;

        health = maxHealth;

        rigid = GetComponent<Rigidbody>();

        SetupAnimator();

        camera = Camera.main.transform;

        bMove = true;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100))
        {
            lookPosition = hit.point;
        }

        Vector3 lookDir = lookPosition - transform.position;
        lookDir.y = 0;

        transform.LookAt(transform.position + lookDir, Vector3.up);

        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Attack2();
        }

    }

    private void FixedUpdate()
    {

        if(bMove)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (camera != null)
            {
                cameraForward = Vector3.Scale(camera.up, new Vector3(1, 0, 1)).normalized;
                move = vertical * cameraForward + horizontal * camera.right;
            }
            else
            {
                move = vertical * Vector3.forward + horizontal * Vector3.right;
            }

            if (move.magnitude > 1)
            {
                move.Normalize();
            }

            Move(move);

            Vector3 movement = new Vector3(horizontal, 0, vertical);
            rigid.AddForce(movement * speed / Time.deltaTime);
        }
       

    }

    private void Move(Vector3 Move)
    {
        if(Move.magnitude > 1)
        {
            Move.Normalize();
        }

        this.moveInput = Move;

        ConvertMoveInput();
        
        UpdateAnimator();
    
    }

    private void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        turnAmount = localMove.x;

        forwardAmount = localMove.z;
    }

    //플레이어 움직일 경우 소리 출력 부분 

    public void PlayMoveSound()
    {
        AudioManager.Instance.PlaySFX("PlayerMoveSound");
    }

    //

    //콤보 공격 부분 
    #region ComboAttack

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    private void Attack2()
    {
        Debug.Log("발차기 공격 부분");
        //animator.SetTrigger("Attack2");
    }

    #endregion
    //

    //공격 콜라이더 부분 
    #region AttackCollision

    //주먹 부분 
    public void ActiveRightFistCollision()
    {
        rightFistCollision.SetActive(true);
        AudioManager.Instance.PlaySFX("HumanTypeAttackSound1");
        bMove = false;
    }

    public void DeActiveRightFistCollision()
    {
        rightFistCollision.SetActive(false);
        bMove = true;

    }

    public void ActiveLeftFistCollision()
    {
        leftFistCollision.SetActive(true);
        AudioManager.Instance.PlaySFX("HumanTypeAttackSound1");
        bMove = false;
    }

    public void DeActiveLeftFistCollision()
    {
        leftFistCollision.SetActive(false);
        bMove = true;
    }

    //다리 부분 

    public void ActiveRightLegCollision()
    {
        rightLegCollision.SetActive(true);
        bMove = false;
    }

    public void DeActiveRightLegCollision()
    {
        rightLegCollision.SetActive(false);
        bMove = true;
    }

    public void ActiveLeftLegCollision()
    {
        leftLegCollision.SetActive(true);
        bMove = false;
    }

    public void DeActiveLeftLegCollision()
    {
        leftLegCollision.SetActive(false);
        bMove = true;
    }

    #endregion

    private void UpdateAnimator()
    {
        animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
        animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
    }

    private void SetupAnimator()
    {
        animator = GetComponent<Animator>();

        foreach(var childAnimator in GetComponentsInChildren<Animator>())
        {
            if(childAnimator != animator)
            {
                animator.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;
            }
        }
    }

    public void Damage(float Damage)
    {
        health -= Damage;
        healthBar.fillAmount = health / 50.0f;
        AudioManager.Instance.PlaySFX("PlayerHitSound");

        GameObject obj = Instantiate(playerHitParticle, playerHitParticleLocation.position, Quaternion.identity);
        Destroy(obj, 2.0f);

        if (health <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Destroy(this.gameObject);

        GameManager.Instance.bPlayerDead = true;
    }

}
