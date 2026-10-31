using System;

// Ввод данных и вывод результатов
static class UserInterface
{
    // ограничения для размера массива
    public const int maxSize = 20;
    // ограничения для коэффициентов уравнений
    public const int minValue = -100;
    public const int maxValue = 100;

    // ввод целого числа из диапазона
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
                ok = false;
            }
        } while (!ok);
        return n;
    }

    // ввод вещественного числа, по умолчанию - из диапазона значений коэффициентов
    public static double EnterDouble(string text, double min = minValue, double max = maxValue)
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

    // печать меню, пунктов может быть разное количество
    public static void PrintMenu(string title, params string[] items)
    {
        Console.WriteLine(title);
        for (int i = 0; i < items.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }
    }

    // ввод коэффициентов одного уравнения
    public static Uravnenie EnterUravnenie()
    {
        double a = EnterDouble("Введите коэффициент a: ");
        double b = EnterDouble("Введите коэффициент b: ");
        double c = EnterDouble("Введите коэффициент c: ");
        return new Uravnenie(a, b, c);
    }

    // ввод размера уравнений с клавиатуры
    public static Uravnenie[] EnterUravnenia(int size)
    {
        Uravnenie[] items = new Uravnenie[size];
        for (int i = 0; i < size; i++)
        {
            Console.WriteLine($"Уравнение {i + 1}:");
            items[i] = EnterUravnenie();
        }
        return items;
    }

    // печать уравнения
    public static void PrintUravnenie(Uravnenie u)
    {
        if (u == null)
        {
            Console.WriteLine("Уравнение не задано!");
            return;
        }
        Console.WriteLine(u.ToString()); // ToString, иначе сработает неявное приведение к double
    }

    // печать массива уравнений
    public static void PrintArray(UravnenieArray array)
    {
        if (array == null)
        {
            Console.WriteLine("Массив не создан!");
            return;
        }
        Console.WriteLine(array.View());
    }
}
