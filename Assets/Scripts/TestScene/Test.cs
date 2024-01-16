using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Test : MonoBehaviour, IInteractable
{
    public GameObject objectToActivate;
    public PlayableDirector timeline;
    public GameObject playerObject; // Reference to the player object you want to disable

    private void Start()
    {
        // Subscribe to the timeline events
        if (timeline != null)
        {
            timeline.played += OnTimelinePlayed;
            timeline.stopped += OnTimelineStopped;
        }
    }

    public void Interact()
    {
        // Check if the object to activate is not null
        if (objectToActivate != null)
        {
            // Activate the specified object
            objectToActivate.SetActive(true);
        }

        // Disable the player object if it's not null
        if (playerObject != null)
        {
            playerObject.SetActive(false);
        }

        // Play the timeline
        if (timeline != null)
        {
            timeline.Play();
        }
    }

    private void OnTimelinePlayed(PlayableDirector director)
    {
        // Debug.Log("Timeline started playing");
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        // Reactivate the player object when the timeline is done playing
        if (playerObject != null)
        {
            playerObject.SetActive(true);
        }

        // Debug.Log("Timeline stopped playing");
    }
}
