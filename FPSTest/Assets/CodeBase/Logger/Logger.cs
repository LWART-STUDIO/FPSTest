using System;
using System.IO;
using UnityEngine;

namespace CodeBase.Logger
{
    public class Logger : MonoBehaviour
    {
        private string _directory;
        private FileWriter _fileWriter;

        private void Awake()
        {
            _directory = $"{Application.persistentDataPath}/Logs";
            
            if (!Directory.Exists(_directory)) 
                Directory.CreateDirectory(_directory);

            _fileWriter = new FileWriter(_directory);
            Application.logMessageReceivedThreaded += OnLogRecieved;
        }
        
        private void OnLogRecieved(string condition, string stacktrace, LogType type)
        {
            if (condition.StartsWith("!"))
            {
                _fileWriter.Write(new LogMessage(type,condition));
            }
            /*if (type == LogType.Exception)
            {
                _fileWriter.Write(new LogMessage(type,condition));
                _fileWriter.Write(new LogMessage(type,stacktrace));
            }
            else
            {
                _fileWriter.Write(new LogMessage(type,condition));
            }*/
            
        }

        private void Update()
        {
            #if UNITY_EDITOR
            if (Input.GetKey(KeyCode.L))
            {
                OpenInExplover();
            }
            #endif
        }

        public void OpenInExplover()
        {
            UnityEditor.EditorUtility.RevealInFinder(_directory);
        }

        private void OnDestroy()
        {
            _fileWriter.Dispose();
        }
    }
}
