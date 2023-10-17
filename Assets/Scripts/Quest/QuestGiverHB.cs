using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiverHB : MonoBehaviour
{
    public string QuestName = string.Empty;
    public TextMeshProUGUI Captions = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the player
        {
            // Call handleDialogue from the DialogueHandler script
            DialogueHandler dialogueHandler = GetComponent<DialogueHandler>();
            if (dialogueHandler != null)
            {
                dialogueHandler.handleDialogue();
            }

            Quest.QUESTSTATUS Status = QuestManager.GetQuestStatus(QuestName);
            if (Status == Quest.QUESTSTATUS.UNASSIGNED)
            {
                QuestManager.SetQuestStatus(QuestName, Quest.QUESTSTATUS.ASSIGNED);
                QuestManager.IncrementQuestStep(QuestName);
                // Add the quest to the UI panel
                QuestPanel questPanel = FindObjectOfType<QuestPanel>();
                if (questPanel != null)
                {
                    questPanel.AddQuest(QuestManager.GetQuestByName(QuestName));
                }
            }
        }
    }

}
