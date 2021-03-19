using System;
using System.IO;
using System.Threading;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
namespace Lab2
{
    class Program
{ 
    private static string[] passwords = {
      "1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad", 
      "3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b", 
      "74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f"
    };
    private static string result;
    private static int charactersToTestLength = 0;
    private static long computedKeys = 0;
    static Mutex locker = new Mutex();
    private static char[] charactersToTest =
    {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z'
    };
    static void Main(string[] args)
    {
      for (int i=0; i<3; i++)
      {
        Thread ytfug = new Thread(new ParameterizedThreadStart(StartProgram));
        ytfug.Start(passwords[i]);
      }
      //StartProgram();
    }
    private static void StartProgram(object pasnumber)
    {
      locker.WaitOne();
        var timeStarted = DateTime.Now;
        Console.WriteLine("Время начала подбора - {0}", timeStarted.ToString());
        charactersToTestLength = charactersToTest.Length;
        var estimatedPasswordLength = 5;
        string pswd = pasnumber.ToString();
        startBruteForce(estimatedPasswordLength, pswd);
        Console.WriteLine("Паролей подобрано - {0}", DateTime.Now.ToString());
        Console.WriteLine("Прошло времени: {0} сек", DateTime.Now.Subtract(timeStarted).TotalSeconds);
        Console.WriteLine("Полученный пароль: {0}", result);
        Console.WriteLine("Вычислено паролей: {0}", computedKeys);
      locker.ReleaseMutex();
    }
    private static void startBruteForce(int keyLength, string pswd)
    {
        var keyChars = createCharArray(keyLength, charactersToTest[0]);
        var indexOfLastChar = keyLength - 1;
      Console.WriteLine(pswd);
      createNewKey(0, keyChars, keyLength, indexOfLastChar, pswd);
    }
  
    private static char[] createCharArray(int length, char defaultChar)
    {
        return (from c in new char[length] select defaultChar).ToArray();
    }
    
    private static void createNewKey(int currentCharPosition, char[] keyChars, int keyLength, int indexOfLastChar, string pswd)
    {
        var nextCharPosition = currentCharPosition + 1;
        for (int i = 0; i < charactersToTestLength; i++)
        {
          keyChars[currentCharPosition] = charactersToTest[i];
          
            if (currentCharPosition < indexOfLastChar)
            {
                createNewKey(nextCharPosition, keyChars, keyLength, indexOfLastChar, pswd);
            }
            else
            {
              computedKeys++;
              if (GetHashString(new String(keyChars)) == pswd)
                {
                    result = new String(keyChars);
                    return;
                }
            }
        }
    }
    public static byte[] GetHash(string inputString)
    {
      using (HashAlgorithm algorithm = SHA256.Create())
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }
    public static string GetHashString(string inputString)
    {
      StringBuilder sb = new StringBuilder();
      foreach (byte b in GetHash(inputString))
        sb.Append(b.ToString("X2"));
      return sb.ToString().ToLower();
    }

  }
}