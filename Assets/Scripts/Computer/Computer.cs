using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    private bool isInteracting = false;

    private void Update()
    {
        if (isInteracting)
        {
            // Check if the player presses the "Escape" key
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StopInteracting();
            }
        }
    }

    public void Interact()
    {
        StartInteracting();
    }

    private void StartInteracting()
    {
        // Enable the cursor and stop time
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        isInteracting = true;
    }

    private void StopInteracting()
    {
        // Re-enable the cursor and resume time
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        isInteracting = false;
    }
}
