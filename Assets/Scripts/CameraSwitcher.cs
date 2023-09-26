using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour, IInteractable
{
    public Camera[] cameras;
    private int currentCameraIndex = 0;
    public int direction = 1;

    public void Interact()
    {
        SwitchCamera(direction); // Switch to the next camera
    }

    private void SwitchCamera(int direction)
    {
        cameras[currentCameraIndex].gameObject.SetActive(false); // Disable the current camera

        currentCameraIndex += direction;
        if (currentCameraIndex < 0)
        {
            currentCameraIndex = cameras.Length - 1;
        }
        else if (currentCameraIndex >= cameras.Length)
        {
            currentCameraIndex = 0;
        }

        cameras[currentCameraIndex].gameObject.SetActive(true); // Enable the new camera
    }
}




