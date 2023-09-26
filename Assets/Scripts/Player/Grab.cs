using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour, IInteractable
{
    public Transform transportLocation; // Reference to the empty GameObject
    public float releaseDistance = 2.0f; // Distance at which the object is released
    private Quaternion defaultRotation; // Store the default rotation of the object
    private Rigidbody rb; // Reference to the Rigidbody of the object being grabbed
    private bool isGrabbed = false; // Flag to indicate if the object is being grabbed

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        defaultRotation = transform.rotation; // Store the default rotation
        rb.interpolation = RigidbodyInterpolation.Interpolate; // Set interpolation to improve smoothness
    }

    public void Interact()
    {
        if (!isGrabbed)
        {
            // Freeze the rotation when grabbed
            rb.freezeRotation = true;

            // Make the object follow the transport location
            isGrabbed = true;
        }
        else
        {
            ReleaseObject();
        }
    }

    private void FixedUpdate()
    {
        // If the object is grabbed, apply physics to it
        if (isGrabbed)
        {
            // Calculate the velocity required to reach the transport location
            Vector3 velocity = (transportLocation.position - transform.position) / Time.fixedDeltaTime;

            // Apply the calculated velocity to the Rigidbody
            rb.velocity = velocity;

            // Check the distance to the transportLocation
            float distanceToTransport = Vector3.Distance(transform.position, transportLocation.position);

            // If the distance exceeds the releaseDistance, release the object
            if (distanceToTransport > releaseDistance)
            {
                ReleaseObject();
            }
        }
    }

    private void ReleaseObject()
    {
        // Unfreeze the rotation when released
        rb.freezeRotation = false;

        // Release the object
        isGrabbed = false;

        // Clear the velocity to prevent further movement
        rb.velocity = Vector3.zero;
    }
}
