using System.Diagnostics;
using UnityEngine;
using System.Threading.Tasks;

public class RoombaController : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 100.0f;
    public float detectionRange = 0.1f;
    private bool isRotating = false;
    private float rotationAngle = 0f;

    private Process pythonProcess;

    void Update()
    {
        if (!isRotating)
        {
            MoveForward();
            CheckForObstacles();
        }
        else
        {
            RotateRoomba(rotationAngle);
        }
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    async void CheckForObstacles()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRange))
        {
            await CallPythonScriptAsync();
        }
    }

    async Task CallPythonScriptAsync()
    {
        isRotating = true;

        pythonProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "wsl",
                Arguments = "python3 /mnt/c/Users/81909/Desktop/Unity/CSPy/Assets/PythonScripts/roomba_navigation.py",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        pythonProcess.Start();
        string output = await pythonProcess.StandardOutput.ReadToEndAsync();
        pythonProcess.WaitForExit();

        rotationAngle = float.Parse(output);
    }

    void RotateRoomba(float angle)
    {
        transform.Rotate(0, angle, 0);
        isRotating = false;
    }
}
