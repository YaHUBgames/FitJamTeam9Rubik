using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieProjectil : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody>();
        throwDie();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void throwDie()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(pointToLook);
            rb.AddForce(transform.forward * 1000);
        }
    }

}
