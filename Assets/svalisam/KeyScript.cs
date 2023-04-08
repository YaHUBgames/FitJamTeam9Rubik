using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public int id;
    GameInfo info;
    bool done = false;
    public GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.Find("GameInfo").GetComponent<GameInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.GetComponentInChildren<throwScript>() && !done)
        {
            switch (id)
            {
                case 1: info.key1 = true;
                    break;

                case 2: info.key2 = true;
                    break;

                case 3: info.key3 = true;
                    break;

                case 4: info.card1 = true;
                    break;

                case 5: info.card2 = true;
                    break;

                case 6: info.card2 = true;
                    break;
            }
            done = true;
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
}
