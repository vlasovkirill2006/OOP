using laba11_OOP;
using System;
using System.Globalization;

namespace laba11_OOP
{
    // Вспомогательный класс: безопасный ввод с клавиатуры.
    // Если пользователь ввёл не число, программа просто спросит ещё раз.
    public static class Input
    {
        public static string ReadString(string text)
        {
            Console.Write(text);
            string s = Console.ReadLine();
            if (s == null) s = "";
            return s.Trim();
        }

        public static int ReadInt(string text)
        {
            while (true)
            {
                int value;
                if (int.TryParse(ReadString(text), out value))
                    return value;
                Console.WriteLine("Нужно ввести целое число. Попробуйте ещё раз.");
            }
        }

        public static double ReadDouble(string text)
        {
            while (true)
            {
                // разрешаем вводить и 4.5, и 4,5
                string s = ReadString(text).Replace(',', '.');
                double value;
                if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                    return value;
                Console.WriteLine("Нужно ввести число. Попробуйте ещё раз.");
            }
        }

        public static Person ReadPerson()
        {
            string name = ReadString("Фамилия: ");
            int age = ReadInt("Возраст: ");
            return new Person(name, age);
        }

        public static Student ReadStudent()
        {
            string name = ReadString("Фамилия: ");
            int age = ReadInt("Возраст: ");
            int course = ReadInt("Курс: ");
            double rating = ReadDouble("Рейтинг: ");
            return new Student(name, age, course, rating);
        }
    }
}
