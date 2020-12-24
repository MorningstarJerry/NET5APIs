using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyNet5APIs
{
    public class EventTest
    {
        public event EventHandler<FileFoundArgs> FileFound;

        public int Add(int a, int b) => a + b;

        public EventTest()
        {
        }

        public void Test()
        {
            int filesFound = 0;
            EventHandler<FileFoundArgs> onFileFound = (sender, eventArgs) =>
            {
                Console.WriteLine(eventArgs.FoundFile);
                filesFound++;
            };

            FileSearcher fileLister = new FileSearcher();
            fileLister.FileFound += onFileFound;
        }
    }

    public class FileFoundArgs : EventArgs
    {
        public string FoundFile { get; }

        public FileFoundArgs(string fileName)
        {
            FoundFile = fileName;
        }
    }

    public class FileSearcher
    {
        public event EventHandler<FileFoundArgs> FileFound;

        public void Search(string directory, string searchPattern)
        {
            foreach (var file in Directory.EnumerateFiles(directory, searchPattern))
            {
                FileFound?.Invoke(this, new FileFoundArgs(file));
            }
        }
    }
}