using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public GameObject target;

    public float speed;

    private Vector3 difValue;

    private void Start()
    {
        difValue = transform.position - target.transform.position;
        difValue = new Vector3(Mathf.Abs(difValue.x), Mathf.Abs(difValue.y), Mathf.Abs(difValue.z));
    }

    private void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, target.transform.position + difValue, speed);
    }

}
