using System;
using UnityEngine;

public class CursorFix : MonoBehaviour
{
    // idk why this script dissapeared - im guessing it looked like this [daniel]
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
