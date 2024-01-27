using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSystem : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Teleport();
        }
    }

    public void Teleport()
    {
        CharacterController cc = FindObjectOfType<CharacterController>();
        cc.enabled = false;
        cc.transform.position += new Vector3(0, 0, 30);
        cc.enabled = true;
    }
}