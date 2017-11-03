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
            string[] TempString = System.Text.ASCIIEncoding.ASCII.GetString(TempBuffer).Split("\r\n"); //декадирование в текст
            // и перемещение в временный сассив строк 
            StreamRead.Position = StreamRead.Length - int.Parse(TempString[TempString.Length - 1]); //предпоследние значение в масиве строк это пазация потока где начинаеться список файлов 
            TempBuffer = new byte[int.Parse(TempString[TempString.Length - 1])]; // выделение памяти для чтения байт со списком файлов
            StreamRead.Read(TempBuffer, 0, TempBuffer.Length); //Процесс чтения списка файлов 
            TempString = System.Text.ASCIIEncoding.ASCII.GetString(TempBuffer).Split("\r\n"); //пребразуем в масив строк
            string[] String1Temp = TempString[0].Split('|');
            TempString[0] = "0|" + String1Temp[String1Temp.Length - 2] + '|' + String1Temp[String1Temp.Length - 1]; //откидывает мусор создаем первую позицию
            ListFiles = new InfoFile[TempString.Length - 1]; //в классе обявляем список файлов
            for (int shag = 0; shag <= ListFiles.Length - 1; shag++) //заполняем это все
            {
                string[] StrArrayFor = TempString[shag].Split('|');
                ListFiles[shag] = new InfoFile(long.Parse(StrArrayFor[0]), long.Parse(StrArrayFor[1]), StrArrayFor[2]);
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
                StreamRead.Read(TempOut, 0, int.Parse(ListFiles[shag].Length.ToString()));
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
        ListFiles = new InfoFile[InfoFiles.Length];
        StreamWrite = File.Create(PathFile);
        for (int shag = 0; shag<= InfoFiles.Length - 1; shag++)
        {
            ListFiles[shag] = new InfoFile(StreamWrite.Position, StreamWrite.Position + InfoFiles[shag].Length, InfoFiles[shag].Name);
            StreamWrite.Write(File.ReadAllBytes(InfoFiles[shag].FullName), 0, GetInt(InfoFiles[shag].Length.ToString()));
            System.Console.WriteLine(((shag * 100) / ListFiles.Length).ToString() + "% = " + StreamWrite.Position.ToString() + '|' + (StreamWrite.Position + ListFiles[shag].Length).ToString() + '|' + ListFiles[shag].NameFile);
        }
        byte[] GetInfoFuction = OutBytesGetStringsListFiles(GetStringsInfoFile(ListFiles));
        StreamWrite.Write(GetInfoFuction, 0, GetInfoFuction.Length);
        GetInfoFuction = System.Text.UTF8Encoding.UTF8.GetBytes((GetInfoFuction.Length * 2).ToString());
        StreamWrite.Write(GetInfoFuction, 0, GetInfoFuction.Length);
        StreamWrite.Close();

        
    }

    protected byte[] OutBytesGetStringsListFiles(string[] GetListFiles)
    {
        string StringOut = null;
        for (int shag = 0; shag <= GetListFiles.Length - 1; shag++)
        {
            StringOut = StringOut + GetListFiles[shag] + "\r\n";
        }
        return System.Text.UTF8Encoding.UTF8.GetBytes(StringOut);
    }


    protected string[] GetStringsInfoFile(InfoFile[] GetListInfoFile)
    {
        string[] TempOut = new string[GetListInfoFile.Length];
        for (int shag = 0; shag <= GetListInfoFile.Length - 1; shag++)
        {
            TempOut[shag] = GetListInfoFile[shag].GetString();
        }
        return TempOut;
    }
    protected class InfoFile    
    {
        public string NameFile;
        public long Length;
        protected long StartPosition, EndPosition;
        public InfoFile(long StartPosition, long EndPosition, string NameFile)
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

        public long[] GetPosition()
        {
            return new long[] {StartPosition, EndPosition};
        }

        public string GetString()
        {
            return this.StartPosition + "|" + this.EndPosition + "|" + this.NameFile;
        }
    }
    protected int GetInt(string Info)
    {
        
        return int.Parse(Info);
    }
   
}