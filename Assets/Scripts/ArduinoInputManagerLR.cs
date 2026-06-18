using UnityEngine;
using StarterAssets;

public class ArduinoInputManagerLR : MonoBehaviour
{
    public FirstPersonController playerController;  // Reference to FirstPersonController
    public SpeedLogger speedLogger;
    public float minSpeed = 0f;  // Minimum speed when the button isn't pressed
    public float maxSpeed = 20f;  // Maximum speed
    public float scalingFactor = 1f;  // Factor to convert Arduino speed to Unity speed
    public float elapsedTime = 0f;
    public GameObject afterRunCanvasLR;

    private float nextSerialRead = 0f;  // Timer for serial reads
    private float readInterval = 0.3f;  // Interval for serial reads (3.3Hz)
    private SerialController serialController;  // Ardity's SerialController component

    private void Start()
    {
        // Get the SerialController component from the same GameObject
        serialController = GetComponent<SerialController>();
        if (serialController == null)
        {
            Debug.LogError("SerialController component not found! Please attach SerialController to the ArduinoManager GameObject.");
        }

        if (playerController == null)
        {
            Debug.LogError("FirstPersonController reference not set in ArduinoManager!");
        }
    }

    private void Update()
    {
        // Only read from the serial if the timer allows
        elapsedTime += Time.deltaTime;

        if(elapsedTime >= 125f)
        {
            afterRunCanvasLR.SetActive(true);
            playerController.MoveSpeed = 0f;
            speedLogger.gameStarted = false;
        }

        if (Time.time >= nextSerialRead)
        {
            // Check for new messages from Arduino
            string message = serialController.ReadSerialMessage();

            if (message == null) return;  // No new message

            if (message == SerialController.SERIAL_DEVICE_CONNECTED)
            {
                Debug.Log("Arduino connected!");
                return;
            }

            if (message == SerialController.SERIAL_DEVICE_DISCONNECTED)
            {
                Debug.LogError("Arduino disconnected!");
                playerController.MoveSpeed = 0f;  // Stop the player on disconnection
                return;
            }

            // Process the received message
            OnMessageArrived(message);
            nextSerialRead = Time.time + readInterval;  // Set the next time to read serial input
        }
    }

    public void OnMessageArrived(string message)
    {
        try
        {
            // Parse the speed value received from Arduino
            float receivedSpeed = float.Parse(message.Trim());
            Debug.Log($"Speed Received from Arduino: {receivedSpeed}");

            // Convert and clamp the speed to Unity's range
            float unitySpeed = Mathf.Clamp(receivedSpeed * scalingFactor, minSpeed, maxSpeed);

            // Directly set the player's MoveSpeed
            playerController.MoveSpeed = unitySpeed;
            Debug.Log($"Player MoveSpeed set to: {playerController.MoveSpeed}");
        }
        catch (System.FormatException)
        {
            Debug.LogError($"Invalid speed format received: {message}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error processing message: {ex.Message}");
        }
    }

}
