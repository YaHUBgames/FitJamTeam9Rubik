using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public IEnumerator shake(float duration, float strenght)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-0.2f, 0.2f) * strenght;
            float z = Random.Range(-0.2f, 0.2f) * strenght;

            transform.localPosition = new Vector3(x, originalPos.y, z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;

    }

}
