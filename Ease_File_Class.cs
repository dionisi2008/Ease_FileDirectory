using System;
using System.IO;
public class EaseFileCompact
{
    protected string PathFile;
    protected InfoFile[] ListFiles;
    protected FileStream StreamRead;
    protected FileStream StreamWrite;

   

    public EaseFileCompact(string Path)
    {
        this.PathFile = Path;
        if (File.Exists(Path) == true)
        {
            StreamRead = File.Open(Path, FileMode.Open); //Создает поток с файлом
            StreamRead.Position = StreamRead.Length - 100; //Выберает позицию последниз 100 байт
            byte[] TempBuffer = new byte[100]; // буфер выделяеться для чтения 100 байт
            StreamRead.Read(TempBuffer, 0, 100); // чтение последних 100 байт в потоке
            string[] TempString = System.Text.UTF8Encoding.UTF8.GetString(TempBuffer).Split("\n"); //декадирование в текст
            // и перемещение в временный сассив строк 
            StreamRead.Position = StreamRead.Length - System.Convert.ToInt32(TempString[TempString.Length - 2]); //предпоследние значение в масиве строк это пазация потока где начинаеться список файлов 
            TempBuffer = new byte[System.Convert.ToInt32(TempString[TempString.Length - 2])]; // выделение памяти для чтения байт со списком файлов
            StreamRead.Read(TempBuffer, 0, TempBuffer.Length); //Процесс чтения списка файлов 
            TempString = System.Text.UTF8Encoding.UTF8.GetString(TempBuffer).Split("\n"); //пребразуем в масив строк
            TempString[0] = "0|" + TempString[0].Split('|')[1] + '|' + TempString[0].Split('|')[2]; //откидывает мусор создаем первую позицию
            ListFiles = new InfoFile[TempString.Length - 1]; //в классе обявляем список файлов
            for (int shag = 0; shag <= ListFiles.Length - 2; shag++) //заполняем это все
            {
                ListFiles[shag] = new InfoFile(GetInt(TempString[shag].Split('|')[0]), GetInt(TempString[shag].Split('|')[1]), TempString[shag].Split('|')[2]);
            }
        }
           
    }
    public string[] GetListFiles() // получаем список строк с списком файлов и их размером
    {
        string[] OutInfo = new string[ListFiles.Length];
        for (int shag = 0; shag <= ListFiles.Length - 2; shag++)
        {
            OutInfo[shag] = ListFiles[shag].GetInfoFile();
        }
        return OutInfo;
    }

    public byte[] ReadBytes(string NameFile)
    {
        for (int shag = 0; shag <= ListFiles.Length -1; shag++)
        {
            if (ListFiles[shag].NameFile == NameFile)
            {
                byte[] TempOut = new byte[ListFiles[shag].Length];
                StreamRead.Position = ListFiles[shag].GetPosition()[0];
                StreamRead.Read(TempOut, 0, ListFiles[shag].Length);
                return TempOut;
            }
        }
        return null;
    }

    public void WriteFile(string NameFile, string SevePath)
    {
        File.WriteAllBytes(SevePath, ReadBytes(NameFile));
    }

    public void DecomposeFiles(System.IO.DirectoryInfo Direcory)
    {
        Direcory.Create();
        for (int shag = 0; shag<= ListFiles.Length - 2; shag++)
        {
            File.WriteAllBytes(Direcory.FullName + '/' + ListFiles[shag].NameFile, ReadBytes(ListFiles[shag].NameFile));
        }
        
    }

    public void ComposeDirectory(System.IO.DirectoryInfo directory, System.IO.FileInfo FileSeve)
    {
        System.IO.FileInfo[] InfoFiles = directory.GetFiles();
        string[] TempListFile = new string[InfoFiles.Length];
        StreamWrite = File.Create(PathFile);
        for (int shag = 0; shag<= InfoFiles.Length - 1; shag++)
        {
            StreamWrite.Write(File.ReadAllBytes(InfoFiles[shag].FullName), 0, GetInt(InfoFiles[shag].Length.ToString()));
            TempListFile[shag] = StreamWrite.Position.ToString() + '|' + (StreamWrite.Position + ListFiles[shag].Length).ToString() + '|' + ListFiles[shag].NameFile;
        }
        StreamWrite.Close();
        File.AppendAllLines(PathFile, TempListFile);
    }
    protected class InfoFile
    {
        public string NameFile;
        public int Length;
        protected int StartPosition, EndPosition;
        public InfoFile(int StartPosition, int EndPosition, string NameFile)
        {
            Length = EndPosition - StartPosition;
            this.StartPosition = StartPosition;
            this.EndPosition = EndPosition;
            this.NameFile = NameFile;
        }
        public string GetInfoFile()
        {
            return NameFile + " " + Length.ToString();
        }

        public int[] GetPosition()
        {
            return new int[] {StartPosition, EndPosition};
        }
    }
    protected int GetInt(string Info)
    {
        return System.Convert.ToInt32(Info);
    }
   
}