using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public int score;
    public int chips=0;

    public bool key1 = false;
    public bool key2 = false;
    public bool key3 = false;

    public bool card1 = false;
    public bool card2 = false;
    public bool card3 = false;

    public InventoryScript inventory;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckKeys()
    {
        if (key1 || key3)
            inventory.ShowItem(0);
        else
            inventory.useKey(0);

        if (key2)
            inventory.ShowItem(1);
        else
            inventory.useKey(1);

        if (card1 || card3)
            inventory.ShowItem(2);
        else
            inventory.useKey(2);

        if (card2)
            inventory.ShowItem(3);
        else
            inventory.useKey(3);

    }

    public void addScore()
    {
        score++;
    }

    public void addChips(int value)
    {
        chips+=value;
    }


}
