using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public GameObject gameObject;        // Normal game objects group
    public GameObject gameObject1;      // Duplicated game objects group
    //public GameObject comPortCanvas;    // COM Port selection canvas
    public GameObject speedLogCanvas;   // Speed Log upload canvas
    public GameObject setSelectionCanvas1; // Duplicated set selection canvas


    // Call this after the Speed Log file is uploaded and a set is selected
    public void OnSpeedLogFileUploaded()
    {
        // Hide Speed Log canvas
        speedLogCanvas.SetActive(false);

        // Show the set selection canvas for the replay session
        setSelectionCanvas1.SetActive(true);

        // Ensure normal gameObject remains OFF during replay
        gameObject.SetActive(false);
       // gameObject1.SetActive(true);
    }

    // Call this when a set is selected in setSelectionCanvas1
    public void StartReplaySession()
    {
        // Hide set selection canvas
        

        // Start the replay session with gameObject1
        gameObject.SetActive(false);
        
        //Debug.Log("Replay session started. Normal GameObject is OFF, and GameObject1 is ON.");
    }
}
