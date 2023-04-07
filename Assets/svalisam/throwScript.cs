using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwScript : MonoBehaviour
{
    public GameObject die;
    public Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            throwDie();
        }
    }

    private void FixedUpdate()
    {
        Vector3 tmpPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos = new Vector3(tmpPos.x - transform.position.x, tmpPos.z - transform.position.y, tmpPos.z - transform.position.z);
            
    }

    public void throwDie()
    {

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if(groundPlane.Raycast(cameraRay,out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);
            GameObject newDie = Instantiate(die, transform.position, Quaternion.identity);
            Debug.Log(newDie);
        }

    }


}
