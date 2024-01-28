using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingItem : MonoBehaviour
{
    public AnimationCurve rotationCurve;

    public void OnInteract()
    {
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        float elapsedTime = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(0, 180, 0);
        float rotationTime = rotationCurve.keys[rotationCurve.length - 1].time;
        while (elapsedTime < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation,
                rotationCurve.Evaluate((elapsedTime / rotationTime)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}