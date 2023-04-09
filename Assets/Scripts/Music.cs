using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music _instance;

    public AudioSource source;
    public float turnOnRate = 0.001f;
    public float turnOffRate = 0.001f;

    public float maxVolume = 0.1f;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public float hourglass = 0;

    public void Update() {
        if(hourglass > 0)
            hourglass -= Time.deltaTime;
        else if(hourglass > -1){

            StopAllCoroutines();
            on = false;
            StartCoroutine(TurnOff());
            hourglass = -10;
        }
    }
 public bool on = false;
    public void TriggerMusic(float time)
    {   
        hourglass = time;
        if(on)
            return;
        StopAllCoroutines();
        on = true;
        StartCoroutine(TurnOn());
    }

    IEnumerator TurnOn()
    {
        while(source.volume < maxVolume)
        {
            source.volume += turnOnRate;
            yield return new WaitForSeconds(0.001f);
        }
        source.volume = maxVolume;
    }

    IEnumerator TurnOff()
    {
        while(source.volume > 0)
        {
            source.volume -= turnOffRate;
            yield return new WaitForSeconds(0.001f);
        }
        source.volume = 0;
    }
}
