using System;
using System.IO;

namespace Ease_FileDirectory
{
    class Program       
    {
        public void Main(string[] args)
        {
            EaseFileCompact BetaTest = null;
            if (args.Length > 0)
            {
                if (File.Exists(args[0]) == true)
                {
                    BetaTest = new EaseFileCompact(args[0]);
                    System.Console.WriteLine("Здрасвуйте! Работаем с файлом " + args[0] + '\n' + "Что требуеться?");
                    System.Console.WriteLine("DecomposeALL Имя_Папки - Распоковать все в папаку X");
                    System.Console.WriteLine("ListFiles - Вывести список файлов");
                    System.Console.WriteLine("ReadBytes Имя_Файла - вЫвести массив байт определеного файла");
                    System.Console.WriteLine("WriteFile Имя_Файла Каталог_Сохранения - Запись на жесткий диск определенныЙ файла");
                    string[] ReadConsole = System.Console.ReadLine().Split(' ');
                    switch (ReadConsole[0])
                    {
                        case "ListFiles":
                            System.Console.WriteLine(BetaTest.GetListFiles());
                            Main(args);
                        break;
                        case "DecomposeALL":
                            BetaTest.DecomposeFiles(new DirectoryInfo(ReadConsole[1]));
                            Main(args);
                        break;
                        case "ReadBytes":
                            byte[] TempOut = BetaTest.ReadBytes(ReadConsole[1]);
                            for ( int shag = 0; shag <= TempOut.Length - 1; shag++)
                            {
                                System.Console.WriteLine(TempOut[shag]);
                            }
                            Main(args);
                        break;
                        case "WriteFile":
                        BetaTest.WriteFile(ReadConsole[1], ReadConsole[2]);
                        Main(args);
                        break;
                        
                    }
                }
                else
                {
                    NewFile(args[0], new EaseFileCompact(args[0]));
                }
            }
            else
            {
                System.Console.WriteLine("Укажите путь к файлу с которым придеться работать.");
                string path = System.Console.ReadLine();
                NewFile(path, BetaTest);
                Main(new string[] {path});

            }
        }
        public void NewFile(string  path, EaseFileCompact EaSeEDC)
        {
            if (File.Exists(path) == false)
                {
                    System.Console.WriteLine("Такого файла в системе нет! Он будет создан! Выберите каколог для укамплектации файлов.");
                    EaSeEDC.ComposeDirectory(new DirectoryInfo(System.Console.ReadLine()), null);
                    System.Console.WriteLine("Фаил создан. Перезапуск.");
                    Main(new string[] {path});
                }
        }
    }
}
