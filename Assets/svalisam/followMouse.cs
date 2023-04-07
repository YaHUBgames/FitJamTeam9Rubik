using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMouse : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public float speed;
    public Transform target;
    public Vector3 pushForce;
    void Start()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        pushForce = transform.position - mousePos;
        rb.AddForce(new Vector3(pushForce.x,0,pushForce.z)*speed);

    }
}
