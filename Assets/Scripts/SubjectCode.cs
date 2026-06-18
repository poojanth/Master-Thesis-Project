using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // For TextMeshPro UI

public class SubjectCode : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject subjectCodeCanvas;
    public GameObject runSelectionCanvas;
    public TMP_InputField subjectNameInput;  // Input field for subject name
    public TMP_Text statusText;
    // Start is called before the first frame update
    void Start()
    {
        statusText.text = "Enter the Subject Code to Start!";
    }
    // Update is called once per fram

    public void confirm()
    {
        string subjectName = subjectNameInput.text.Trim();

        if (string.IsNullOrEmpty(subjectName))
        {
            statusText.text = "Subject Code is not entered!";
            return;
        }
        

        subjectCodeCanvas.SetActive(false);
        runSelectionCanvas.SetActive(true);

    }
}
