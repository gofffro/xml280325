using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace xml280325
{
   public class FileSer
  {
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }

    public FileSer() { }

    public FileSer(string name, string surname, int age)
    {
      Name = name;
      Surname = surname;
      Age = age;
    }

    public void Print()
    {
      Console.WriteLine($"Name={Name} Surname={Surname} Age={Age}");
    }
  }
}
