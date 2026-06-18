using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject comPortCanvas;
    public GameObject setSelectionCanvas;
    public GameObject setSelectionCanvas1;
    public GameObject testSetCanvas;
    public GameObject testSetReplayCanvas;
    public GameObject speedLogCanvas;
    public Camera uiCamera;  // Reference to the UICamera
    public Camera playerCamera;  // Reference to the Player Capsule Camera
    public GameObject keyLogger;

    private void Start()
    {
        uiCamera.enabled = true;  // Enable the UICamera initially
        playerCamera.enabled = false;  // Disable the Player Camera initially
        mainMenuCanvas.SetActive(true);
        comPortCanvas.SetActive(false);
        setSelectionCanvas.SetActive(false);
        setSelectionCanvas1.SetActive(false);
        speedLogCanvas.SetActive(false);
        keyLogger.SetActive(false);
        testSetCanvas.SetActive(false);
        testSetReplayCanvas.SetActive(false);
    }

    public void OnPlayButtonClicked()
    {
        mainMenuCanvas.SetActive(false);
        comPortCanvas.SetActive(true);
    }

    public void OnReplaySession()
    {
        //speedLogCanvas.SetActive(true);
        setSelectionCanvas1.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }


    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
