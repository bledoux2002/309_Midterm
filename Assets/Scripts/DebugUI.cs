using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

// code source
// https://stackoverflow.com/questions/60228993/putting-debug-log-as-a-gui-element-in-unity

// Put this on any GameObject in the scene
public class DebugUI : MonoBehaviour
{
    // Adjust via the Inspector
    public Text _debugUI;
    public int maxLines = 8;
    private Queue<string> queue = new Queue<string>();
    private string currentText = "";

    void OnEnable()
    {
        Application.logMessageReceivedThreaded += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceivedThreaded -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Delete oldest message
        if (queue.Count >= maxLines) queue.Dequeue();

        queue.Enqueue(logString);

        var builder = new StringBuilder();
        foreach (string st in queue)
        {
            builder.Append(st).Append("\n");
        }

        currentText = builder.ToString();
        _debugUI.text = "Debug Message:\n" + currentText;
    }
}