using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class AfterRun : MonoBehaviour
{
    public FirstPersonController firstPersonController;
    public GameObject gameObjects;
    public GameObject testSetCanvas;
    public GameObject afterRunCanvas;
    public ImageManager imageManager;
    public KeyPressLogger keyPressLogger; 
    public Camera uiCamera;
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        //
    }

    public void TestSetSelection()
    {
        afterRunCanvas.SetActive(false);
        testSetCanvas.SetActive(true);
        gameObjects.SetActive(false);
        uiCamera.enabled = true;
        mainCamera.enabled = false;
        imageManager.gameStarted = false;
        keyPressLogger.gameStarted = false;
        imageManager.elapsedTime = 0f;
        keyPressLogger.elapsedTime = 0f;

    }

    public void TestSetPresentation()
    {
        firstPersonController.MoveSpeed = 0f;
        imageManager.firstPopUpDistance = 0f;
        imageManager.gameStarted = true;
        
    }

}
