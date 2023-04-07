using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CamMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody RB;
    [SerializeField] private Transform camRoot;
    private Vector3 offset;

    [SerializeField] private float speed = 1f;

    private void Start() 
    {
        offset = transform.position - camRoot.position; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, camRoot.position + offset, Time.deltaTime *speed );
    }
}
