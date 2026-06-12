using UnityEngine;

public class FolderSelection1 : MonoBehaviour
{
    public ImageManager1 imageManager;       // Reference to the ImageManager script
    public GameObject setSelectionCanvas1;  // Reference to the Set Selection Canvas
   // public GameObject gameCanvas;          // Reference to the Game Canvas
    public GameObject gameObjects1;         // Parent object for gameplay-related objects
   // public GameObject gameObjects;
   // public GameObject pauseManager;
    public GameObject speedLogCanvas;
    public Camera uiCamera;  // Reference to the UICamera
    public Camera playerCamera1;  // Reference to the Player Capsule Camera
    //public Camera playerCamera;


    public void SelectSet1(string folderName)
    {
        // Load the images from the selected set into the ImageManager
        imageManager.SetImagesFromFolder(folderName);

        // Show the game UI and start the game
        setSelectionCanvas1.SetActive(false); // Hide Set Selection Menu
        speedLogCanvas.SetActive(true);
        //logSelectionCanvas.SetActive(false);
       // gameCanvas.SetActive(true);         // Show Game UI
        //gameObjects1.SetActive(true);        // Enable gameplay objects
        //gameObjects.SetActive(false);
        //pauseManager.SetActive(true);
        //uiCamera.enabled = false;  // Disable the UICamera when the game starts
        //playerCamera.enabled = false;  // Enable the Player Camera
       // playerCamera1.enabled = true;

        Debug.Log($"Selected Set: {folderName}. Game Started!");
    }

    public void SelectTestSet(string folderName)
    {
      imageManager.SetImagesFromFolder(folderName);
      gameObjects1.SetActive(true);
      uiCamera.enabled = false;
      playerCamera1.enabled = true;

    }
}
