using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Test : MonoBehaviour, IInteractable
{
    public GameObject objectToActivate;
    public PlayableDirector timeline;

    public void Interact()
    {
        // Check if the object to activate is not null
        if (objectToActivate != null)
        {
            // Activate the specified object
            objectToActivate.SetActive(true);
        }

        // Play the timeline
        if (timeline != null)
        {
            timeline.Play();
        }
    }
}
