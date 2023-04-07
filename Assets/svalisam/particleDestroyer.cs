using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleDestroyer : MonoBehaviour
{
    float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        destroyTime = Time.time + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
