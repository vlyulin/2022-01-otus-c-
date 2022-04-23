using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw05_delegates_and_events
{
    public class FileSystemScanner
    {
        public event EventHandler<FileArgs>? RaiseFileFoundEvent;
        private bool _bProcess = true;
        public int counter { get; set; }

        public void Scan(string path)
        {
            _bProcess = true;
            counter = 0;

            if (Directory.Exists(path))
            {
                ProcessDirectory(path);
            }
            else if (File.Exists(path))
            {
                ProcessFile(path);
            }
            else
            {
                Console.WriteLine("Path " + path + " doesn't exist.");
            }
        }

        public void ProcessDirectory(string targetDirectory)
        {
            if (!_bProcess) return;

            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                if (!_bProcess) return;
                ProcessFile(fileName);
            }

            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                if (!_bProcess) return;
                ProcessDirectory(subdirectory);
            }
        }

        public void ProcessFile(string path)
        {
            counter++;
            if (RaiseFileFoundEvent != null)
            {
                RaiseFileFoundEvent(this, new FileArgs(path));
            }
        }

        public void StopScanning()
        {
            Console.WriteLine("Прерывание обработки");
            _bProcess = false;
        }
    }
}
