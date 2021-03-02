using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab1_4
{
    class Worker
    {   
        public string Name { get; set; }
        public int ID { get; set; }

    }

    class Program
    {
        static async Task Main(string[] args)
        {
            using (FileStream fs = new FileStream("person.json", FileMode.OpenOrCreate))
            {
                Worker tom = new Worker() { Name = "R6853", ID = 5060396 };
                await JsonSerializer.SerializeAsync<Worker>(fs, tom);
                Console.WriteLine("Данные сохранены в файл");
            }
            
            using (FileStream fs = new FileStream("person.json", FileMode.Open))
            {
                Worker restoredPerson = await JsonSerializer.DeserializeAsync<Worker>(fs);
                Console.WriteLine($"Name: {restoredPerson.Name}  Age: {restoredPerson.ID}");
            }

        }
    }
}