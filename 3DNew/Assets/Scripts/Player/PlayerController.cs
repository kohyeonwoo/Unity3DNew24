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

    public float speed = 4.0f;

    private Vector3 lookPosition;

    private Transform camera;

    private Vector3 cameraForward;
    private Vector3 move;
    private Vector3 moveInput;

    private float forwardAmount;
    private float turnAmount;

    private void Start()
    {
        maxHealth = 50.0f;

        health = maxHealth;

        rigid = GetComponent<Rigidbody>();

        SetupAnimator();

        camera = Camera.main.transform;
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

    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(camera != null)
        {
            cameraForward = Vector3.Scale(camera.up, new Vector3(1, 0, 1)).normalized;
            move = vertical * cameraForward + horizontal * camera.right;
        }
        else
        {
            move = vertical * Vector3.forward + horizontal * Vector3.right;
        }

        if(move.magnitude > 1)
        {
            move.Normalize();
        }

        Move(move);

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        rigid.AddForce(movement * speed / Time.deltaTime);

     
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
