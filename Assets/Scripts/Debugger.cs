#if UNITY_IOS
using System.Collections;
using UnityEngine;

namespace Utility_For_iOS
{
    public class LogOnScreen : MonoBehaviour
    {
        public uint maxLogCount = 15; // number of messages to keep
        public int fontSize = 30;
        private readonly Queue _myLogQueue = new();
        
        private void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            _myLogQueue.Enqueue("[" + type + "] " + logString);
            if (type == LogType.Exception) _myLogQueue.Enqueue(stackTrace);
            while (_myLogQueue.Count > maxLogCount)
                _myLogQueue.Dequeue();
        }

        private void OnGUI()
        {

            GUI.skin.label.fontSize = fontSize;
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.Label("\n" + string.Join("\n", _myLogQueue.ToArray()));
            GUILayout.EndArea();
        }
    }
}
#endif