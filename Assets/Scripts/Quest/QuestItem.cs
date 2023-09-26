using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour, IInteractable
{

    public string QuestName = string.Empty;
    public int questStep;

    public void Interact()
    {
        
        if (QuestManager.GetQuestStep(QuestName) == questStep)
        {
            QuestManager.IncrementQuestStep(QuestName);
        }

    }
}
