using System;
using System.Text;
using System.Threading.Tasks;

namespace laba11_OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            // чтобы русские буквы нормально отображались в консоли
            Console.OutputEncoding = new UTF8Encoding(false);   // false = без служебного символа BOM
            Console.InputEncoding = new UTF8Encoding(false);

            Task1 task1 = new Task1();
            Task2 task2 = new Task2();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("===== Практическая работа №11. Коллекции (вариант 6) =====");
                Console.WriteLine("1 - Задание 1 (ArrayList)");
                Console.WriteLine("2 - Задание 2 (Dictionary<K,T>)");
                Console.WriteLine("3 - Задание 3 (List<T> и SortedDictionary<K,T>, замер времени поиска)");
                Console.WriteLine("0 - Выход");
                int choice = Input.ReadInt("Ваш выбор: ");

                if (choice == 0)
                    return;
                else if (choice == 1)
                    task1.Run();
                else if (choice == 2)
                    task2.Run();
                else if (choice == 3)
                    RunTask3();
                else
                    Console.WriteLine("Нет такого пункта меню.");
            }
        }

        static void RunTask3()
        {
            TestCollections tc = new TestCollections(1000);
            Console.WriteLine("Созданы коллекции. Количество элементов:");
            tc.ShowCounts();

            // Проверим добавление и удаление
            Student extra = new Student("Новый", 20, 2, 4.2);
            tc.AddStudent(extra);
            Console.WriteLine("\nПосле добавления студента:");
            tc.ShowCounts();
            tc.RemoveStudent(extra);
            Console.WriteLine("\nПосле удаления студента:");
            tc.ShowCounts();

            Console.WriteLine("\nИзмерение времени поиска (в тиках Stopwatch):");
            tc.RunTests();
        }
    }
}
