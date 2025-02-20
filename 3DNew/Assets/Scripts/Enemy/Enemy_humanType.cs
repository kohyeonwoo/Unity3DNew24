using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_humanType : EnemyBase, IDamageable
{

    public GameObject characterObject;
    public GameObject ragdollObject;

    public Rigidbody spine;

    public bool bDead;

    private void Start()
    {
        maxHealth = 10.0f;
        currentHealth = maxHealth;
        bDead = false;
    }


    private void Update()
    {

        ragdollObject.transform.position = characterObject.transform.position;

       if(bDead == true)
       {
            ChangeRagdoll();
       }

    }

    public void ChangeRagdoll()
    {
        CopyCharacterTransformToRagdoll(characterObject.transform, ragdollObject.transform);

        characterObject.SetActive(false);
        ragdollObject.SetActive(true);

       // spine.AddForce(new Vector3(0, 0, 50.0f), ForceMode.Impulse);
        //spine.AddForce(Vector3.back * 15, ForceMode.Impulse);
    }

    private void CopyCharacterTransformToRagdoll(Transform origin, Transform ragdoll)
    {
        for (int i = 0; i < origin.childCount; i++)
        {
            if (origin.childCount != 0)
            {
                CopyCharacterTransformToRagdoll(origin.GetChild(i), ragdoll.GetChild(i));
            }

            ragdoll.GetChild(i).localPosition = origin.GetChild(i).localPosition;
            ragdoll.GetChild(i).localRotation = origin.GetChild(i).localRotation;
        }
    }

    public void Damage(float Damage)
    {
        currentHealth -= Damage;

        if(currentHealth <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        bDead = true;
    }
}
