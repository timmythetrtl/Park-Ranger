using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class LogbookPanel : MonoBehaviour
{
    public Image creatureImage;
    public TextMeshProUGUI creatureNameText;
    public TextMeshProUGUI creatureDescriptionText;

    private PlayerInput playerInput;
    private float lastHorizontalInput = 0f; // Store the last horizontal input value

    public CreatureData creatureData; // Assigned in the Unity inspector
    private int currentCreatureIndex = 0;

    private void Start()
    {
        if (creatureData == null)
        {
            Debug.LogError("CreatureData not assigned to LogbookPanel.");
            return;
        }

        DisplayCreature(currentCreatureIndex);

        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        // Get the horizontal input value
        float horizontalInput = playerInput.actions["Movement"].ReadValue<Vector2>().x;

        // Check if the input value has changed
        if (horizontalInput != lastHorizontalInput)
        {
            if (horizontalInput > 0.5f)
            {
                NextCreature();
            }
            else if (horizontalInput < -0.5f)
            {
                PreviousCreature();
            }

            // Store the current input value
            lastHorizontalInput = horizontalInput;
        }
    }

    public void NextCreature()
    {
        currentCreatureIndex = (currentCreatureIndex + 1) % creatureData.creatures.Length;
        DisplayCreature(currentCreatureIndex);
    }

    public void PreviousCreature()
    {
        currentCreatureIndex = (currentCreatureIndex - 1 + creatureData.creatures.Length) % creatureData.creatures.Length;
        DisplayCreature(currentCreatureIndex);
    }

    public void DisplayCreature(int index)
    {
        CreatureInfo creatureInfo = creatureData.creatures[index];
        creatureImage.sprite = creatureInfo.creatureImage;
        creatureNameText.text = creatureInfo.creatureName;
        creatureDescriptionText.text = creatureInfo.description;
    }
}
