using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Computer : MonoBehaviour, IInteractable
{
    private bool isInteracting = false;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    public Transform player; // Reference to the player object.
    public Transform targetEmpty; // Reference to the empty GameObject for the target position.
    public Vector3 customRotation = new Vector3(0, 0, 0); // Set your specific rotation here.

    public GameObject playerObject; // Assign the Player GameObject containing FPSController and Interactor

    private FPSController fpsController; // Reference to the FPSController script.


    private Camera mainCamera;
    private Transform originalParent;
    private bool snapping = false;
    private Vector3 originalCameraPosition; // Store the original camera position.
    private Vector3 originalCameraRotation; // Store the original camera rotation.

    private void Start()
    {
        // Get the MeshRenderer and MeshCollider components of the GameObject
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        mainCamera = Camera.main;

        // Find the Player GameObject with FPSController and Interactor components
        GameObject foundPlayerObject = GameObject.FindWithTag("Player");

        if (foundPlayerObject != null)
        {
            playerObject = foundPlayerObject;
        }

        fpsController = playerObject.GetComponent<FPSController>();
    }

    public void Interact()
    {
        originalCameraPosition = mainCamera.transform.position; // Store the original camera position.
        originalCameraRotation = mainCamera.transform.eulerAngles; // Store the original camera rotation.
        StartInteracting();
        
    }

    void Update(){
        if(isInteracting == true){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void StartInteracting()
    {
        // Disable the mesh renderer and the mesh collider
        meshRenderer.enabled = false;
        meshCollider.enabled = false;

        originalParent = mainCamera.transform.parent;
        mainCamera.transform.parent = player; // Parent the camera to the player temporarily.
        mainCamera.transform.position = targetEmpty.position; // Set the camera's position to the empty GameObject's position.
        mainCamera.transform.rotation = Quaternion.Euler(customRotation); // Set the camera's rotation to the customRotation value.
        snapping = true;

        // Enable the cursor and stop time
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (fpsController != null)
        {
            fpsController.enabled = false; // Disable the FPSController script.
        }
        isInteracting = true;
    }

    public void StopInteracting()
    {
        // Enable the mesh renderer and the mesh collider
        meshRenderer.enabled = true;
        meshCollider.enabled = true;

        mainCamera.transform.parent = originalParent;
        mainCamera.transform.position = originalCameraPosition; // Return to the original camera position.
        mainCamera.transform.rotation = Quaternion.Euler(originalCameraRotation); // Return to the original camera rotation.
        snapping = false;

        // Re-enable the cursor and resume time
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Re-enable the "Player Input" component.
        if (fpsController != null)
        {
            fpsController.enabled = true; // Enable the FPSController script.
        }
        isInteracting = false;
    }
}
