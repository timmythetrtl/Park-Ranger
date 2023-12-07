using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, IInteractable
{

    public GameObject objectToActivate;

    public void Interact()
    {
        // Check if the object to activate is not null
            if (objectToActivate != null)
            {
                // Activate the specified object
                objectToActivate.SetActive(true);
            }
    }
}
