using UnityEngine;

public class FolderSelection : MonoBehaviour
{
    public ImageManager imageManager;       // Reference to the ImageManager script
    public GameObject setSelectionCanvas;  // Reference to the Set Selection Canvas
    public GameObject gameCanvas;          // Reference to the Game Canvas
    public GameObject gameObjects;         // Parent object for gameplay-related objects
    public GameObject pauseManager;
    public GameObject afterRunCanvas;
    public Camera uiCamera;  // Reference to the UICamera
    public Camera playerCamera;  // Reference to the Player Capsule Camera
    public GameObject keyLogger;

    public void SelectSet(string folderName)
    {
        // Load the images from the selected set into the ImageManager
        imageManager.SetImagesFromFolder(folderName);

        // Show the game UI and start the game
        setSelectionCanvas.SetActive(false); // Hide Set Selection Menu
        //logSelectionCanvas.SetActive(false);
        gameCanvas.SetActive(true);         // Show Game UI
        gameObjects.SetActive(true);        // Enable gameplay objects
        pauseManager.SetActive(true);
        uiCamera.enabled = false;  // Disable the UICamera when the game starts
        playerCamera.enabled = true;  // Enable the Player Camera
        keyLogger.SetActive(true);
        //afterRunCanvas.SetActive(false);

        Debug.Log($"Selected Set: {folderName}. Game Started!");
    }
}
