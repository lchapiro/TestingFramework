using System;
using System.Diagnostics;
using System.IO;

namespace TestingFramework.Model
{
    public class Logger
    {
        private static string _logFilePath = "";
        private static readonly object Sync = new object();

        // Leo, 27.04.2017 let's be a singleton
        private static Logger _instance;

        /// <summary>
        /// Private c'tor
        /// </summary>
        private Logger()
        {

        }

        public static Logger Instance
        {
            get
            {
                lock (Sync)
                {
                    if (_instance == null)
                    {
                        _instance = new Logger();

                        //if (String.IsNullOrWhiteSpace(_logFilePath))
                        {
                            _logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), string.Format("{0}_{1}{2}{3}_{4}{5}{6}.txt",
                                                    System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                                                    DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                                    DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
                        }

                        // Leo, 06.05.2015 Delete file if exists
                        if (File.Exists(_logFilePath))
                            File.Delete(_logFilePath);
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Writes the specified text out, access like "Logger.Instance.Write("")
        /// </summary>
        /// <param name="text">The text.
        /// </param>
        public void Write(string text)
        {
            lock (Sync)
            {
                // Logtext
                string strText = string.Format("{0}: {1}", DateTime.Now, text + Environment.NewLine);
#if(DEBUG)
                Debug.WriteLine(strText);
#endif
                File.AppendAllText(_logFilePath, strText);
            }
        }

        /// <summary>
        /// Writes the exception out, access like "Logger.Instance.Write(ex)
        /// </summary>
        /// <param name="exc"></param>
        public void Write(Exception exc)
        {
            Instance.Write(exc.Message + Environment.NewLine + exc.StackTrace);
        }

        public static string GetFileName()
        {
            return _logFilePath;
        }

        /*
        public static void SetLogPath(string str)
        {
            _logFilePath = Path.Combine(str, string.Format("{0}_{1}{2}{3}.txt",
                                                    System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                                                    DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
        }
        */
    }
}
