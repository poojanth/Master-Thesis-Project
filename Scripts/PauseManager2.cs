using UnityEngine;
using UnityEngine.SceneManagement;  // Needed for restarting the session and scene management
using UnityEngine.UI;

public class PauseManager2 : MonoBehaviour
{
    public GameObject inGameMenuCanvas;  // Reference to the Pause Menu Canvas
    public GameObject gameObjects;       // Reference to all gameplay objects
    
    private bool isPaused = false;

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
        
        inGameMenuCanvas.SetActive(false);
        gameObjects.SetActive(false);
        uiCamera.enabled = true;  // Enable the UICamera when the game starts
        playerCamera.enabled = false;  // disable the Player Camera
        
    }

}
