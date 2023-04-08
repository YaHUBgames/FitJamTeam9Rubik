using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpScript : MonoBehaviour
{

    public int chipValue;
    GameInfo info;
    public GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.Find("GameInfo").GetComponent<GameInfo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.PlayStereoSound(ESound.PickupChip, transform.position);
            Instantiate(effect, transform.position, Quaternion.identity);
            info.addChips(chipValue);
            Destroy(gameObject);
        }
      
    }
}
