using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public AnimationCurve curve;

    public IEnumerator Shake(float shakeStrength, float duration = 1)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / (duration / 2));
            transform.position = startPosition + Random.insideUnitSphere * shakeStrength;
            yield return null;
        }

        transform.position = startPosition;
    }
}