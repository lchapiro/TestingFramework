
using System;
using System.Collections.Generic;
using System.IO;
using ADOR;

namespace TestingFramework.Model
{
    public class WorkerSingleton
    {
        private static readonly object Sync = new object();
        private static WorkerSingleton _instance;

        private string _strInputFile;
        public string GetInputFile() { return _strInputFile; }


        private WorkerSingleton()
        {
            //_strConfigFile = string.Empty;
            _strInputFile = string.Empty;
        }

        public static WorkerSingleton Instance
        {
            get
            {
                lock (Sync)
                {
                    if (_instance == null)
                        _instance = new WorkerSingleton();
                }

                return _instance;
            }
        }

        ~WorkerSingleton()
        {
            
        }

        public void ReadArgs()
        {
            string[] args = Environment.GetCommandLineArgs();

            try
            {
                if (args.Length > 1)
                {
                    _strInputFile = args[1];
                    Logger.Instance.Write("Input File: " + _strInputFile);

                    if (!File.Exists(_strInputFile))
                        _strInputFile = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(ex);
            }
        }

    }
}
