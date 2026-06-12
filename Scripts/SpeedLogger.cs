using System.Collections;
using System.IO;
using UnityEngine;
using TMPro;  // For TextMeshPro UI
using StarterAssets;
using SFB;

public class SpeedLogger : MonoBehaviour
{
    public FirstPersonController playerController;  // Reference to player movement script
    public TMP_InputField subjectNameInput;  // Input field for subject name
    private string filePath;  // Path to save log file
    private float nextLogTime = 0f;  // Timer for logging interval
    public float elapsedTime = 0f;
    private const float logInterval = 0.3f; // Log every 300ms
    private bool isLoggingEnabled = false; // Flag to enable/disable logging
    public bool gameStarted = false;  // Ensures logging starts after set selection
    private bool isRunning = true;
    private string speedlogfilePath;
    //public GameObject gameCanvas;          // Reference to the Game Canvas
    //public GameObject gameObjects;         // Parent object for gameplay-related objects
    //public GameObject pauseManager;
    //public Camera uiCamera;  // Reference to the UICamera
    //public Camera playerCamera;  // Reference to the Player Capsule Camera

    private void Start()
    {
        // Show log selection UI at the start
        //comPortCanvas.SetActive(false);
        
    }

    private void Update()
    {
        if (!isLoggingEnabled || !gameStarted) return; // Only log if enabled and game has started
        
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            
            if(elapsedTime >= nextLogTime)
            {
                LogSpeed();
                nextLogTime = elapsedTime + logInterval; // Update next log time
            }
        }

        
    }

    public void SpeedLogFileLocation()
    {
        var paths = StandaloneFileBrowser.OpenFolderPanel("Select Log Directory", "", false);
        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            speedlogfilePath = paths[0];
            Debug.Log("Selected Directory: " + speedlogfilePath);
        }
        else
        {
            Debug.LogWarning("No directory selected!");
        }
    }

    // Called when "Enable Logging" button is pressed
    public void EnableLogging()
    {
        gameStarted = true;
        string subjectName = subjectNameInput.text.Trim();

        if (string.IsNullOrEmpty(subjectName))
        {
            Debug.LogError("Subject Name is empty. Please enter a valid name.");
            return;
        }

        // Generate a unique filename using timestamp
        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        filePath = Path.Combine(speedlogfilePath, $"{subjectName}_SpeedLog_{timestamp}.txt");

        // Create an empty file
        File.WriteAllText(filePath, "");

        Debug.Log($"Speed log enabled. File created at: {filePath}");
        isLoggingEnabled = true;
        
        //uiCamera.enabled = false;
        //playerCamera.enabled = true;
        //gameCanvas.SetActive(true);         // Show Game UI
        //gameObjects.SetActive(true);        // Enable gameplay objects
        //pauseManager.SetActive(true);

    }


    private void LogSpeed()
    {
        if (playerController == null)
        {
            Debug.LogWarning("PlayerController not assigned in SpeedLogger!");
            return;
        }

        // Get current timestamp in milliseconds
        int timestamp = Mathf.FloorToInt(elapsedTime * 1000);
        float currentSpeed = playerController.MoveSpeed; // Get player speed

        // Format: "S <timestamp> <speed>"
        string logEntry = $"S {timestamp} {currentSpeed}";

        // Append to the log file if logging is enabled
        if (isLoggingEnabled)
        {
            File.AppendAllText(filePath, logEntry + "\n");
        }

        Debug.Log(logEntry);  // Log in Unity Console for debugging
    }
}
