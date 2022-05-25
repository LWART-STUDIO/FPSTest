using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading;

namespace CodeBase.Logger
{
    public class FileWriter:IDisposable
    {
        private const string DATE_FORMAT = "yyyy-MM-dd";
        private const string LOG_TIME_FORMAT = "{0:dd:MM:yyyy HH:mm:ss:ffff} [{1}]: {2}\r";
        private const int MAX_MESSAGE_LENGTH = 3500;
        
        
        private string _folder;
        private string _filePath;
        private FileAppender _appender;
        private Thread _workingThread;
        private readonly ConcurrentQueue<LogMessage> _messages = new ConcurrentQueue<LogMessage>();
        private bool _disposing;
        private readonly ManualResetEvent _manualResetEvent = new ManualResetEvent(true);

        private Thread _checkNewDateThread;
        private DateTime _prevDate;
        

        public FileWriter(string folder)
        {
            _folder = folder;
            ManagePath();
            _workingThread = new Thread(StoreMessages)
            {
                IsBackground = true,
                Priority = ThreadPriority.BelowNormal
            };
            _workingThread.Start();
            _checkNewDateThread = new Thread(CheckNewDay)
            {
                IsBackground = true,
                Priority = ThreadPriority.BelowNormal
            };
            _checkNewDateThread.Start();
        }

        private void ManagePath()
        {
            _prevDate=DateTime.UtcNow;
            _filePath = $"{_folder}/{DateTime.UtcNow.ToString(DATE_FORMAT)}.log";
        }

        public void Write(LogMessage message)
        {
            try
            {
                if (message.Message.Length > MAX_MESSAGE_LENGTH)
                {
                    var preview = message.Message.Substring(0, MAX_MESSAGE_LENGTH);
                    _messages.Enqueue(new LogMessage(message.Type,$"Message is too long{message.Message.Length}. Preview{preview}")
                    {
                        Time = message.Time
                    });
                }
                else
                {
                    _messages.Enqueue(message);
                    
                }
                _manualResetEvent.Set();
            }
            catch (Exception e)
            {
                
            }
            
        }

        private void StoreMessages()
        {
            while (!_disposing)
            {
                while (!_messages.IsEmpty)
                {
                    try
                    {
                        LogMessage message;
                        if (!_messages.TryPeek(out message))
                        {
                            Thread.Sleep(5);
                        }

                        if (_appender == null || _appender.FileName != _filePath)
                        {
                            _appender = new FileAppender(_filePath);
                        }
                        string messageToWrite = string.Format(LOG_TIME_FORMAT, message.Time, message.Type, message.Message);
                        if (_appender.Append(messageToWrite))
                        {
                            _messages.TryDequeue(out message);
                        }
                        else
                        {
                            Thread.Sleep(5);
                        }
                    }
                    catch (Exception e)
                    {
                        break;
                    }
                }

                _manualResetEvent.Reset();
                _manualResetEvent.WaitOne(500);
            }
        }

        private void CheckNewDay()
        {
            while (!_disposing)
            {
                var currentDate = DateTime.UtcNow;
                if (currentDate.Day != _prevDate.Day)
                {
                    _prevDate = currentDate;
                    ManagePath();
                }
                Thread.Sleep(1000);
            }
        }
        public void Dispose()
        {
            _disposing = true;
            _workingThread?.Abort();
            _checkNewDateThread?.Abort();
            GC.SuppressFinalize(this);
        }
    }
   
}