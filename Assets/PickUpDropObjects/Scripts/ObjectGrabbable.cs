using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour, IInteractable
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    private bool isGrabbed = false;

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
    }

    public void Interact()
    {
        if (!isGrabbed)
        {
            // Assuming the object with this script is the grab point (e.g., an empty GameObject on the player)
            objectGrabPointTransform = transform;
            isGrabbed = true;
            objectRigidbody.useGravity = false;
        }
        else
        {
            objectGrabPointTransform = null;
            isGrabbed = false;
            objectRigidbody.useGravity = true;
        }
    }

    private void FixedUpdate()
    {
        if (isGrabbed && objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
        }
    }
}
