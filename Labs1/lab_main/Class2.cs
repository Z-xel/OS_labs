using System;
using System.IO;

namespace lab_main
{
  class lab2
  {
   public static void pract2()
    {
      string path = @"D:\Documents";
      DirectoryInfo dirInfo = new DirectoryInfo(path);
      if (!dirInfo.Exists)
      {
        dirInfo.Create();
      }
      Console.WriteLine("Введите строку для записи в файл:");
      string text = Console.ReadLine();

      using (FileStream fstream = new FileStream($"{path}\\stringinFile.txt", FileMode.Create))
      {
        byte[] array = System.Text.Encoding.Default.GetBytes(text);
        fstream.Write(array, 0, array.Length);
        Console.WriteLine("Строка записана в файл");
      }

      using (FileStream fstream = File.OpenRead($"{path}\\stringinFile.txt"))
      {
        byte[] array = new byte[fstream.Length];
        fstream.Read(array, 0, array.Length);
        string textFromFile = System.Text.Encoding.Default.GetString(array);
        Console.WriteLine($"Строка из файла: {textFromFile}");
      }
      Console.WriteLine("Удалить файл? y/n");
      string choose = Console.ReadLine();
      switch (choose)
      {
        case "y":
          path = @"D:\Documents\stringinFile.txt";
          FileInfo fileInf = new FileInfo(path);
          if (fileInf.Exists)
          {
            fileInf.Delete();
          }
          Console.WriteLine("Файл удален");
          break;

        case "n":
          Console.WriteLine("Выбрано значение 'Не удалять'");
          break;

        default:
          Console.WriteLine("Значение не выбрано");
          break;
      }
      Console.ReadLine();
    }
  }
}
