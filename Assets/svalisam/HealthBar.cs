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
    public ShakeCamera cameraShake;

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
        player.LoseDice(5);
        cameraShake.shake(0.5f, 2);
        AudioManager.PlayStereoSound(ESound.DiceLost, transform.position);
    }
}
