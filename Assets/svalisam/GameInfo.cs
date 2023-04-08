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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
