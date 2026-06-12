using UnityEngine;
using UnityEngine.SceneManagement;  // Needed for restarting the session and scene management
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    
    public GameObject inGameMenuCanvas;  // Reference to the Pause Menu Canvas
    public GameObject gameObjects;       // Reference to all gameplay objects
    public GameObject setSelectionCanvas;  // Reference to the Set Selection Canvas
    public GameObject mainMenuCanvas;
    public GameObject runSelectionCanvas;
    public GameObject afterRunCanvas;
    private bool isPaused = false;
    public ImageManager imageManager;
    public KeyPressLogger keyPressLogger;
    public GameObject testSetCanvas;

    public Camera uiCamera;  // Reference to the UICamera
    public Camera playerCamera;  // Reference to the Player Capsule Camera

    private void Start()
    {
        // Ensure gameplay objects are active
        gameObjects.SetActive(true);

    // Hide all menus at the start of the session
       // MainMenuCanvas.SetActive(false);
        //SetSelectionCanvas.SetActive(false);
        // Ensure the Pause Menu starts disabled
        inGameMenuCanvas.SetActive(false);
    }

    private void Update()
    {
        // Press ESC to toggle pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        // Show the Pause Menu
        inGameMenuCanvas.SetActive(true);
        Time.timeScale = 0f;  // Pause game time
        isPaused = true;
    }

    public void ResumeGame()
    {
        // Hide the Pause Menu
        inGameMenuCanvas.SetActive(false);
        Time.timeScale = 1f;  // Resume game time
        isPaused = false;
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
        Debug.Log("Game Quit!");  // Logs for testing in Unity Editor
    }

    public void RestartSession()
    {
        // Reload the current scene to restart the session
        Time.timeScale = 1f;  // Ensure the game time is running
        // Destroy all DontDestroyOnLoad objects before reloading
        foreach (var rootObj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            Destroy(rootObj);
        }


        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        mainMenuCanvas.SetActive(true);
        inGameMenuCanvas.SetActive(false);
        gameObjects.SetActive(false);
        uiCamera.enabled = true;  // Enable the UICamera when the game starts
        playerCamera.enabled = false;  // disable the Player Camera
        
    }

    public void BackToMainMenu()
    {
        // Return to set selection menu
        Time.timeScale = 1f;  // Ensure the game time is running
        inGameMenuCanvas.SetActive(false);
        // Destroy all DontDestroyOnLoad objects before reloading
        foreach (var rootObj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            Destroy(rootObj);
        }


        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        
        //runSelectionCanvas.SetActive(true);  // Show the set selection canvas
        afterRunCanvas.SetActive(false);

        gameObjects.SetActive(false);
        //Time.timeScale = 0f; // Keep game paused
        uiCamera.enabled = true;  // Enable the UICamera when the game starts
        playerCamera.enabled = false;  // disable the Player Camera

        Canvas.ForceUpdateCanvases();
        
    }

    public void MoveToTestSet()
    {
        Time.timeScale = 1f;
        inGameMenuCanvas.SetActive(false);
        testSetCanvas.SetActive(true);
        gameObjects.SetActive(false);
        uiCamera.enabled = true;  // Enable the UICamera when the game starts
        playerCamera.enabled = false;  // disable the Player Camera
        imageManager.gameStarted = false;
        keyPressLogger.gameStarted = false;
        imageManager.elapsedTime = 0f;
        keyPressLogger.elapsedTime = 0f;
    }
}
