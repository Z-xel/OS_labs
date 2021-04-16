using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab3
{
  class Program
  {
    public static List<int> shelf = new List<int>() { };
    static void Main(string[] args)
    {
      /*while (true)
      {
          Producer();
          for (int i = 0; i < shelf.Count; i++)
          {
              Console.WriteLine(shelf[i]);
          }
      }*/
      Thread newproducer = new Thread(new ThreadStart(Producer));
      Thread newproducera = new Thread(new ThreadStart(Producer));
      Thread newproducerb = new Thread(new ThreadStart(Producer));
      Thread newcustomer = new Thread(new ThreadStart(Customer));
      Thread newcustomera = new Thread(new ThreadStart(Customer));
      newproducer.Start();
      newproducera.Start();
      newproducerb.Start();
      newcustomer.Start();
      newcustomera.Start();
      if (Console.ReadKey(true).Key == ConsoleKey.Q)
      {
        newproducer.Abort();
        newproducera.Abort();
        newproducerb.Abort();
        while (shelf.Count != 0)
        {
          Thread.Sleep(200);
        }
        if (shelf.Count == 0)
        {
          newcustomer.Abort();
          newcustomera.Abort();
        }
      }
    }
    private static void Producer()
    {
      while (true)
      {
        if (shelf.Count < 100)
        {
          Random newitem = new Random();
          int a = newitem.Next(10, 100);
          shelf.Add(a);
          Console.WriteLine("Added " + a);
          Thread.Sleep(newitem.Next(300, 1500));
        }
        else if (shelf.Count >= 100)
        {
          while (shelf.Count > 80)
          {
            Random newitem = new Random();
            Thread.Sleep(newitem.Next(300, 1500));
          }
        }
      }
    }
    private static void Customer()
    {
      while (true)
      {
        Random pickitem = new Random();
        int b = pickitem.Next(0, shelf.Count);
        if (shelf.Count > 0)
        {
          if (shelf.Count == 1)
          {
            shelf.RemoveAt(0);
            Console.WriteLine("Removed " + b + " " + shelf.Count);
          }
          if (shelf.Count > 1)
          {
            shelf.RemoveAt(b);
            Console.WriteLine("Removed " + b + " " + shelf.Count);
          }
        }
        Thread.Sleep(pickitem.Next(200, 2000));
      }
    }
  }
}