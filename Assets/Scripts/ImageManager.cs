using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;  // For TextMeshPro UI

public class ImageManager : MonoBehaviour
{
    public ImageManager imageManager;
    public Image PopImage; // Reference to the UI Image in Game Canvas
    public BlinkingSquare blinkingSquare;
    public float firstPopUpDistance = 10.0f;
    public float range1 = 3.0f;
    public float range2 = 3.1f;
    public float displayDuration = 0.5f;
    public Transform player;

    private List<Sprite> popUpSprites = new List<Sprite>();
    private Vector3 initialPosition;
    private float nextPopUpTime = 0f;
    public bool hasFirstPopUpShown = false;
    public bool gameStarted = false;
    public string selectedSet;
    private int currentIndex = 0; // Track which image to show next
    public GameObject afterRunCanvas;
    public KeyPressLogger filemanager;
    public SpeedLogger1 speedLogger1;

    private string logFilePath; // Path for the image log file
    public TMP_InputField subjectNameInput;  // Input field for subject name
    public float elapsedTime = 0f; // Track elapsed time since the game started

    private void Start()
    {
        PopImage.gameObject.SetActive(false);
        afterRunCanvas.SetActive(false);
    }

    private void Update()
    {
        if (!gameStarted || popUpSprites.Count == 0) return;

        elapsedTime += Time.deltaTime;
        if(elapsedTime >= 5f)
        {
            ImageDisplay();
        }

        
    }

    private void ImageDisplay()
    {

        if (!hasFirstPopUpShown)
        {
            float distanceCovered = Vector3.Distance(initialPosition, player.position);
            if (distanceCovered >= firstPopUpDistance)
            {
                ShowNextImage();
                hasFirstPopUpShown = true;
                nextPopUpTime = Time.time + Random.Range(range1,range2);
            }
        }
        else
        {
            if (Time.time >= nextPopUpTime && currentIndex < popUpSprites.Count)
            {
                ShowNextImage();
                nextPopUpTime = Time.time + Random.Range(range1,range2);
            }
        }
    }

    public void TestISI()
    {
        imageManager.range1 = 2.0f;
        imageManager.range2 = 2.1f;
    }

    public void SetImagesFromFolder(string folderName)
    {
        popUpSprites.Clear();
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>(folderName);
        if (loadedSprites.Length > 0)
        {
            popUpSprites.AddRange(loadedSprites);
            ShuffleImages(); // Shuffle once to get random order
            Debug.Log($"Loaded {loadedSprites.Length} images from folder: {folderName}");
        }
        else
        {
            Debug.LogWarning($"No images found in folder: {folderName}");
        }

        selectedSet = folderName;
        // Reset game logic
        initialPosition = player.position;
        currentIndex = 0;
        hasFirstPopUpShown = false;
        gameStarted = true;

        // Create a log file for this session
        CreateLogFile();
    }

    private void ShowNextImage()
    {
        

        Sprite selectedImage = popUpSprites[currentIndex]; // Get the next image in sequence
        PopImage.sprite = selectedImage;
        PopImage.gameObject.SetActive(true);
        blinkingSquare.ShowTriggerColor();


        // Log the image pop-up event
        LogImageEvent(selectedImage.name);

        Invoke(nameof(HideImage), displayDuration);

        currentIndex++; // Move to the next image

        if (currentIndex >= popUpSprites.Count)
        {
            Debug.Log("All images have been shown. Ending scene.");
            Invoke(nameof(EndScene), 3f);
            return;
        }
    }

    private void HideImage()
    {
        PopImage.gameObject.SetActive(false);
        blinkingSquare.ResetColor();
    }

    private void ShuffleImages()
    {
        System.Random random = new System.Random();
        for (int i = popUpSprites.Count - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            Sprite temp = popUpSprites[i];
            popUpSprites[i] = popUpSprites[j];
            popUpSprites[j] = temp;
        }
    }

    private void CreateLogFile()
    {
        string subjectName = subjectNameInput.text.Trim();

        if (string.IsNullOrEmpty(subjectName))
        {
            Debug.LogError("Subject Name is empty. Please enter a valid name.");
            return;
        }

        string path = filemanager.DirectoryPath();
        Debug.Log("Accessed Directory Path in Another Script: " + path);

        string setname = selectedSet;

        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        logFilePath = Path.Combine(path, $"{subjectName}_ImageLog_{setname}_{timestamp}.txt");

        File.WriteAllText(logFilePath, ""); // Create an empty file
        Debug.Log($"Image log file created at: {logFilePath}");
    }

    private void LogImageEvent(string imageName)
    {
        int timestamp = Mathf.FloorToInt(elapsedTime * 1000); // Convert elapsed time to milliseconds
        string imageType = int.Parse(Path.GetFileNameWithoutExtension(imageName)) <= 40 ? "O" : "N";

        string logEntry = $"P {timestamp} {imageName} {imageType}";

        File.AppendAllText(logFilePath, logEntry + "\n");
        Debug.Log(logEntry);
    }

    private void EndScene()
    {
        afterRunCanvas.SetActive(true);
        speedLogger1.gameStarted = false;
        speedLogger1.elapsedTime = 0f;
        //playerController.MoveSpeed1 = 0f;
        gameStarted = false;
        Debug.Log("Scene has ended. All images displayed.");
        // Add any additional logic for stopping the scene or transitioning here
    }
}
