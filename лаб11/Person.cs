using System;

namespace laba11_OOP
{
    // Базовый класс иерархии (из работы №10)
    public class Person : IComparable, ICloneable
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person()
        {
            Name = "Без имени";
            Age = 0;
        }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // virtual - значит, что наследник может переопределить метод
        public virtual void Show()
        {
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            return Name + ", " + Age;
        }

        // IComparable: нужен для сортировки (Sort, BinarySearch) и для SortedDictionary.
        // Сравниваем людей по имени.
        public int CompareTo(object obj)
        {
            Person other = obj as Person;
            if (other == null)
                throw new ArgumentException("Можно сравнивать только с Person");
            return string.CompareOrdinal(Name, other.Name);
        }

        // ICloneable: создаём независимую копию объекта
        public virtual object Clone()
        {
            return new Person(Name, Age);
        }

        // Equals и GetHashCode нужны, чтобы коллекции сравнивали СОДЕРЖИМОЕ объектов,
        // а не ссылки (это важно для Contains, ContainsKey, ContainsValue).
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Person other = (Person)obj;
            return Name == other.Name && Age == other.Age;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Age;
        }
    }
}
