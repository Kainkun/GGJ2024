using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchableItem : MonoBehaviour
{
    public float force = 100f;
    public float torque = 100f;

    public void Punch()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 f = Random.insideUnitSphere * force;
        f.y = Mathf.Abs(f.y);
        rb.AddForce(f, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * torque, ForceMode.Impulse);
    }
}