using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PP.AI;

public class ReturnPoint : MonoBehaviour
{
    public bool seeking = false;

    public void SetSeek(bool b)
    {
        seeking = b;
    }
    private void OnTriggerStay(Collider other)
    {
        if(!seeking)
            return;
        if (other.transform.gameObject.tag == "Enemy")
        {
            other.transform.gameObject.GetComponentInChildren<Eyes>().OnEndpointCollision();
            Destroy(gameObject);
        }
    }
}
