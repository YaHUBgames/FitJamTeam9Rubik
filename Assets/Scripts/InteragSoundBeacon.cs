using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteragSoundBeacon : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private void Start() {
        StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        yield return new WaitForEndOfFrame();
        while(true)
        {
            yield return new WaitForSeconds(speed);
            
            AudioManager.PlayStereoSound(ESound.InteractiblePing, transform.position);
        }
    }
}
