using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winTrigger : MonoBehaviour
{
    pauseMenuScript menu;
    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("PauseMenu").GetComponent<pauseMenuScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            menu.win();
    }
}
