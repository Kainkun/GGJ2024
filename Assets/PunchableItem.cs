using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchableItem : MonoBehaviour
{
    public void Punch()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(Random.Range(-3f, 3f), Random.Range(2, 4f), Random.Range(-3f, 3f)), ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 1000f, ForceMode.Impulse);
    }
}