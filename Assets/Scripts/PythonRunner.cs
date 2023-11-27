// PythonRunner.cs
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class PythonRunner : MonoBehaviour
{
    public MazeGenerator mazeGenerator;

    private void Start()
    {
        RunPythonScript();
    }

    void RunPythonScript()
    {
        string wslCommand = "python3 /mnt/c/Users/81909/Desktop/Unity/CSPy/Assets/PythonScripts/generate_maze.py";

        ProcessStartInfo startInfo = new ProcessStartInfo()
        {
            FileName = "wsl",
            Arguments = wslCommand,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        using (Process process = Process.Start(startInfo))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string mazeData = reader.ReadToEnd();
                if (mazeGenerator != null)
                {
                    mazeGenerator.GenerateMaze(mazeData);
                }
                else
                {
                    UnityEngine.Debug.LogError("MazeGenerator not set in the inspector");
                }
            }

            process.WaitForExit();
        }
    }
}
