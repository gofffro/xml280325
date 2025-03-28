using System;
using System.IO;

namespace xml280325
{
  internal class Program
  {
    static void Main(string[] args)
    {
      string menuChoice;
      string path = null;
      string keyword;


      Caretaker caretaker = new Caretaker();
      Originator originator = new Originator();

      while (true)
      {
        Console.Write("Меню: \n" +
            "0) Выход \n" +
            "1) Поиск файлов по ключевому слову\n" +
            "2) Прочитать файл\n" +
            "3) Записать в файл\n" +
            "4) Откат изменений\n" +
            "Ввод: ");
        menuChoice = Console.ReadLine();
        Console.Clear();

        if (menuChoice == "0")
        {
          Console.WriteLine("Программа завершила работу");
          break;
        }

        switch (menuChoice)
        {
          case "1":
            Console.WriteLine("Введите директорию: ");
            path = Console.ReadLine();
            Console.WriteLine("Введите ключевое слово: ");
            keyword = Console.ReadLine();
            Search search = new Search(keyword, path);
            search.Result();
            break;
        }
      }
    }
  }
}