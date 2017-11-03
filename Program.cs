using System;
using System.IO;

namespace Ease_FileDirectory
{
    class Program       
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                if (File.Exists(args[0]) == true)
                {
                EaseFileCompact ReadFile = new EaseFileCompact(args[0]);
                string[] ReadListFiles = ReadFile.GetListFiles();
                for (int shag = 0; shag <= ReadListFiles.Length - 1; shag++)
                    {
                    System.Console.WriteLine(ReadListFiles[shag]);
                    }
                }
            }
            
        }
    }
}
