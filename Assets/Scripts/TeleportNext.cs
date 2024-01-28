using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportNext : MonoBehaviour
{
    public Transform teleportTarget;
    public UnityEvent onTeleport;
    public Transform[] teleportWith;

    public void Teleport()
    {
        print("Teleport to " + teleportTarget.name);
        CharacterController cc = FindObjectOfType<CharacterController>();
        cc.enabled = false;
        cc.transform.position += teleportTarget.position - transform.position;
        cc.enabled = true;
        foreach (Transform t in teleportWith)
        {
            t.position += teleportTarget.position - transform.position;
        }
        onTeleport?.Invoke();
    }
}