using UnityEngine;
using UnityEngine.SceneManagement;  // Needed for restarting the session and scene management
using UnityEngine.UI;

public class PauseManager1 : MonoBehaviour
{
    public GameObject inGameMenuCanvas;  // Reference to the Pause Menu Canvas
    public GameObject gameObjects1;       // Reference to all gameplay objects
    public GameObject setSelectionCanvas1;  // Reference to the Set Selection Canvas
    public GameObject mainMenuCanvas;
    private bool isPaused = false;
    public GameObject afterRunCanvas;
    public ImageManager1 imageManager1;
    public KeyPressLogger keyPressLogger; 
    public GameObject testSetReplayCanvas;
    public GameObject speedLogCanvas;

    public Camera uiCamera;  // Reference to the UICamera
    public Camera playerCamera;  // Reference to the Player Capsule Camera

    private void Start()
    {
        // Ensure gameplay objects are active
        gameObjects1.SetActive(true);

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
        gameObjects1.SetActive(false);
        uiCamera.enabled = true;  // Enable the UICamera when the game starts
        playerCamera.enabled = false;  // disable the Player Camera
        
    }

    public void BackToMainMenu()
    {
        // Return to set selection menu
        Time.timeScale = 1f;  // Ensure the game time is running
        //inGameMenuCanvas.SetActive(false);
        // Destroy all DontDestroyOnLoad objects before reloading
        foreach (var rootObj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            Destroy(rootObj);
        }


        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        //runSelectionCanvas.SetActive(true);  // Show the set selection canvas
        afterRunCanvas.SetActive(false);

        gameObjects1.SetActive(false);
        //Time.timeScale = 0f; // Keep game paused
        uiCamera.enabled = true;  // Enable the UICamera when the game starts
        playerCamera.enabled = false;  // disable the Player Camera
        
    }

    public void MoveToReplayTestSet()
    {
        Time.timeScale = 1f;
        inGameMenuCanvas.SetActive(false);
        speedLogCanvas.SetActive(false);
        testSetReplayCanvas.SetActive(true);
        gameObjects1.SetActive(false);
        uiCamera.enabled = true;  // Enable the UICamera when the game starts
        playerCamera.enabled = false;  // disable the Player Camera
        imageManager1.gameStarted = false;
        keyPressLogger.gameStarted = false;
        imageManager1.elapsedTime = 0f;
        keyPressLogger.elapsedTime = 0f;
    }
}
