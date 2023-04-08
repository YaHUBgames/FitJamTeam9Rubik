using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{

    public List<GameObject> keys;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowItem(int id)
    {
        keys[id].SetActive(true);
    }

    public void useKey(int id)
    {
        keys[id].SetActive(false);
    }
}
