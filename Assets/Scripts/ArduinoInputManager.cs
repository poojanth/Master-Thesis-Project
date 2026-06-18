using UnityEngine;
using StarterAssets;


public class ArduinoInputManager : MonoBehaviour
{
    private SerialController serialController;
    public float minSpeed = 3f;
    public float maxSpeed = 20f;
    public FirstPersonController playerController;

    void Start()
    {
        serialController = GetComponent<SerialController>();
    }

    void Update()
    {
        string message = serialController.ReadSerialMessage();

        if (message != null)
        {
            if (message == "1")  // Button is pressed
            {
                Debug.Log("Button Pressed!");
                playerController.MoveSpeed = maxSpeed;  // Increase player speed
            }
            else if (message == "0")  // Button is released
            {
                Debug.Log("Button Released!");
                playerController.MoveSpeed = minSpeed;  // Reset to min speed
            }
        }
    }
}
