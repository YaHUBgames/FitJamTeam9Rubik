using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieProjectil : MonoBehaviour
{
    Rigidbody rb;
    bool destroyed = false;
    public GameObject particle;
    public GameObject sonar;
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
            rb.angularVelocity=new Vector3(10,0,10);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!destroyed)
        {
            if(collision.transform.CompareTag("Enemy"))
                AudioManager.PlayStereoSound(ESound.DiceFleshImpact, transform.position);
            else
                AudioManager.PlayStereoSound(ESound.DiceWallImpact, transform.position);
            
            StartCoroutine(GameObject.Find("CameraHolder").GetComponent<ShakeCamera>().shake(0.4f, 1));
            Destroy(gameObject);
            Instantiate(particle, transform.position, Quaternion.identity);
            Instantiate(sonar, transform.position, Quaternion.identity);
            destroyed = true;
        }
    }

}
