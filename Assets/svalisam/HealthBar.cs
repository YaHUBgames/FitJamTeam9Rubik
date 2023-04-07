using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    int maxHealth = 50;
    public int health;

    public Slider slider;
    void Start()
    {
        health = maxHealth;
        slider.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health;
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }
}
