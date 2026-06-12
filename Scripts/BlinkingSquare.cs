using UnityEngine;
using UnityEngine.UI;

public class BlinkingSquare : MonoBehaviour
{
    public Image squareImage; // Reference to the Image component
    public Color triggerColor = Color.red; // The color to show when triggered
    private Color originalColor; // To store the original color of the square

    void Start()
    {
        // Get the Image component and store its original color
        if (squareImage == null)
        {
            squareImage = GetComponent<Image>();
        }
        originalColor = squareImage.color; // Save the original color
    }

    // Method to change the color (called when the image pops up)
    public void ShowTriggerColor()
    {
        squareImage.color = triggerColor; // Change to the trigger color
    }

    // Method to revert to the original color (called when the image disappears)
    public void ResetColor()
    {
        squareImage.color = originalColor; // Revert to the original color
    }
}
