using System.IO.Ports;
using UnityEngine;
using TMPro; // for TMP_Dropdown

public class COMPortManagerLR : MonoBehaviour
{
    public ArduinoInputManagerLR arduinoManager;  // Reference to ArduinoManager
    public TMP_Dropdown comPortDropdown;   // Reference to TMP_Dropdown
    public GameObject comPortCanvas;       // Reference to COM Port Canvas
    public GameObject gameObjectsLogRecord;  // Reference to Set Selection Canvas
    public GameObject pauseManager;
    public Camera uiCamera;  // Reference to the UICamera
    public Camera playerCamera;  // Reference to the Player Capsule Camera
    
    private void Start()
    {
        comPortCanvas.SetActive(true);
        gameObjectsLogRecord.SetActive(false);
        uiCamera.enabled = true;
        playerCamera.enabled = false;
        // Populate the dropdown with available COM ports
        string[] ports = SerialPort.GetPortNames();
        //comPortDropdown.ClearOptions();
        foreach (string port in ports)
        {
            comPortDropdown.options.Add(new TMP_Dropdown.OptionData(port));
        }

        if (ports.Length > 0)
        {
            comPortDropdown.value = 0; // Default to the first COM port
        }
        else
        {
            Debug.LogError("No COM ports available. Please connect your Arduino.");
        }

        //comPortCanvas.SetActive(false); // Hide the COM Port Canvas by default
        
    }


    public void SetSelectedCOMPort()
    {
        // Get selected COM port from the dropdown
        string selectedPort = comPortDropdown.options[comPortDropdown.value].text;

        // Set the selected COM port
        arduinoManager.GetComponent<SerialController>().portName = selectedPort;
        Debug.Log($"COM Port selected: {selectedPort}");

        // Hide the COM Port Selection canvas and show Set Selection menu
        comPortCanvas.SetActive(false); // Hide COM Port Canvas
        gameObjectsLogRecord.SetActive(true);
        pauseManager.SetActive(true);
        uiCamera.enabled = false;
        playerCamera.enabled = true;

        
       
    }

    public void BackToMainMenu()
    {
        // Hide all canvases and return to Main Menu
        comPortCanvas.SetActive(false);
        
        
    }
}
