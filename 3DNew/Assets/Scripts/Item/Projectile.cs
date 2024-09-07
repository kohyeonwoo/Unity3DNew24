using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Weapon
{

    protected float speed = 70.0f;
    protected GameObject impactObject;

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(this.transform.forward * speed);

        Invoke("Dissapear", 4.0f);
    }

    protected void Dissapear()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaa");
            collision.gameObject.GetComponent<IDamageable>().Damage(10.0f);
            Dissapear();
        }
    }

}
