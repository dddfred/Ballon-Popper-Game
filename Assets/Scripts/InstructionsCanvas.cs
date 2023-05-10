using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InstructionsCanvas : MonoBehaviour
{
    public Canvas instructionsCanvas;
    public float displayTime = 6f;

    private bool instructionsDisplayed = false;

    void Start()
    {
        // Check if instructions were already displayed
        if (!instructionsDisplayed)
        {
            // Display instructions canvas
            DisplayInstructions();

            // Set instructionsDisplayed flag to true
            instructionsDisplayed = true;
        }
    }

    public void DisplayInstructions()
    {
        // Display instructions canvas
        instructionsCanvas.gameObject.SetActive(true);

        // Wait for displayTime seconds
        StartCoroutine(WaitAndHideCanvas());
    }

    IEnumerator WaitAndHideCanvas()
    {
        yield return new WaitForSeconds(displayTime);

        // Hide instructions canvas
        instructionsCanvas.gameObject.SetActive(false);
    }
}