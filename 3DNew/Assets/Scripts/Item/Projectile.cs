using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Weapon
{

    protected float speed = 70.0f;
    protected GameObject impactObject;

    public override void InitSetting()
    {
        data.attackPoint = 10.0f;
    }

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
            collision.gameObject.GetComponent<IDamageable>().Damage(data.attackPoint);
            Dissapear();
        }
    }


}
