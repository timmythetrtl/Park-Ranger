using UnityEngine;

public class JournalManager : MonoBehaviour
{
    public GameObject logbookPanel;
    public GameObject questPanel;
    public GameObject mapPanel;

    private GameObject activePanel = null;

    private void Start()
    {
        // You might perform any initialization here if needed
    }

    private void Update()
    {
        // Check for the "Escape" key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If an active panel exists, close it
            if (activePanel != null)
            {
                ClosePanel(activePanel);
            }
        }
    }
    public bool IsAnyPanelActive
    {
        get { return activePanel != null && activePanel.activeSelf; }
    }


    public void TogglePanel(string panelName)
    {
        GameObject panel = GetPanel(panelName);

        if (panel != null)
        {
            if (activePanel == panel)
            {
                ClosePanel(panel);
            }
            else
            {
                if (activePanel != null)
                {
                    ClosePanel(activePanel);
                }

                OpenPanel(panel);
            }
        }
    }

    private GameObject GetPanel(string panelName)
    {
        if (panelName == "logbookPanel")
        {
            return logbookPanel;
        }
        else if (panelName == "questPanel")
        {
            return questPanel;
        }
        else if (panelName == "mapPanel")
        {
            return mapPanel;
        }

        return null;
    }

    private void OpenPanel(GameObject panel)
    {
        Time.timeScale = 0f;
        activePanel = panel;
        panel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        EventManager.InvokePanelStateChanged(true);
    }

    private void ClosePanel(GameObject panel)
    {
        Time.timeScale = 1f;
        activePanel = null;
        panel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        EventManager.InvokePanelStateChanged(false);
    }
}
