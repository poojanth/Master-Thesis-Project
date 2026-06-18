using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using TMPro;
using SFB;

public class SpeedLogReader : MonoBehaviour
{
    public FirstPersonController1 playerController; // Reference to the player movement script
    public Button uploadLogButton;                // Button to upload the speed log file
    public TMP_Text statusText;                   // Text to display status messages to the user
    public GameObject setselectionCanvas1;
    public GameObject speedLogCanvas;

    private string filePath;                      // Path of the uploaded log file
    private List<KeyValuePair<int, float>> speedLogData = new List<KeyValuePair<int, float>>(); // Parsed speed log data
    private bool isReplaying = false;             // Flag to check if replay mode is active
    private float elapsedTime = 0f;               // Elapsed time in milliseconds
    private int currentIndex = 0; 
    public GameObject gameCanvas;          // Reference to the Game Canvas
    public GameObject gameObjects1;         // Parent object for gameplay-related objects
    public GameObject gameObjects;
    public GameObject pauseManager;
    public Camera uiCamera;  // Reference to the UICamera
    public Camera playerCamera1;  // Reference to the Player Capsule Camera
    public Camera playerCamera;
    public GameObject keyLogger;
    private bool initialRunCompleted = false;

    private void Start()
    {
        // Attach file upload logic to the button
        uploadLogButton.onClick.AddListener(OpenFileBrowser);
        statusText.text = "Upload a speed log file to start!";
    }

    private void Update()
    {
        if (isReplaying && speedLogData.Count > 0)
        {
            elapsedTime += Time.deltaTime * 1000f; // Convert elapsed time to milliseconds

            // If we reached the last entry, restart from the beginning
            if (currentIndex >= speedLogData.Count)
            {
                initialRunCompleted = true;
                currentIndex = FindFirstNonZeroIndex(); // Find the first non-zero index
                if (currentIndex == -1)
                {
                    Debug.LogWarning("All speed values are zero. Keeping speed at the last value.");
                    return; // If all values are zero, stop updating to prevent a freeze
                }
                

                elapsedTime = speedLogData[currentIndex].Key; // Sync elapsed time to first valid timestamp
                Debug.Log($"Restarting speed log from index {currentIndex} at timestamp {elapsedTime} ms.");
            }

            if(initialRunCompleted)
            {
                while (currentIndex < speedLogData.Count && speedLogData[currentIndex].Value == 0)
                {
                    Debug.Log($"Skipping zero speed at index {currentIndex}, timestamp {speedLogData[currentIndex].Key} ms.");
                    currentIndex++; // Move to the next available speed value
                }
            }

            // Check if the current timestamp is reached
            if (elapsedTime >= speedLogData[currentIndex].Key)
            {
                float speed = speedLogData[currentIndex].Value;
                playerController.MoveSpeed1 = speed; // Set the player's speed
                currentIndex++; // Move to the next log entry
            }
        }
    }

    private int FindFirstNonZeroIndex()
    {
        for (int i = 0; i < speedLogData.Count; i++)
        {
            if (speedLogData[i].Value > 1) // If speed is greater than 0, return this index
            {
                return i;
            }
        }
        return -1; // If all values are 0 (unlikely), restart from the first entry
    }

    // Open file browser dialog to upload speed log
    private void OpenFileBrowser()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Select Speed Log File", "", "txt", false);

        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            filePath = paths[0];
        }
        else
        {
            statusText.text = "No file selected!";
        }
    }

    // Load and parse the speed log file
    private void LoadSpeedLog(string path)
    {
        speedLogData.Clear(); // Clear previous data

        string[] lines = File.ReadAllLines(path);
        foreach (string line in lines)
        {
            string[] parts = line.Split(' ');
            if (parts.Length == 3 && parts[0] == "S")
            {
                int timestamp = int.Parse(parts[1]); // Timestamp in milliseconds
                float speed = float.Parse(parts[2]); // Speed value
                speedLogData.Add(new KeyValuePair<int, float>(timestamp, speed));
            }
        }

        statusText.text = $"Loaded the file with {speedLogData.Count} entries!";
    }


    // Start the replay mode
    private void StartReplay()
    {
        if (speedLogData.Count == 0)
        {
            Debug.LogError("Speed log data is empty! Cannot start replay.");
            return;
        }

        isReplaying = true;
        elapsedTime = 0f;
        currentIndex = 0;
        Debug.Log("Replay mode started using speed log.");
    }

    public void ConfirmFile()
    {
        //setselectionCanvas1.SetActive(true);
        LoadSpeedLog(filePath);
        StartReplay();
        gameCanvas.SetActive(true);         // Show Game UI
        gameObjects1.SetActive(true);        // Enable gameplay objects
        gameObjects.SetActive(false);
        pauseManager.SetActive(true);
        uiCamera.enabled = false;  // Disable the UICamera when the game starts
        playerCamera.enabled = false;  // Enable the Player Camera
        playerCamera1.enabled = true;
        keyLogger.SetActive(true);
    }
}
