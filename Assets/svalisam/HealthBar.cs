using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    float maxHealth=100;
    public float health;
    throwScript player;
    public ShakeCamera cameraShake;
    public RectTransform bar;

    void Start()
    {
        player = GameObject.Find("Player").GetComponentInChildren<throwScript>();
        maxHealth = player.dice;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = player.dice;
        float newScale = health / maxHealth;
        bar.localScale= new Vector3(newScale,1,1);
        
    }

    public void takeDamage(int damage)
    {
        player.LoseDice(damage);
        cameraShake.shake(0.5f, 2);
        AudioManager.PlayStereoSound(ESound.DiceLost, transform.position);
    }
}
