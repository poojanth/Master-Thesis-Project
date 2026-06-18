using System.Collections;
using System.IO;
using UnityEngine;
using TMPro;
using SFB;

public class KeyPressLogger : MonoBehaviour
{
    public ImageManager imageManager;
    public ImageManager1 imageManager1;
    private string logFilePath;  // Path for logging key presses
    public float elapsedTime = 0f; // Time since game start
    public bool gameStarted = false; // Flag to start logging only after game begins
    private string directoryPath;
    public TMP_InputField subjectNameInput;  // Input field for subject name

    private void Start()
    {
        // The file will be created after the game starts
    }

    private void Update()
    {
        if (!gameStarted) return; // Only log when the game has started

        elapsedTime += Time.deltaTime;

        // Check if the user presses "1" or "2"
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            LogKeyPress("49"); // ASCII code for '1'
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            LogKeyPress("50"); // ASCII code for '2'
        }
    }

    public void StartKeyLogging()
    {
        gameStarted = true; // Allow logging when the game starts
        CreateLogFile();
        Debug.Log("Key press logging started.");
    }

    public void StartKeyLoggingRep()
    {
        gameStarted = true; // Allow logging when the game starts
        CreateLogFileRep();
        Debug.Log("Key press logging started.");
    }

    public void ChooseFileLocation()
    {
        var paths = StandaloneFileBrowser.OpenFolderPanel("Select Log Directory", "", false);
        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            directoryPath = paths[0];
            Debug.Log("Selected Directory: " + directoryPath);
        }
        else
        {
            Debug.LogWarning("No directory selected!");
        }
    }

    public string DirectoryPath()
    {
        return directoryPath;
    }

    private void CreateLogFile()
    {
        string subjectName = subjectNameInput.text.Trim();

        string setName = imageManager.selectedSet;


        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        logFilePath = Path.Combine(directoryPath, $"{subjectName}_KeyPressLog_{setName}_{timestamp}.txt");

        File.WriteAllText(logFilePath, ""); // Create an empty file
        Debug.Log($"Key press log file created at: {logFilePath}");
    }

    private void CreateLogFileRep()
    {
        string subjectName = subjectNameInput.text.Trim();

        string setName = imageManager1.selectedSet;


        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        logFilePath = Path.Combine(directoryPath, $"{subjectName}_KeyPressLog_{setName}_{timestamp}.txt");

        File.WriteAllText(logFilePath, ""); // Create an empty file
        Debug.Log($"Key press log file created at: {logFilePath}");
    }

    private void LogKeyPress(string asciiCode)
    {
        int timestamp = Mathf.FloorToInt(elapsedTime * 1000); // Convert to milliseconds
        string logEntry = $"R {timestamp} {asciiCode}";


        File.AppendAllText(logFilePath, logEntry + "\n");
        Debug.Log(logEntry); // Log in Unity console for debugging
    }
}
