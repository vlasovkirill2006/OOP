using laba11_OOP;
using System;
using System.Collections.Generic;
using System.Diagnostics;    // здесь находится Stopwatch (секундомер)

namespace laba11_OOP
{
    // ЗАДАНИЕ 3. Сравнение скорости поиска в четырёх коллекциях (вариант 6):
    //   Коллекция_1: List<T>
    //   Коллекция_2: SortedDictionary<K,T>
    public class TestCollections
    {
        private List<Student> col1 = new List<Student>();
        private List<string> col2 = new List<string>();
        private SortedDictionary<Person, Student> col3 = new SortedDictionary<Person, Student>();
        private SortedDictionary<string, Student> col4 = new SortedDictionary<string, Student>();

        // Конструктор создаёт коллекции с заданным числом элементов
        public TestCollections(int count)
        {
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                // Имя уникальное (Студент0, Студент1, ...), потому что
                // в SortedDictionary нельзя добавить два одинаковых ключа
                string name = "Студент" + i;
                int age = rnd.Next(17, 30);
                int course = rnd.Next(1, 6);
                double rating = Math.Round(1 + rnd.NextDouble() * 4, 1);   // от 1,0 до 5,0

                Student s = new Student(name, age, course, rating);
                AddStudent(s);   // один и тот же студент попадает во все 4 коллекции
            }
        }

        public int Count
        {
            get { return col1.Count; }
        }

        // Добавление элемента сразу во все четыре коллекции
        public void AddStudent(Student s)
        {
            col1.Add(s);
            col2.Add(s.ToString());           // в строковые коллекции кладём ToString()
            col3.Add(s.BasePerson, s);        // ключ - объект базового класса
            col4.Add(s.ToString(), s);        // ключ - строка
        }

        // Удаление элемента из всех четырёх коллекций
        public bool RemoveStudent(Student s)
        {
            bool removed = col1.Remove(s);
            col2.Remove(s.ToString());
            col3.Remove(s.BasePerson);
            col4.Remove(s.ToString());
            return removed;
        }

        // Количество элементов в каждой из коллекций
        public int CountList { get { return col1.Count; } }
        public int CountStringList { get { return col2.Count; } }
        public int CountPersonDict { get { return col3.Count; } }
        public int CountStringDict { get { return col4.Count; } }

        // Элемент первой коллекции по номеру
        public Student GetStudent(int index)
        {
            return col1[index];
        }

        // Пять видов поиска. Искать нужно СОДЕРЖИМОЕ, поэтому в них передаётся
        // студент, а строки и ключи строятся из него заново.
        public bool ContainsInList(Student s) { return col1.Contains(s); }
        public bool ContainsInStringList(Student s) { return col2.Contains(s.ToString()); }
        public bool ContainsKeyPerson(Student s) { return col3.ContainsKey(s.BasePerson); }
        public bool ContainsKeyString(Student s) { return col4.ContainsKey(s.ToString()); }
        public bool ContainsValueStudent(Student s) { return col3.ContainsValue(s); }

        public void ShowCounts()
        {
            Console.WriteLine("List<Student>                     : " + col1.Count);
            Console.WriteLine("List<string>                      : " + col2.Count);
            Console.WriteLine("SortedDictionary<Person,Student>  : " + col3.Count);
            Console.WriteLine("SortedDictionary<string,Student>  : " + col4.Count);
        }

        // Создаёт НОВЫЙ объект с тем же содержимым (а не ссылку на существующий!)
        public Student Copy(Student s)
        {
            return new Student(s.Name, s.Age, s.Course, s.Rating);
        }

        // Измеряет время одного поиска. search - это "кусочек кода", который возвращает true/false.
        private string Measure(Func<bool> search)
        {
            search();    // "прогревочный" запуск (первый запуск всегда дольше из-за JIT-компиляции)

            Stopwatch sw = Stopwatch.StartNew();
            bool found = search();
            sw.Stop();

            // Обязательно показываем, найден ли элемент
            return sw.ElapsedTicks + " тиков (" + (found ? "найден" : "НЕ найден") + ")";
        }

        private void TestElement(string title, Student target)
        {
            Console.WriteLine();
            Console.WriteLine("--- " + title + ": " + target + " ---");

            Console.WriteLine("List<Student>.Contains                    : " +
                              Measure(() => ContainsInList(target)));
            Console.WriteLine("List<string>.Contains                     : " +
                              Measure(() => ContainsInStringList(target)));
            Console.WriteLine("SortedDict<Person,Student>.ContainsKey    : " +
                              Measure(() => ContainsKeyPerson(target)));
            Console.WriteLine("SortedDict<string,Student>.ContainsKey    : " +
                              Measure(() => ContainsKeyString(target)));
            Console.WriteLine("SortedDict<Person,Student>.ContainsValue  : " +
                              Measure(() => ContainsValueStudent(target)));
        }

        // Ищем четыре элемента: первый, центральный, последний и отсутствующий
        public void RunTests()
        {
            Console.WriteLine("Частота таймера Stopwatch: " + Stopwatch.Frequency + " тиков в секунду");

            // Пробный прогон: первые вызовы всегда медленнее (компиляция кода, "холодный" кэш),
            // поэтому сначала делаем замеры "вхолостую" и их результат не выводим
            Student probe = Copy(col1[0]);
            for (int i = 0; i < 3; i++)
            {
                Measure(() => ContainsInList(probe));
                Measure(() => ContainsInStringList(probe));
                Measure(() => ContainsKeyPerson(probe));
                Measure(() => ContainsKeyString(probe));
                Measure(() => ContainsValueStudent(probe));
            }

            Student first = Copy(col1[0]);
            Student middle = Copy(col1[col1.Count / 2]);
            Student last = Copy(col1[col1.Count - 1]);
            Student missing = new Student("Нет такого", 99, 5, 5.0);

            TestElement("Первый элемент", first);
            TestElement("Центральный элемент", middle);
            TestElement("Последний элемент", last);
            TestElement("Элемент, которого нет", missing);
        }
    }
}
