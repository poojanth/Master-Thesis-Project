using UnityEngine;
using TMPro;  // For TextMeshPro UI

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the UI Text
    private float elapsedTime = 0f;
    private bool isRunning = true; // Timer runs when the game starts

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Call this to start the timer
    public void StartTimer()
    {
        isRunning = true;
        elapsedTime = 0f;
    }

    // Call this to stop the timer
    public void StopTimer()
    {
        isRunning = false;
    }

    // Call this to reset the timer
    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerUI();
    }
}
