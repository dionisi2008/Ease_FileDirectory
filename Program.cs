using System;
using System.IO;

namespace Ease_FileDirectory
{
    class Program       
    {
        static void Main(string[] args)
        {
            EaseFileCompact BetaTest = new EaseFileCompact(@"D:\nas.edc");
            BetaTest.ComposeDirectory(new DirectoryInfo(@"D:\all"), null);
            string[] list = BetaTest.GetListFiles();
            //BetaTest.DecomposeFiles(new DirectoryInfo(@"C:\Users\tania\Desktop\out1"));
            for (int shag = 0; shag <= list.Length - 1; shag++)
            {
                System.Console.WriteLine(list[shag]);
            }
        }
    }
}
