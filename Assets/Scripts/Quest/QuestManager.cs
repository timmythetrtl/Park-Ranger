using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Define a delegate type for quest completion events.
    public delegate void QuestCompletedEventHandler(string questName, int questStep);

    // Define the event using the delegate type.
    public static event QuestCompletedEventHandler OnQuestCompleted;

    private static QuestManager ThisInstance = null;
    public Quest[] Quests;

    void Awake()
    {
        if (ThisInstance == null)
        {
            ThisInstance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }


    public static Quest GetQuestByName(string QuestName)
    {
        foreach (Quest Q in ThisInstance.Quests)
        {
            if (Q.QuestName.Equals(QuestName))
            {
                return Q;
            }
        }
        return null;
    }


    public static Quest.QUESTSTATUS GetQuestStatus(string QuestName)
    {
        foreach (Quest Q in ThisInstance.Quests)
        {
            if (Q.QuestName.Equals(QuestName))
            {
                return Q.Status;
            }
        }
        return Quest.QUESTSTATUS.UNASSIGNED;
    }

    public static void SetQuestStatus(string QuestName, Quest.QUESTSTATUS NewStatus)
    {
        foreach (Quest Q in ThisInstance.Quests)
        {
            if (Q.QuestName.Equals(QuestName))
            {
                Q.Status = NewStatus; return;
            }
        }
    }

    public static void IncrementQuestStep(string QuestName)
    {
        foreach (Quest Q in ThisInstance.Quests)
        {
            if (Q.QuestName.Equals(QuestName))
            {
                Q.IncrementStep();
                OnQuestCompleted?.Invoke(QuestName, Q.steps);

                if (Q.steps == Q.compStep)
                {
                    Q.Status = Quest.QUESTSTATUS.COMPLETE;
                    // Invoke the quest completed event with the quest name.
                    
                }

                return;
            }
        }
    }
    public static int GetQuestStep(string QuestName)
    {
        foreach (Quest Q in ThisInstance.Quests)
        {
            if (Q.QuestName.Equals(QuestName))
            {
                return Q.steps;
            }
        }
        return -1; // Return -1 if the quest is not found
    }


    public static void Reset()
    {
        foreach (Quest Q in ThisInstance.Quests)
        {
            Q.Status = Quest.QUESTSTATUS.UNASSIGNED;
        }
    }
}