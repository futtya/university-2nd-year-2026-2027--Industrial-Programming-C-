using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab5csharp
{
    internal class Gift
    {
        string GiftName;
        List<Sweet> sweets;

        public Gift(String name)
        {
            this.GiftName = name;
            this.sweets = new List<Sweet>(); 
        }

        public void AddDirectly(Sweet sweet)
        {
            sweets.Add(sweet);
        }

        public string ShowGiftComposition()
        {
            if (sweets == null || sweets.Count == 0) { return "Нет сладостей"; }
            StringBuilder sb = new StringBuilder();
            sb.Append($"Состав подарка: {GiftName}\n");
            foreach (var i in sweets)
            {
                sb.Append(i.GetBriefInfo());
            }
            return sb.ToString();
        }

        public void AddNewSweet()
        {
            Console.Write("""
                1.Добавить конфету
                2.Добавить пряник
                3.Добавить шоколад
                4.Добавить зефир
                Ввод: 
                """);
            int choice = int.Parse(Console.ReadLine());
            if (choice < 1 || choice > 4)
            {
                return;
            }

            Console.Write("Название: ");
            string name = Console.ReadLine();
            Console.Write("Вес: ");
            double weight = double.Parse(Console.ReadLine());
            Console.Write("Сахар: ");
            int sugar = int.Parse(Console.ReadLine());
            Console.Write("Калории: ");
            int calories = int.Parse(Console.ReadLine());

            Sweet sweet = choice switch
            {
                1 => new Candy(name, weight, sugar, calories),
                2 => new Gingerbread(name, weight, sugar, calories),
                3 => new Chocolate(name, weight, sugar, calories),
                4 => new Marshmallow(name, weight, sugar, calories),
                _ => throw new InvalidOperationException("Неверный вид сладости")
            };
            sweets.Add(sweet);
        }

        public void SortSweets()
        {
            Console.WriteLine("1. Вес\n2. Сахар\n3. Калории");
            Console.Write("\nВыберите параметр сортировки: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Неверный ввод!");
                return;
            }

            List<Sweet> sortedList = null;

            switch (choice)
            {
                case 1:
                    sortedList = sweets.OrderBy(x => x.GetSortingParams().weight).ToList();
                    Console.WriteLine("\n Отсортировано по весу:");
                    break;
                case 2:
                    sortedList = sweets.OrderBy(x => x.GetSortingParams().sugar).ToList();
                    Console.WriteLine("\n Отсортировано по сахару:");
                    break;
                case 3:
                    sortedList = sweets.OrderBy(x => x.GetSortingParams().calories).ToList();
                    Console.WriteLine("\n Отсортировано по калориям:");
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    return;
            }

            foreach (var sweet in sortedList)
            {
                Console.WriteLine(sweet.GetFullDescription());
            }
        }

        public void FindBySugar()
        {
            Console.Write("\nВведите минимальное и максимальное значение сахара\nmin: ");
            int min = int.Parse(Console.ReadLine());
            Console.Write("max: ");
            int max = int.Parse(Console.ReadLine());
            Console.WriteLine($"\nКонфеты, содержание сахара которых находится в промежутке от {min} до {max}:");

            foreach(var i in sweets)
            {
                if(i.GetSortingParams().sugar <= max && i.GetSortingParams().sugar >= min)
                {
                    Console.WriteLine(i.Name);
                }
            }
        }

        public void CalculateTotalWeight()
        {
            double sum = 0;
            foreach(var i in sweets)
            {
                sum += i.GetSortingParams().weight;
            }
            Console.WriteLine($"\nВес подарка: {sum} г.");
        }
    }
}
