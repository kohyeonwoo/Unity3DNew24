using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{

    private List<GameObject> bulletPoolObject = new List<GameObject>();

    [SerializeField]
    private int amountToPool = 10;

    public VariableJoystick joystick;
    public float speed;
    public float bulletSpeed;

    public float maxHealth;
    public float health;

    public Slider healthBar;

    public GameObject humanAttackCollision;
    public GameObject bulletPrefab;
    public GameObject muzzleLocation;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody rigidBody;
    private Vector3 moveVector;

    public float attack1Duration = 5.0f;



    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        maxHealth = 100.0f;
        health = maxHealth;

        bulletSpeed = 5.0f;

        healthBar.value = (float)health / (float)maxHealth;

        CreateBulletPool();

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

        HandleHp();

    }

    private void LateUpdate()
    {
       animator.SetFloat("moveFloat", moveVector.sqrMagnitude);
    }

    private void HandleHp()
    {
        healthBar.value = (float)health / (float)maxHealth;
    }

    public void Damage(float Damage)
    {
        health -= Damage;

        HandleHp();

        if (health <= 0)
        {
            Dead();
        }

    }

    public void CreateBulletPool()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            bulletPoolObject.Add(obj);
        }
    }

    public GameObject GetBulletPoolObject()
    {
       for (int i = 0; i < bulletPoolObject.Count; i++)
       {
            if (!bulletPoolObject[i].activeInHierarchy)
            {
                return bulletPoolObject[i];
            }
       }

        return null;
    }

    private void SpawnBulletMode()
    {

        GameObject objects = GetBulletPoolObject();

        if (objects != null)
        {
            objects.transform.position = muzzleLocation.transform.position;
            objects.transform.rotation = muzzleLocation.transform.rotation;
            objects.SetActive(true);
        }
    }

    ////////////////////////////////////////////////////////////////

    public void HumanTypeAttack()
    {
        animator.SetTrigger("Attack");
        Debug.Log("플레이어의 인간 상태일 때의 공격입니다!");
        
    }

    public void HumanTypeAttackCollisionActive()
    {
        humanAttackCollision.SetActive(true);
        AudioManager.Instance.PlaySFX("HumanTypeAttackSound1");
        SpawnBulletMode();
        //GameObject bullet = Instantiate(bulletPrefab, muzzleLocation.transform.position, muzzleLocation.transform.rotation);
        //bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
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

    public void Dead()
    { 
    
    }



}
