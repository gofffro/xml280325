using System;

namespace xml280325
{
  internal class Program
  {
    static void Main(string[] args)
    {
      string choice;

      while (true)
      {
        Console.Write("Меню: \n" +
            "0) Выход \n" +
            "1) Поиск файлов по ключевому слову\n" +
            "2) Прочитать файл\n" +
            "3) Записать в файл\n" +
            "4) Откат изменений\n" +
            "Ввод: ");
        choice = Console.ReadLine();
        Console.Clear();

        if (choice == "0")
        {
          Console.WriteLine("Программа завершила работу");
          break;
        }
      }
    }
  }
}