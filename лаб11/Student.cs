using System;

namespace laba11_OOP
{
    // Производный класс: студент - это человек, у которого есть курс и рейтинг
    public class Student : Person
    {
        public int Course { get; set; }
        public double Rating { get; set; }

        public Student() : base()
        {
            Course = 1;
            Rating = 0;
        }

        public Student(string name, int age, int course, double rating) : base(name, age)
        {
            Course = course;
            Rating = rating;
        }

        // Свойство из задания 3: возвращает объект БАЗОВОГО класса
        public Person BasePerson
        {
            get { return new Person(Name, Age); }
        }

        public override void Show()
        {
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            return base.ToString() + ", курс=" + Course + ", рейтинг=" + Rating;
        }

        public override object Clone()
        {
            return new Student(Name, Age, Course, Rating);
        }

        public override bool Equals(object obj)
        {
            // base.Equals уже проверяет, что тип совпадает, и сравнивает имя и возраст
            if (!base.Equals(obj))
                return false;
            Student other = (Student)obj;
            return Course == other.Course && Rating == other.Rating;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Course;
        }
    }
}
