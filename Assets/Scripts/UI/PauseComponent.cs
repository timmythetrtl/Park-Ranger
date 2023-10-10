using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;  // Add this line to include the necessary namespace

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject playerObject; // Assign the Player GameObject containing FPSController and Interactor

    private FPSController fpsController;
    private Interactor interactor;
    private JournalManager journalManager; // Add reference to JournalManager

    private void Start()
    {
        if (playerObject != null)
        {
            fpsController = playerObject.GetComponent<FPSController>();
            interactor = playerObject.GetComponent<Interactor>();
        }
        else
        {
            Debug.LogError("Player GameObject not assigned to PauseMenu script!");
        }

        // Find and store the JournalManager script reference
        journalManager = FindObjectOfType<JournalManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !journalManager.IsAnyPanelActive) // Check if no panel is active
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        // Re-enable the "Player Input" component.
        PlayerInput playerInput = playerObject.GetComponent<PlayerInput>();
        if (playerInput != null) playerInput.enabled = true;

        // Lock the cursor again when resuming.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        // Disable the "Player Input" component.
        PlayerInput playerInput = playerObject.GetComponent<PlayerInput>();
        if (playerInput != null) playerInput.enabled = false;

        // Unlock the cursor when pausing.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    // Call this method when the options button is pressed.
    public void ShowOptionsMenu()
    {
        optionsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void Back()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
