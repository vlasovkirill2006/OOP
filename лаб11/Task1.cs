using System;
using System.Collections;   // здесь живёт ArrayList

namespace laba11_OOP
{
    // ЗАДАНИЕ 1. Обычная (необобщённая) коллекция ArrayList
    public class Task1
    {
        private ArrayList list = new ArrayList();

        public Task1()
        {
            // Начальные данные: объекты иерархии Person -> Student
            list.Add(new Student("Иванов", 18, 1, 4));
            list.Add(new Student("Петров", 19, 2, 5));
            list.Add(new Student("Сидоров", 21, 5, 4.5));
            list.Add(new Person("Семенов", 23));
        }

        // ---------- Методы работы с коллекцией (без ввода/вывода, их легко тестировать) ----------

        public int Count
        {
            get { return list.Count; }
        }

        public void Add(Person p)
        {
            list.Add(p);
        }

        public Person Get(int index)
        {
            return (Person)list[index];
        }

        // Удаление по номеру. Возвращает false, если такого номера нет.
        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= list.Count)
                return false;
            list.RemoveAt(index);
            return true;
        }

        // Запрос 1: количество студентов
        public int CountStudents()
        {
            int count = 0;
            foreach (object o in list)
            {
                if (o is Student)    // "is" проверяет, что объект - студент
                    count++;
            }
            return count;
        }

        // Запрос 2: студенты с рейтингом не ниже minRating
        public ArrayList GetGoodStudents(double minRating)
        {
            ArrayList result = new ArrayList();
            foreach (object o in list)
            {
                Student s = o as Student;     // если o не студент, получится null
                if (s != null && s.Rating >= minRating)
                    result.Add(s);
            }
            return result;
        }

        // Запрос 3: все (и люди, и студенты), кто старше заданного возраста
        public ArrayList GetOlderThan(int age)
        {
            ArrayList result = new ArrayList();
            foreach (Person p in list)
            {
                if (p.Age > age)
                    result.Add(p);
            }
            return result;
        }

        // Глубокая копия: каждый элемент клонируем отдельно
        public ArrayList DeepClone()
        {
            ArrayList copy = new ArrayList();
            foreach (Person p in list)
                copy.Add(p.Clone());
            return copy;
        }

        // Поверхностная копия: копируется сам список, элементы остаются теми же объектами
        public ArrayList ShallowClone()
        {
            return (ArrayList)list.Clone();
        }

        // Сортировка по фамилии (использует CompareTo из класса Person)
        public void Sort()
        {
            list.Sort();
        }

        // Двоичный поиск по фамилии. Возвращает номер элемента или -1, если не найден.
        // Двоичный поиск работает только в отсортированной коллекции, поэтому сначала сортируем.
        public int Find(string name)
        {
            list.Sort();
            int index = list.BinarySearch(new Person(name, 0));
            if (index < 0)
                return -1;
            return index;
        }

        // ---------- Меню ----------

        public void Run()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== ЗАДАНИЕ 1. ArrayList ===");
                Console.WriteLine("1  - Показать коллекцию (foreach)");
                Console.WriteLine("2  - Добавить человека (Person)");
                Console.WriteLine("3  - Добавить студента (Student)");
                Console.WriteLine("4  - Удалить элемент по номеру");
                Console.WriteLine("5  - Запрос 1: сколько в коллекции студентов");
                Console.WriteLine("6  - Запрос 2: студенты с рейтингом не ниже 4.5");
                Console.WriteLine("7  - Запрос 3: люди старше заданного возраста");
                Console.WriteLine("8  - Клонирование коллекции");
                Console.WriteLine("9  - Сортировка по фамилии");
                Console.WriteLine("10 - Поиск по фамилии (двоичный поиск)");
                Console.WriteLine("0  - Назад");
                int choice = Input.ReadInt("Ваш выбор: ");

                if (choice == 0) return;
                else if (choice == 1) ShowAll();
                else if (choice == 2) Add(Input.ReadPerson());
                else if (choice == 3) Add(Input.ReadStudent());
                else if (choice == 4) RemoveByIndex();
                else if (choice == 5) QueryCountStudents();
                else if (choice == 6) QueryGoodStudents();
                else if (choice == 7) QueryOlderThan();
                else if (choice == 8) CloneDemo();
                else if (choice == 9) SortList();
                else if (choice == 10) Search();
                else Console.WriteLine("Нет такого пункта меню.");
            }
        }

        // Перебор элементов с помощью foreach
        private void ShowAll()
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Коллекция пуста.");
                return;
            }
            int number = 0;
            foreach (Person p in list)
            {
                Console.Write(number + ": ");
                p.Show();     // для Student вызовется версия из Student (полиморфизм)
                number++;
            }
        }

        private void RemoveByIndex()
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Коллекция пуста, удалять нечего.");
                return;
            }
            int index = Input.ReadInt("Номер удаляемого элемента (с нуля): ");
            if (RemoveAt(index))
                Console.WriteLine("Элемент удалён.");
            else
                Console.WriteLine("Нет элемента с таким номером.");
        }

        // Запрос 1
        private void QueryCountStudents()
        {
            Console.WriteLine("Студентов в коллекции: " + CountStudents());
        }

        // Запрос 2
        private void QueryGoodStudents()
        {
            ArrayList good = GetGoodStudents(4.5);
            if (good.Count == 0)
                Console.WriteLine("Таких студентов нет.");
            foreach (Student s in good)
                s.Show();
        }

        // Запрос 3
        private void QueryOlderThan()
        {
            int age = Input.ReadInt("Возраст: ");
            ArrayList older = GetOlderThan(age);
            foreach (Person p in older)
                p.Show();
            Console.WriteLine("Найдено: " + older.Count);
        }

        // Клонирование
        private void CloneDemo()
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Коллекция пуста.");
                return;
            }

            // 1) Поверхностное копирование: копируется сам список, но элементы остаются теми же объектами
            ArrayList shallow = ShallowClone();

            // 2) Глубокое копирование: каждый элемент клонируется отдельно
            ArrayList deep = DeepClone();

            Console.WriteLine("Клонирование выполнено. Элементов в копиях: " +
                              shallow.Count + " и " + deep.Count);

            // Покажем разницу: временно изменим фамилию первого элемента в каждой копии
            Person original = (Person)list[0];
            string oldName = original.Name;

            ((Person)shallow[0]).Name = "ИЗМЕНЕНО";
            Console.WriteLine("Изменили первый элемент в поверхностной копии. В оригинале: " + original.Name);
            original.Name = oldName;   // возвращаем как было

            ((Person)deep[0]).Name = "ИЗМЕНЕНО";
            Console.WriteLine("Изменили первый элемент в глубокой копии. В оригинале: " + original.Name);
        }

        private void SortList()
        {
            Sort();
            Console.WriteLine("Коллекция отсортирована.");
            ShowAll();
        }

        private void Search()
        {
            string name = Input.ReadString("Фамилия для поиска: ");
            int index = Find(name);

            if (index >= 0)
            {
                Console.Write("Найден, номер " + index + ": ");
                Get(index).Show();
            }
            else
            {
                Console.WriteLine("Такого элемента нет.");
            }
        }
    }
}
