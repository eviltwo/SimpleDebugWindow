using SimpleDebugWindow;
using UnityEngine;

public class SimpleDebugWindowSample : MonoBehaviour
{
    private DebugTextWindow _textWindow;

    private void Start()
    {
        _textWindow = DebugWindowManager.CreateTextWindow("Debug Text");
    }

    private void Update()
    {
        _textWindow.ClearTexts();
        _textWindow.PushText("Hello, world!");
        _textWindow.PushText("This is a sample text.");
        _textWindow.PushText($"ElapsedTime: {Time.time}");
    }
}
