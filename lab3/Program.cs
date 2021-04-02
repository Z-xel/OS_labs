using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace Lab3
{
  class Program
  {
    public static List<int> shelf = new List<int>() { };

    static void Main(string[] args)
    {
      // Console.WriteLine("Press any key to start");


      
      Thread newproducer_f = new Thread(new ThreadStart(Producer));
      Thread newproducer_s = new Thread(new ThreadStart(Producer));
      Thread newproducer_t = new Thread(new ThreadStart(Producer));
      Thread newcustomer_f = new Thread(new ThreadStart(Customer));
      Thread newcustomer_s = new Thread(new ThreadStart(Customer));
      newproducer_f.Start();
      newproducer_s.Start();
      newproducer_t.Start();
      newcustomer_f.Start();
      newcustomer_s.Start();
      key({
        newproducer_f,
        newproducer_s,
        newproducer_t
      });
      while (shelf.Count < 100 && shelf.Count > 80)
      {
        newproducer_f.Abort();
        newproducer_s.Abort();
        newproducer_t.Abort();
        if (shelf.Count < 80)
        {
          newproducer_f.Start();
          newproducer_s.Start();
          newproducer_t.Start();
        }
      }
      
    }
      private async static void key(Thread[] threads) {
        char c = await Task.Run(() => Console.ReadKey().KeyChar);
      if (c == 'q') 
        for (int i = 0; i < threads.Length; i++) 
          threads[i].Abort();
      }

    
 
      
    
    
   

    private static void Producer()
    {
      Random newitem = new Random();
      int a;

      while (true)
      {
        if (shelf.Count < 100)
        {
          a = newitem.Next(10, 100);
          shelf.Add(a);
          Console.WriteLine("Added " + a);

          Thread.Sleep(newitem.Next(300, 1500));
        }
        else Thread.Sleep(newitem.Next(300, 1500));
      }
    }
    private static void Customer()
    {
      Random pickitem = new Random();
      int b;

      while (true)
      {
        b = pickitem.Next(0, shelf.Count);
        if (shelf.Count > 0)
        {
          shelf.RemoveAt(b);
          Console.WriteLine("Removed " + b);
          Thread.Sleep(pickitem.Next(200, 2000));
        }
        Thread.Sleep(pickitem.Next(200, 2000));
      }
    }
  }
}