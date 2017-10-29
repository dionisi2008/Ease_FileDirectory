using System;
using System.IO;
public class EaseFileCompact
{
    protected InfoFile[] ListFiles;
    protected FileStream StreamRead;

   

    public EaseFileCompact(string Path)
    {
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
    }
    protected int GetInt(string Info)
    {
        return System.Convert.ToInt32(Info);
    }
   
}