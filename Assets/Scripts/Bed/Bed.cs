using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    private int SleepTime = 8;
    private bool isLoopActive = false; // Flag to track if the loop is active

    public void Interact()
    {
        if (!isLoopActive) // Only start the loop if it's not already active
        {
            LightingManager lightingManager = FindObjectOfType<LightingManager>();
            if (lightingManager != null)
            {
                isLoopActive = true; // Set the flag to indicate the loop is active

                StartCoroutine(AdvanceTimeWithDelay(lightingManager));
            }
        }
    }

    private IEnumerator AdvanceTimeWithDelay(LightingManager lightingManager)
    {
        for (int i = 0; i < SleepTime; i++)
        {
            lightingManager.IncrementHours(1);
            yield return new WaitForSeconds(1); // Wait for one second
        }

        isLoopActive = false; // Reset the loop flag
    }
}
