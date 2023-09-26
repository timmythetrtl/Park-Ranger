using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskDisplay : MonoBehaviour
{
    public TextMeshProUGUI taskListText;

    private Quest currentQuest; // Store the current quest

    // Call this function to trigger DisplayTasks with the stored quest
    public void RefreshDisplay()
    {
        if (currentQuest != null)
        {
            DisplayTasks(currentQuest);
        }
    }

    public void DisplayTasks(Quest quest)
    {
        currentQuest = quest; // Store the current quest

        taskListText.text = ""; // Clear the text initially

        if (currentQuest != null)
        {
            for (int i = 0; i < currentQuest.steps && i < currentQuest.Tasks.Length; i++)
            {
                string taskText = "- " + currentQuest.Tasks[i] + "\n";
                if (i != currentQuest.steps - 1) // Apply gray color to all except last task
                {
                    taskText = "<color=#808080>" + taskText + "</color>";
                }
                taskListText.text += taskText;
            }
        }
    }

    private void OnEnable()
    {
        RefreshDisplay();
    }
}
