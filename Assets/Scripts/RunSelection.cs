using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSelection : MonoBehaviour
{
    public GameObject runSelectionCanvas;
    public GameObject experimentRun;
    public GameObject logRecordingSession;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void RecordSpeedLog()
    {
        runSelectionCanvas.SetActive(false);
        logRecordingSession.SetActive(true);
    }

    public void RunExperiment()
    {
        runSelectionCanvas.SetActive(false);
        experimentRun.SetActive(true);
    }
}
