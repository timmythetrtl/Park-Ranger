using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed;

    private int index;
    private string[] lines;
    private bool[] choices;

    public Vector3 onScreenPosition;
    public Vector3 offScreenPosition;

    public GameObject playerObject; // Assign the Player GameObject containing FPSController and Interactor

    private FPSController fpsController;
    private Interactor interactor;

    void Start()
    {
        // Initialize on and off screen positions
        offScreenPosition = transform.position; // Assuming it starts on screen
        onScreenPosition = new Vector3(transform.position.x, +10f, transform.position.z); // Adjust -10f to your off-screen Y position

        // Find the Player GameObject with FPSController and Interactor components
        GameObject foundPlayerObject = GameObject.FindWithTag("Player");

        if (foundPlayerObject != null)
        {
            playerObject = foundPlayerObject;
            fpsController = playerObject.GetComponent<FPSController>();
            interactor = playerObject.GetComponent<Interactor>();
        }
        else
        {
            Debug.LogError("Player GameObject with FPSController and Interactor components not found!");
        }
    }



    public void StartDialogue(string[] dialogueLines, bool[] dialogueChoices)
    {
        lines = dialogueLines;
        choices = dialogueChoices;
        index = 0;
        textComponent.text = string.Empty;
        gameObject.SetActive(true);

        // Move the Dialogue object on screen
        transform.position = onScreenPosition;

        // Disable the "Player Input" component.
        PlayerInput playerInput = playerObject.GetComponent<PlayerInput>();
        if (playerInput != null) playerInput.enabled = false;

        StartCoroutine(TypeLine());
    }

    void EndDialogue()
    {
        // Move the Dialogue object off screen
        transform.position = offScreenPosition;

        // Re-enable the "Player Input" component.
        PlayerInput playerInput = playerObject.GetComponent<PlayerInput>();
        if (playerInput != null) playerInput.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent != null && lines != null && index < lines.Length)
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
            else
            {
                // Handle the case when textComponent or lines are null or index is out of bounds
                EndDialogue();
            }
        }
    }

    IEnumerator TypeLine()
    {
        if (lines != null && index < lines.Length && textComponent != null)
        {
            foreach (char c in lines[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }

    void NextLine()
    {
        if (lines != null && index < lines.Length && textComponent != null)
        {
            if (index < lines.Length - 1)
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                EndDialogue();
            }
        }
        else
        {
            // Handle the case when textComponent or lines are null or index is out of bounds
            EndDialogue();
        }
    }
}
