using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    int maxHealth;
    public int health;
    throwScript player;

    public Slider slider;
    void Start()
    {
        player = GameObject.Find("Player").GetComponentInChildren<throwScript>();
        maxHealth = player.dice;
        health = maxHealth;
        slider.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = player.dice;
        if(health > maxHealth)
        {
            slider.maxValue = health;
        }
        slider.value = health;
    }

    public void takeDamage(int damage)
    {
        Debug.Log("TAKE GAMAGE");
        AudioManager.PlayStereoSound(ESound.DiceLost, transform.position);
        health -= damage;
    }
}
