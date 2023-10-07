using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed;

    private int index;
    private string[] lines;
    private bool[] choices;

    public Vector3 onScreenPosition;
    public Vector3 offScreenPosition;



    void Start()
    {
        // Initialize on and off screen positions
        offScreenPosition = transform.position; // Assuming it starts on screen
        onScreenPosition = new Vector3(transform.position.x, +10f, transform.position.z); // Adjust -10f to your off-screen Y position
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

        StartCoroutine(TypeLine());
    }

    void EndDialogue()
    {
        // Move the Dialogue object off screen
        transform.position = offScreenPosition;

        // Deactivate the GameObject
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
