using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // Make sure to include the Input System namespace

interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;

    public Image crosshairImage; // Reference to the crosshair UI Image

    private PlayerInput playerInput; // Reference to the Player Input component

    private int raycastLayerMask; // Layer mask for raycasting

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial transparency of the crosshair to 0
        crosshairImage.color = new Color(1f, 1f, 1f, 0f);
        playerInput = GetComponent<PlayerInput>();
        // Create a layer mask that includes all layers except "Player"
        raycastLayerMask = ~LayerMask.GetMask("Player");
    }

    void Update()
    {
        // Cast a ray from the InteractorSource using the layer mask
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, raycastLayerMask))
        {
            // Check if the object hit is interactable
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                // Show the crosshair at full opacity (alpha = 1)
                crosshairImage.color = new Color(1f, 1f, 1f, 1f);

                // Check if the player pressed the interact key (E)
                if (playerInput.actions["Interact"].triggered)
                {
                    // Interact with the object
                    interactObj.Interact();
                }
            }
            else
            {
                // The object is not interactable, so keep the crosshair transparent
                crosshairImage.color = new Color(1f, 1f, 1f, 0f);
            }
        }
        else
        {
            // The ray didn't hit anything, keep the crosshair transparent
            crosshairImage.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
