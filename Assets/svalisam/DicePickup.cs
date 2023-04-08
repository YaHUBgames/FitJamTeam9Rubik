using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePickup : MonoBehaviour
{
    // Start is called before the first frame update
    throwScript player;
    public int dice = 20;
    void Start()
    {
        player = GameObject.Find("Player").GetComponentInChildren<throwScript>();  
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.parent.gameObject.GetComponentInChildren<throwScript>()!= null)
        {
            player.AddDice(20);
            AudioManager.PlayStereoSound(ESound.DicePickUp, transform.position);
            Destroy(gameObject);
        }
    }
}
