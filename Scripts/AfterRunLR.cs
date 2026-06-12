using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class AfterRunLR : MonoBehaviour
{
    public ArduinoInputManagerLR arduinoInputManagerLR;
    public SpeedLogger speedLogger;
    public GameObject gameObjectsLogRecord;
    public GameObject runSelectionCanvas;
    public GameObject afterRunCanvasLR;
     
    public Camera uiCamera;
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        //
    }

    public void MovetoExperimentRun()
    {
        afterRunCanvasLR.SetActive(false);
        gameObjectsLogRecord.SetActive(false);
        runSelectionCanvas.SetActive(true);
        uiCamera.enabled = true;
        mainCamera.enabled = false;
        arduinoInputManagerLR.elapsedTime = 0f;
        speedLogger.elapsedTime = 0f;

    }

}
