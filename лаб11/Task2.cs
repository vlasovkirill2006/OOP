using laba11_OOP;
using System;
using System.Collections.Generic;   // здесь живут обобщённые коллекции

namespace laba11_OOP
{
    // ЗАДАНИЕ 2. Обобщённая коллекция Dictionary<K,T>
    // Ключ - номер записи (int), значение - Person (в нём можно хранить и Student)
    public class Task2
    {
        private Dictionary<int, Person> dict = new Dictionary<int, Person>();
        private int nextKey = 1;     // следующий свободный ключ

        public Task2()
        {
            Add(new Student("Иванов", 18, 1, 4));
            Add(new Student("Петров", 19, 2, 5));
            Add(new Student("Сидоров", 21, 5, 4.5));
            Add(new Person("Семенов", 23));
        }

        // ---------- Методы работы с коллекцией (без ввода/вывода, их легко тестировать) ----------

        public int Count
        {
            get { return dict.Count; }
        }

        // Добавляет элемент и возвращает ключ, под которым он сохранён
        public int Add(Person p)
        {
            int key = nextKey;
            dict.Add(key, p);
            nextKey++;
            return key;
        }

        // Удаление по ключу. Возвращает true, если элемент был найден и удалён
        public bool Remove(int key)
        {
            return dict.Remove(key);
        }

        // Безопасный поиск по ключу
        public bool TryGet(int key, out Person p)
        {
            return dict.TryGetValue(key, out p);
        }

        // Запрос 1: количество студентов
        public int CountStudents()
        {
            int count = 0;
            foreach (Person p in dict.Values)    // Values - все значения словаря
            {
                if (p is Student)
                    count++;
            }
            return count;
        }

        // Запрос 2: студенты с рейтингом не ниже minRating
        public List<Student> GetGoodStudents(double minRating)
        {
            List<Student> result = new List<Student>();
            foreach (Person p in dict.Values)
            {
                Student s = p as Student;
                if (s != null && s.Rating >= minRating)
                    result.Add(s);
            }
            return result;
        }

        // Запрос 3: все, кто старше заданного возраста
        public List<Person> GetOlderThan(int age)
        {
            List<Person> result = new List<Person>();
            foreach (Person p in dict.Values)
            {
                if (p.Age > age)
                    result.Add(p);
            }
            return result;
        }

        // Ключи всех элементов с заданной фамилией
        public List<int> FindKeysByName(string name)
        {
            List<int> keys = new List<int>();
            foreach (KeyValuePair<int, Person> pair in dict)
            {
                if (pair.Value.Name == name)
                    keys.Add(pair.Key);
            }
            return keys;
        }

        // Поверхностная копия: новый словарь, но значения - те же самые объекты
        public Dictionary<int, Person> ShallowClone()
        {
            return new Dictionary<int, Person>(dict);
        }

        // Глубокая копия: клонируем каждое значение
        public Dictionary<int, Person> DeepClone()
        {
            Dictionary<int, Person> copy = new Dictionary<int, Person>();
            foreach (KeyValuePair<int, Person> pair in dict)
                copy.Add(pair.Key, (Person)pair.Value.Clone());
            return copy;
        }

        // Dictionary нельзя отсортировать "на месте" (он не хранит порядок),
        // поэтому копируем значения в List<Person> и сортируем его.
        public List<Person> GetSorted()
        {
            List<Person> sorted = new List<Person>(dict.Values);
            sorted.Sort();      // использует CompareTo из Person
            return sorted;
        }

        // ---------- Меню ----------

        public void Run()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== ЗАДАНИЕ 2. Dictionary<int, Person> ===");
                Console.WriteLine("1  - Показать коллекцию (foreach)");
                Console.WriteLine("2  - Добавить человека (Person)");
                Console.WriteLine("3  - Добавить студента (Student)");
                Console.WriteLine("4  - Удалить элемент по ключу");
                Console.WriteLine("5  - Запрос 1: сколько в коллекции студентов");
                Console.WriteLine("6  - Запрос 2: студенты с рейтингом не ниже 4.5");
                Console.WriteLine("7  - Запрос 3: люди старше заданного возраста");
                Console.WriteLine("8  - Клонирование коллекции");
                Console.WriteLine("9  - Сортировка по фамилии");
                Console.WriteLine("10 - Поиск по ключу");
                Console.WriteLine("11 - Поиск по фамилии");
                Console.WriteLine("0  - Назад");
                int choice = Input.ReadInt("Ваш выбор: ");

                if (choice == 0) return;
                else if (choice == 1) ShowAll();
                else if (choice == 2) Add(Input.ReadPerson());
                else if (choice == 3) Add(Input.ReadStudent());
                else if (choice == 4) RemoveByKey();
                else if (choice == 5) QueryCountStudents();
                else if (choice == 6) QueryGoodStudents();
                else if (choice == 7) QueryOlderThan();
                else if (choice == 8) CloneDemo();
                else if (choice == 9) SortDemo();
                else if (choice == 10) SearchByKey();
                else if (choice == 11) SearchByName();
                else Console.WriteLine("Нет такого пункта меню.");
            }
        }

        private void ShowAll()
        {
            if (dict.Count == 0)
            {
                Console.WriteLine("Коллекция пуста.");
                return;
            }
            // Каждый элемент словаря - пара "ключ-значение" (KeyValuePair)
            foreach (KeyValuePair<int, Person> pair in dict)
            {
                Console.Write("Ключ " + pair.Key + ": ");
                pair.Value.Show();
            }
        }

        private void RemoveByKey()
        {
            int key = Input.ReadInt("Ключ удаляемого элемента: ");
            if (Remove(key))
                Console.WriteLine("Элемент удалён.");
            else
                Console.WriteLine("Элемента с таким ключом нет.");
        }

        // Запрос 1
        private void QueryCountStudents()
        {
            Console.WriteLine("Студентов в коллекции: " + CountStudents());
        }

        // Запрос 2
        private void QueryGoodStudents()
        {
            List<Student> good = GetGoodStudents(4.5);
            if (good.Count == 0)
                Console.WriteLine("Таких студентов нет.");
            foreach (Student s in good)
                s.Show();
        }

        // Запрос 3
        private void QueryOlderThan()
        {
            int age = Input.ReadInt("Возраст: ");
            List<Person> older = GetOlderThan(age);
            foreach (Person p in older)
                p.Show();
            Console.WriteLine("Найдено: " + older.Count);
        }

        private void CloneDemo()
        {
            if (dict.Count == 0)
            {
                Console.WriteLine("Коллекция пуста.");
                return;
            }

            Dictionary<int, Person> shallow = ShallowClone();
            Dictionary<int, Person> deep = DeepClone();

            Console.WriteLine("Клонирование выполнено. Элементов в копиях: " +
                              shallow.Count + " и " + deep.Count);

            // Покажем разницу на первом элементе
            int firstKey = 0;
            foreach (int k in dict.Keys) { firstKey = k; break; }

            Person original = dict[firstKey];
            string oldName = original.Name;

            shallow[firstKey].Name = "ИЗМЕНЕНО";
            Console.WriteLine("Изменили элемент в поверхностной копии. В оригинале: " + original.Name);
            original.Name = oldName;

            deep[firstKey].Name = "ИЗМЕНЕНО";
            Console.WriteLine("Изменили элемент в глубокой копии. В оригинале: " + original.Name);
        }

        private void SortDemo()
        {
            Console.WriteLine("Значения словаря, отсортированные по фамилии:");
            foreach (Person p in GetSorted())
                p.Show();
        }

        private void SearchByKey()
        {
            int key = Input.ReadInt("Ключ: ");
            Person p;
            if (TryGet(key, out p))
            {
                Console.Write("Найден: ");
                p.Show();
            }
            else
            {
                Console.WriteLine("Элемента с таким ключом нет.");
            }
        }

        private void SearchByName()
        {
            string name = Input.ReadString("Фамилия: ");
            List<int> keys = FindKeysByName(name);
            if (keys.Count == 0)
                Console.WriteLine("Такой фамилии нет.");
            foreach (int key in keys)
            {
                Console.Write("Ключ " + key + ": ");
                dict[key].Show();
            }
        }
    }
}
