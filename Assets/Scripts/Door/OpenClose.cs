using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour, IInteractable
{
    public bool doorOpen = false;
    public float rotationSpeed = 90f; // Adjust the speed of rotation here
    public int step;

    private bool isRotating = false;
    private Quaternion targetRotation;


    public string questToTrigger; // Public variable to specify the quest to trigger this door

    private void OnEnable()
    {
        // Subscribe to the quest completion event when the script is enabled.
        QuestManager.OnQuestCompleted += HandleQuestCompleted;
    }

    private void OnDisable()
    {
        // Unsubscribe from the quest completion event when the script is disabled or destroyed.
        QuestManager.OnQuestCompleted -= HandleQuestCompleted;
    }

    public void Interact()
    {
        if (!isRotating)
        {
            if (doorOpen)
            {
                // Close the door
                targetRotation = Quaternion.Euler(0, 0, 0); // Target rotation (closed position)
            }
            else
            {
                // Open the door
                targetRotation = Quaternion.Euler(0, 90, 0); // Target rotation (open position)
            }

            StartCoroutine(RotateObject());
        }
    }

    private void HandleQuestCompleted(string questName, int questStep)
    {
        // Check if the completed quest matches the quest to trigger this door.
        if (questName == questToTrigger && questStep == step)
        {
            if (!isRotating)
            {
                if (doorOpen)
                {
                    // Close the door
                    targetRotation = Quaternion.Euler(0, 0, 0); // Target rotation (closed position)
                }
                else
                {
                    // Open the door
                    targetRotation = Quaternion.Euler(0, 90, 0); // Target rotation (open position)
                }

                StartCoroutine(RotateObject());
            }
        }
    }

    

    private IEnumerator RotateObject()
    {
        isRotating = true;

        Quaternion startRotation = transform.rotation;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        isRotating = false;
        doorOpen = !doorOpen;
    }
}
