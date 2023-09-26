using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public TextMeshProUGUI questNameText;

    private Quest quest;
    private TaskDisplay taskDisplay;

    public void Initialize(Quest newQuest)
    {
        quest = newQuest;
        questNameText.text = quest.QuestName;
    }

    public void DisplayTasks()
    {
        taskDisplay = FindObjectOfType<TaskDisplay>(); // Find the TaskDisplay object in the scene
        if (taskDisplay != null)
        {

            taskDisplay.DisplayTasks(quest);
        }
    }
}
