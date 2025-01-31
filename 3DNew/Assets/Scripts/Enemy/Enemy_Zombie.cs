using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Zombie : MonoBehaviour
{

    public GameObject parent;
    public GameObject deadBody;
    public Rigidbody spine;

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

    public void ChangeRagdoll()
    {
        CopyCharacterTransformToRagdoll(this.transform, deadBody.transform);

        this.gameObject.SetActive(false);
        deadBody.SetActive(true);

        //spine.AddForce(new Vector3(0.0f, 0.0f, -300.0f), ForceMode.Impulse);
    }

    private void CopyCharacterTransformToRagdoll(Transform origin, Transform ragdoll)
    {
        for(int i =0; i < origin.childCount; i++)
        {
            if(origin.childCount != 0)
            {
                CopyCharacterTransformToRagdoll(origin.GetChild(i), ragdoll.GetChild(i));
            }

            ragdoll.GetChild(i).localPosition = origin.GetChild(i).localPosition;
            ragdoll.GetChild(i).localRotation = origin.GetChild(i).localRotation;

        }
    }

    public void Dead()
    {
        ChangeRagdoll();

        Invoke("Dissapear", 2.0f);
    }

    public void Dissapear()
    {
        deadBody.SetActive(false);
       // parent.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "AttackPart")
        {
            Dead();
            AudioManager.Instance.PlaySFX("HitSound1");
        }
    }

}
