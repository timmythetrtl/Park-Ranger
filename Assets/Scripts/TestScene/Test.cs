using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Test : MonoBehaviour
{
    public PlayableDirector timeline;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the player
        {
            // Play the timeline
            if (timeline != null)
            {
                timeline.Play();
                // Destroy the object after the timeline is played
                Destroy(gameObject);
            }
        }
    }
}
