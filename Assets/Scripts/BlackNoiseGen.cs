using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackNoiseGen : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private void Start() {
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        yield return new WaitForEndOfFrame();
        while(true)
        {
            yield return new WaitForSeconds(speed);
            
            AudioManager.PlayMonoSound(ESound.BackgroundBass);
        }
    }
}
