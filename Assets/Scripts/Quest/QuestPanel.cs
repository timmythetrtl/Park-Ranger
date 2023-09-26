using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    public GameObject questUIPrefab;
    public GameObject panelObject; // Reference to the panel object where the prefab will be added

    private Vector2 nextQuestPosition = Vector2.zero;

    public void AddQuest(Quest quest)
    {
        GameObject questUI = Instantiate(questUIPrefab, panelObject.transform);
        RectTransform questUIRectTransform = questUI.GetComponent<RectTransform>();
        if (questUIRectTransform != null)
        {
            questUIRectTransform.anchoredPosition = nextQuestPosition;
            nextQuestPosition -= new Vector2(0, questUIRectTransform.sizeDelta.y-40); // Adjust as needed
        }

        QuestUI questUIScript = questUI.GetComponent<QuestUI>();
        if (questUIScript != null)
        {
            questUIScript.Initialize(quest);
        }
    }
}