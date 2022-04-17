using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw05_delegates_and_events
{
    // 2. Написать класс, обходящий каталог файлов и выдающий событие при нахождении каждого файла;
    // 3. Оформить событие и его аргументы с использованием .NET соглашений: public event EventHandler FileFound;
    //    FileArgs – будет содержать имя файла и наследоваться от EventArgs
    // 4. Добавить возможность отмены дальнейшего поиска из обработчика;

    public class FileArgs : EventArgs
    {
        public string filePath { get; set; }
        public FileArgs(string filePath = "")
        {
            this.filePath = filePath;
        }
    }

    public class FileSystemScanner
    {
        public string path { get; set; }
        public event EventHandler<FileArgs> RaiseFileFoundEvent;
        private bool bProcess = true;
        public int counter { get; set; }

        public void Scan(string path)
        {
            bProcess = true;
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
                Console.WriteLine("Path "+path+" doesn't exist.");
            }
        }

        public void ProcessDirectory(string targetDirectory)
        {
            if (!bProcess) return;

            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                if (!bProcess) return;
                ProcessFile(fileName);
            }

            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                if (!bProcess) return;
                ProcessDirectory(subdirectory);
            }
        }

        public void ProcessFile(string path)
        {
            counter++;
            RaiseFileFoundEvent(this, new FileArgs(path));
        }

        public void StopScanning()
        {
            Console.WriteLine("Прерывание обработки");
            bProcess = false;
        }
    }
}
