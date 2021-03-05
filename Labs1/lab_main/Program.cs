using System;

namespace lab_main
{

  class Program
  {
    static void Main(string[] args)
    {
      while (true)
      {
        Console.WriteLine("Выберите задание, которое хотите выполнить (от 1 до 5) или 0, чтобы выйти из программы");
        switch (Console.ReadLine())
        {
          case "1": lab1.pract1(); break;
          case "2": lab2.pract2(); break;
          case "3": lab3.pract3(); break;
          case "4": lab4.pract4(); break;
          case "5": lab5.pract5(); break;
          case "0": return;

          default:
            Console.WriteLine("Вы не выбрали значение");
            break;
        }
      }


  
    }
  }
}

