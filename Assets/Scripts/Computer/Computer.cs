using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    private bool isInteracting = false;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    private void Start()
    {
        // Get the MeshRenderer and MeshCollider components of the GameObject
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
    }

    public void Interact()
    {
        StartInteracting();
    }

    private void StartInteracting()
    {
        // Disable the mesh renderer and the mesh collider
        meshRenderer.enabled = false;
        meshCollider.enabled = false;

        // Enable the cursor and stop time
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        isInteracting = true;
    }

    public void StopInteracting()
    {
        // Enable the mesh renderer and the mesh collider
        meshRenderer.enabled = true;
        meshCollider.enabled = true;

        // Re-enable the cursor and resume time
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        isInteracting = false;
    }
}
