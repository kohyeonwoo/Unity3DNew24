using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraBlade : Bullet
{
    private void Start()
    {
        attack = 10.0f;
        speed = 1.0f;    
    }

    private void OnEnable()
    {
        Invoke("Dissapear", 1.0f);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * 1.0f);     
    }

    private void Dissapear()
    {
         gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

            if(damageable != null)
            {
                this.gameObject.SetActive(false);
                damageable.Damage(attack);
                AudioManager.Instance.PlaySFX("HitSound1");
            }
        }
    }
}
