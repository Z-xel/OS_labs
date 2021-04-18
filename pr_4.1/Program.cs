using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace pr_4._1
{
    class Program
    {
        static int pnum = 2;
        static Process[] p = new Process[pnum];
        static string Path = "C:\\Windows\\system32\\mspaint.exe";

        static void Change_prior(Process p, int prior)
        {
            switch(prior)
            {
                case 0:
                    p.PriorityClass = ProcessPriorityClass.Idle;
                    break;
                case 1:
                    p.PriorityClass = ProcessPriorityClass.Normal;
                    break;
                case 2:
                    p.PriorityClass = ProcessPriorityClass.High;
                    break;
                case 3:
                    p.PriorityClass = ProcessPriorityClass.RealTime;
                    break;

            }
        }
        static void Process_Manipulate(Process p, int pnum)
        {
            int switcher_menu2 = 0;
            int chose_menu2 = 0;
            int prior = 0;
            while (true)
            {
                
                Console.WriteLine($"\nМеню {pnum} процесса");

                switch (switcher_menu2)
                {
                    case 0:
                        Console.WriteLine("1.Запустить процесс");
                        Console.WriteLine("2.Назад");

                        break;
                    case 1:
                        Console.WriteLine("1.Остановить процесс");
                        Console.WriteLine("2.Увеличить приоритет");
                        Console.WriteLine("3.Уменьшить приоритет");
                        Console.WriteLine("4.Назад");

                        break;
                }

                chose_menu2 = Convert.ToInt32(Console.ReadLine());

                if ((switcher_menu2 == 1) && (chose_menu2 == 1))
                {
                    switcher_menu2 = 0;
                    chose_menu2 = 0;
                    p.Kill();
                }
                if ((switcher_menu2 == 0)&&(chose_menu2==1))
                {
                    switcher_menu2 = 1;
                    chose_menu2 = 0;
                    p.Start();
                }

                if ((switcher_menu2 == 1) && (chose_menu2 == 2))
                {
                    prior++;
                    chose_menu2 = 0;
                    Change_prior(p, prior);
                }
                if ((switcher_menu2 == 1) && (chose_menu2 == 3))
                {
                    prior--;
                    chose_menu2 = 0;
                    Change_prior(p, prior);
                }
                if (((switcher_menu2 == 1) && (chose_menu2 == 4))|| ((switcher_menu2 == 0) && (chose_menu2 == 2)))
                {
                    return;
                }




            }


        }
        static void Main(string[] args)
        {
            
            
            
            for (int i = 0; i < pnum; i++)
            {
                p[i] = new Process();
                p[i].StartInfo.FileName = Path;
            }

            Console.WriteLine("Добро пожаловать в практическую работу 4");
            int pchosen;
            while (true)
            {
                Console.WriteLine("\nMain Menu");
                for (int i = 0; i < pnum; i++)
                {
                    Console.WriteLine($"{i+1}.Process {i+1}");
                   
                }
                pchosen = Convert.ToInt32(Console.ReadLine());
                if ((pchosen > pnum)|| (pchosen <=0))
                {
                    Console.WriteLine("Invalid number");
                }

                Process_Manipulate(p[pchosen - 1], pchosen);

            }

        

        }
    }
}
