using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class AfterRunTest : MonoBehaviour
{
    public FirstPersonController1 firstPersonController1;
    public GameObject gameObjects1;
    public GameObject testSetReplayCanvas;
    public GameObject afterRunReplayCanvas;
    public ImageManager1 imageManager1;
    public KeyPressLogger keyPressLogger; 
    public GameObject speedLogCanvas;
    public Camera uiCamera;
    public Camera mainCamera1;
    // Start is called before the first frame update
    void Start()
    {
        //
    }

    public void TestSetSelection()
    {
        afterRunReplayCanvas.SetActive(false);
        testSetReplayCanvas.SetActive(true);
        gameObjects1.SetActive(false);
        speedLogCanvas.SetActive(false);
        uiCamera.enabled = true;
        mainCamera1.enabled = false;
        imageManager1.gameStarted = false;
        keyPressLogger.gameStarted = false;
        imageManager1.elapsedTime = 0f;
        keyPressLogger.elapsedTime = 0f;

    }

    public void TestSetPresentation()
    {
        firstPersonController1.MoveSpeed1 = 0f;
        imageManager1.firstPopUpDistance = 0f;
        imageManager1.gameStarted = true;
    }

}
