using System;
using System.IO;

namespace Ease_FileDirectory
{
    class Program       
    {
        static void Main(string[] args)
        {
            EaseFileCompact BetaTest = new EaseFileCompact("/home/denis/Denis.txt");
            BetaTest.WriteFile(BetaTest.GetListFiles()[1].Split(' ')[0], "/home/denis/tt.jpeg");
            
            //if (yy.Exists == true)
            //{
                //yy.Delete(true);
                //File.Delete("./Denis.txt");
            //}   
            
            
           // yy.Create();
            //Random rand = new Random(DateTime.Now.Millisecond);
            
            //for (int a = 0; a < 200; a++)
            //{
                //System.IO.FileStream temp = System.IO.File.Create(yy.FullName + '/' + rand.Next(0, 200000000).ToString() + ".txt");
                //temp.Write(new byte[666], 0, 666);
                //temp.Close();
            //}
            //FileInfo[] files_List = yy.GetFiles();
            //FileStream yu = File.Create("Denis.txt");
            //string[] Strings_T = new string[files_List.Length + 1];
            //for (int shag = 0; shag < files_List.Length; shag++)
            //{
                //yu.Write(File.ReadAllBytes(files_List[shag].FullName), 0,  System.Convert.ToInt32(files_List[shag].Length));
                 //Strings_T[shag] = (yu.Position - files_List[shag].Length).ToString() + '|' + yu.Position.ToString() + '|' + files_List[shag].Name;
            //}
            //Strings_T[Strings_T.Length - 1] = (System.Text.UTF8Encoding.UTF8.GetBytes(string.Concat(Strings_T)).Length + (2 * Strings_T.Length)).ToString();
            //yu.Close();


            //File.AppendAllLines("Denis.txt", Strings_T);
            //byte[] tttemp = new byte[100];
            //FileStream op = File.Open("Denis.txt", FileMode.Open);
            //op.Position = op.Length - 100;
            //op.Read(tttemp, 0, 100);
            //string[] tr = System.Text.UTF8Encoding.UTF8.GetString(tttemp).Split("\n");
            //op.Position = op.Length - System.Convert.ToInt32(tr[tr.Length - 2]);
            //tttemp = new byte[Convert.ToInt32(tr[tr.Length - 2])];
            //op.Read(tttemp, 0, Convert.ToInt32(tr[tr.Length - 2]));
            //string[] lisss = System.Text.UTF32Encoding.UTF8.GetString(tttemp).Split("\n");
            //lisss[0] = "0|" + lisss[0].Split('|')[1] + "|" + lisss[0].Split('|')[2]; 
            //System.Console.WriteLine(lisss[0]);
            //op.Position = Convert.ToInt32(lisss[6].Split('|')[0]);
            //tttemp = new byte[Convert.ToInt32(lisss[6].Split('|')[1])];
            //op.Read(tttemp, 0, Convert.ToInt32(lisss[6].Split('|')[1]));
            //File.WriteAllBytes(lisss[6].Split('|')[2], tttemp);
        }
    }
}
