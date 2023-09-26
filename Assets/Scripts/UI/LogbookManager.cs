using UnityEngine;
using UnityEngine.InputSystem;

public class PanelManager : MonoBehaviour
{
    public JournalManager journalManager; // Reference to the "JournalManager" script

    private PlayerInput playerInput;
    private FPSController fpsController;
    private Interactor interactor;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        fpsController = GetComponent<FPSController>();
        interactor = GetComponent<Interactor>();

        EnablePlayerComponents();

        // Subscribe to the event using the EventManager
        EventManager.OnPanelStateChanged += HandlePanelStateChanged;
    }

    private void HandlePanelStateChanged(bool panelOpen)
    {
        if (panelOpen)
        {
            DisablePlayerComponents();
        }
        else
        {
            EnablePlayerComponents();
        }
    }

    private void Update()
    {
        if (playerInput.actions["Book"].triggered)
        {
            journalManager.TogglePanel("logbookPanel");
        }
        else if (playerInput.actions["Quest"].triggered)
        {
            journalManager.TogglePanel("questPanel");
        }
        else if (playerInput.actions["Map"].triggered)
        {
            journalManager.TogglePanel("mapPanel");
        }
    }

    private void EnablePlayerComponents()
    {
        fpsController.enabled = true;
        interactor.enabled = true;
    }

    private void DisablePlayerComponents()
    {
        fpsController.enabled = false;
        interactor.enabled = false;
    }
}
