using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueHandler: MonoBehaviour
{
    public DialogueData[] dialogueData; // Reference to the DialogueData scriptable object
    public int state = 0;

    public void handleDialogue()
    {

        // Find the GameObject with the "DialogueBox" tag (you can adjust this as needed)
        GameObject dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");

        if (dialogueData[state] != null)
        {
            Dialogue dialogue = dialogueBox.GetComponent<Dialogue>(); // Assuming the Dialogue component is on the same GameObject as QuestGiver.

            if (dialogue != null)
            {
                dialogue.StartDialogue(dialogueData[state].lines);
                if (state == 0)
                    state = 1;
            }
            else
            {
                Debug.LogError("No Dialogue component found on the GameObject.");
            }
        }
        else
        {
            Debug.LogError("No DialogueData scriptable object assigned to the QuestGiver.");
        }
    }
}
