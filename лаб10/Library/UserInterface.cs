using System;

// Ввод данных с клавиатуры и вывод меню
public static class UserInterface
{
    public static Random rnd = new Random();

    public static int EnterInt(string text, int min, int max)
    {
        int n;
        bool ok;
        do
        {
            Console.Write(text);
            ok = int.TryParse(Console.ReadLine(), out n);
            if (!ok || n < min || n > max)
            {
                Console.WriteLine($"Ошибка: введите целое число от {min} до {max}.");
                ok = false; // число могло быть разобрано, но выйти за диапазон - тогда тоже повторяем ввод
            }
        } while (!ok);
        return n;
    }

    public static double EnterDouble(string text, double min, double max)
    {
        double x;
        bool ok;
        do
        {
            Console.Write(text);
            ok = double.TryParse(Console.ReadLine(), out x);
            if (!ok || double.IsNaN(x) || x < min || x > max)
            {
                Console.WriteLine($"Ошибка: введите число от {min} до {max}.");
                ok = false;
            }
        } while (!ok);
        return x;
    }

    // ввод непустой строки
    // string.IsNullOrWhiteSpace возвращает true для null, пустой строки и строки из одних пробелов.
    public static string EnterString(string text)
    {
        string s;
        do
        {
            Console.Write(text);
            s = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(s))
            {
                Console.WriteLine("Ошибка: строка не должна быть пустой.");
            }
        } while (string.IsNullOrWhiteSpace(s));
        return s.Trim(); // Trim убирает пробелы по краям строки
    }

    public static void PrintMenu(string title, params string[] items)
    {
        Console.WriteLine(title);
        for (int i = 0; i < items.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }
    }
}
