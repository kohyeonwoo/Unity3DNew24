using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public VariableJoystick joystick;
    public float speed;

    public GameObject buffObject;
    public GameObject areaAttackObject;
    public Transform muzzleObject;
    public GameObject projectileObject;

    private Rigidbody rigidBody;
    private Animator animator;
    private Vector3 moveVector;

    public float attack1Duration = 5.0f;
    public Projector t;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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

    public void ActivePowerUp()
    {
        animator.SetBool("bPowerUp", true);
        buffObject.SetActive(true);
    }

    public void DeActivePowerUp()
    {
        animator.SetBool("bPowerUp", false);
        buffObject.SetActive(false);
    }

    public void ActiveTurnAttack()
    {
       // animator.SetBool("bTurnAttack", true);
        Fire();
    }

    public void DeActiveTurnAttack()
    {
       // animator.SetBool("bTurnAttack", false);
    }

    public void ActiveAreaAttack()
    {
        areaAttackObject.SetActive(true);
        Invoke("DeActiveAreaAttack", 4.95f);
    }

    public void DeActiveAreaAttack()
    {
        areaAttackObject.SetActive(false);
    }


    public void Fire()
    {
        GameObject bulletObject = (GameObject)Instantiate(projectileObject, muzzleObject.position, muzzleObject.rotation);
        Projectile projectile = bulletObject.GetComponent<Projectile>();
        StartCoroutine(AttackFirst());
    }


    private IEnumerator AttackFirst()
    {
        float runningTime = attack1Duration;

        t.gameObject.SetActive(true);
        t.orthographicSize = 0.1f;

        while(runningTime > 0.0f)
        {
            runningTime -= Time.deltaTime;
            t.orthographicSize += 3.2f * Time.deltaTime;
            yield return null;
        }

        t.gameObject.SetActive(false);

    }

}
