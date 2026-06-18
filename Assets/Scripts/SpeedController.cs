using System.IO.Ports;
using UnityEngine;
using StarterAssets;

public class SpeedController : MonoBehaviour
{
    public FirstPersonController playerController;


    private SerialPort portNo = new SerialPort("COM9", 9600);


    public float minSpeed = 10.0f;
    public float maxSpeed = 20.0f;

    void Start()
    
        {
            portNo.Open();
            portNo.ReadTimeout = 100;
            Debug.Log("Serial port opened successfully.");
        }
        

    void Update()
    {
        if (portNo.IsOpen)
        {
            int buttonState = portNo.ReadByte();
            playerController.MoveSpeed = buttonState == 1 ? maxSpeed : minSpeed;    
        }
    }

    void OnApplicationQuit()
    {
        if (portNo.IsOpen)
        {
            portNo.Close();
            Debug.Log("Serial port closed on application quit.");
        }
    }
}
