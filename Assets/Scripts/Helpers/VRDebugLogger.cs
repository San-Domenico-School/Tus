using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class VRDebugLogger : MonoBehaviour
{
    private TextMeshProUGUI debugText; // Reference to TMP Text component
    private int maxLines = 10; // Maximum number of lines displayed
    private Queue<string> logQueue = new Queue<string>();

    private static VRDebugLogger instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Automatically find the TextMeshProUGUI component inside the Canvas
        debugText = GetComponentInChildren<TextMeshProUGUI>();

        if (debugText == null)
        {
            Debug.LogError("VRDebugLogger: No TextMeshProUGUI component found! Make sure your Canvas has a child object with a TextMeshPro - Text (UI) component.");
            return;
        }

        Application.logMessageReceived += HandleLog;
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (logQueue.Count >= maxLines)
        {
            logQueue.Dequeue();
        }

        logQueue.Enqueue(logString);
        UpdateDebugText();
    }

    private void UpdateDebugText()
    {
        debugText.text = string.Join("\n", logQueue);
    }
}