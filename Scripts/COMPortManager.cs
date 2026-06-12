using System.IO.Ports;
using UnityEngine;
using TMPro; // for TMP_Dropdown

public class COMPortManager : MonoBehaviour
{
    public ArduinoInputManager1 arduinoManager;  // Reference to ArduinoManager
    public TMP_Dropdown comPortDropdown;   // Reference to TMP_Dropdown
    public GameObject comPortCanvas;       // Reference to COM Port Canvas
    public GameObject setSelectionCanvas;  // Reference to Set Selection Canvas
    public GameObject mainMenuCanvas;      // Reference to Main Menu Canvas
    
    private void Start()
    {
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

       // comPortCanvas.SetActive(false); // Hide the COM Port Canvas by default
        setSelectionCanvas.SetActive(false);
    }

    public void OpenCOMPortSelection()
    {
        // Hide Main Menu and show COM Port Selection Canvas
        mainMenuCanvas.SetActive(false);
        comPortCanvas.SetActive(true);
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
        setSelectionCanvas.SetActive(true); // Show Set Selection Canvas
       
    }

    public void BackToMainMenu()
    {
        // Hide all canvases and return to Main Menu
        comPortCanvas.SetActive(false);
        setSelectionCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
