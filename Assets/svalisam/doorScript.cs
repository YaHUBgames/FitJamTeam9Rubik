using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{

    public bool key1;
    public bool key2;
    public bool key3;
    public bool key4;
    public bool key5;
    public bool key6;

    Animator anim;
    public BoxCollider doorCollider;
    GameInfo info;

    bool hasOpend = false;

    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.Find("GameInfo").GetComponent<GameInfo>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player" && !hasOpend)
        {
            if (key1  && !info.key1)
                return;

            if (key2  && !info.key2)
                return;

            if (key3  && !info.key3)
                return;

            if (key4  && !info.card1)
                return;

            if (key5  && !info.card2)
                return;

            if (key6  && !info.card3)
                return;

            anim.SetBool("open",true);
            doorCollider.enabled = false;
            hasOpend = true;
        }
    }

    public void closeDoor()
    {
        doorCollider.enabled = true;
        anim.SetBool("open", false);
    }

}
