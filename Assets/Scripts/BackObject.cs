using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackObject : MonoBehaviour
{
    public GameObject startMenuCanvas;
    public GameObject subjectCodeCanvas;
    public GameObject runSelectionCanvas;
    public GameObject mainMenuCanvas;
    public GameObject comPortCanvas;
    public GameObject setSelectionCanvas;
    public GameObject setSelectionCanvas1;
    public GameObject speedLogCanvas;
    public GameObject comPortCanvasLR;
    public GameObject experimentRun;
    public GameObject logRecordingSession;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SubjectCode()
    {
        startMenuCanvas.SetActive(true);
        subjectCodeCanvas.SetActive(false);
    }

    public void RunSelection()
    {
        subjectCodeCanvas.SetActive(true);
        runSelectionCanvas.SetActive(false);
    }

    public void MainMenu()
    {
        runSelectionCanvas.SetActive(true);
        experimentRun.SetActive(false);

    }

    public void ComPort()
    {
        mainMenuCanvas.SetActive(true);
        comPortCanvas.SetActive(false);
    }

    public void SetSelect()
    {
        comPortCanvas.SetActive(true);
        setSelectionCanvas.SetActive(false);
    }

    public void SetSelect1()
    {
        mainMenuCanvas.SetActive(true);
        setSelectionCanvas1.SetActive(false);
    }

    public void SpeedLog()
    {
        setSelectionCanvas1.SetActive(true);
        speedLogCanvas.SetActive(false);
    }

    public void ComPortLR()
    {
        runSelectionCanvas.SetActive(true);
        logRecordingSession.SetActive(false);
    }
}
