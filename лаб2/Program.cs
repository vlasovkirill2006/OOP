using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("### Задание 1. ###\n");
        task_1();
        Console.WriteLine("\n### Задание 2. ###\n");
        task_2();
        Console.WriteLine("\n### Задание 3. ###");
        task_3();
    }

    static int EnterInt(string text)
    {
        int n;
        bool ok;
        do
        {
            Console.Write(text);
            ok = int.TryParse(Console.ReadLine(), out n);
            if (!ok)
            {
                Console.WriteLine("Ошибка: введите целое число.");
            }
        } while (!ok);
        return n;
    }
    static int EnterPositiveInt(string text)
    {
        int n;
        bool ok;
        do
        {
            Console.Write(text);
            ok = int.TryParse(Console.ReadLine(), out n);
            if (!ok || n <= 0)
            {
                Console.WriteLine("Ошибка: введите положительное целое число.");
            }
        } while (!ok || n <= 0);
        return n;
    }

    static void task_1()
    {
        int n = EnterPositiveInt("Введите n: ");


        int min = int.MaxValue;
        for (int i = 1; i <= n; i++)
        {
            int m = EnterInt($"Введите {i} число: ");

            if (m < min)
            {
                min = m;
            }
        }   
        Console.WriteLine($"Минимальное значение: {min}");

    }

    static void task_2()
    {
        int counter = 0;
        while (true)
        {
            int n = EnterInt("Введите число: ");
            if (n != 0) { 
                if (n % 2 != 0)
                {
                    counter++;
                }
            } else
            {
                Console.WriteLine("Введен 0, программа завершена.");
                break;
            }
        }
        Console.WriteLine($"Количество введенных нечетных чисел: {counter}");
    }

    static void task_3()
    {
        int n = EnterPositiveInt("Введите n: ");
        double x = (double)EnterInt("Введите Х: ") * Math.PI / 180;
        double sum = 0;
        for (int i = 1; i <= n; i++)
        {
            sum += Math.Pow(Math.Sin(x), i);
        }
        Console.WriteLine($"Сумма: {sum}");
    }
}