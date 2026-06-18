using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject subjectCodeCanvas;
    public GameObject runSelectionCanvas;
    public GameObject experimentRun;
    public GameObject logRecordingSession;
    public Camera uiCamera;  // Reference to the UICamera
    //public Camera playerCamera;  // Reference to the Player Capsule Camera

    private void Start()
    {
        uiCamera.enabled = true;  // Enable the UICamera initially
       // playerCamera.enabled = false;  // Disable the Player Camera initially
        startMenu.SetActive(true);
        subjectCodeCanvas.SetActive(false);
        runSelectionCanvas.SetActive(false);
        experimentRun.SetActive(false);
        logRecordingSession.SetActive(false);
    }

    public void OnPlayButton()
    {
        startMenu.SetActive(false);
        subjectCodeCanvas.SetActive(true);
    }


    public void OnQuitButton()
    {
        Application.Quit();
    }
}
