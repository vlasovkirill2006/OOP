using System;

public class Program
{

    static void Main()
    {
        Uravnenie u = null;
        Uravnenie first = null;
        Uravnenie second = null;
        UravnenieArray array = null;
        int choice;
        do
        {
            UserInterface.PrintMenu("\n### Главное меню ###", "Часть 1. Классы и объекты",
                "Часть 2. Перегрузка операций", "Часть 3. Класс-коллекция", "Выход");
            choice = UserInterface.EnterInt("Выберите пункт: ", 1, 4);
            switch (choice)
            {
                case 1:
                    task_1(ref u);
                    break;
                case 2:
                    task_2(ref first, ref second);
                    break;
                case 3:
                    task_3(ref array);
                    break;
            }
        } while (choice != 4);
    }

    // Часть 1

    static void task_1(ref Uravnenie u)
    {
        int choice;
        do
        {
            UserInterface.PrintMenu("\n### Часть 1. Классы и объекты ###", "Создание уравнения (ввод с клавиатуры)",
                "Создание уравнения по умолчанию", "Печать уравнения", "Корень уравнения (статическая функция)",
                "Корень уравнения (метод класса)", "Количество созданных объектов", "Назад");
            choice = UserInterface.EnterInt("Выберите пункт: ", 1, 7);
            switch (choice)
            {
                case 1:
                    u = UserInterface.EnterUravnenie();
                    break;
                case 2:
                    u = new Uravnenie();
                    Console.WriteLine("Уравнение создано");
                    break;
                case 3:
                    UserInterface.PrintUravnenie(u);
                    break;
                case 4:
                    if (u == null)
                    {
                        Console.WriteLine("Уравнение не задано!");
                    }
                    else
                    {
                        PrintRoot(u, Uravnenie.GetRoot(u));
                    }
                    break;
                case 5:
                    if (u == null)
                    {
                        Console.WriteLine("Уравнение не задано!");
                    }
                    else
                    {
                        PrintRoot(u, u.FindRoot());
                    }
                    break;
                case 6:
                    Console.WriteLine($"Создано объектов Uravnenie: {Uravnenie.Count}");
                    break;
            }
        } while (choice != 7);
    }

    // печать корня уравнения
    static void PrintRoot(Uravnenie u, double root)
    {
        if (u.HasRoots())
        {
            Console.WriteLine($"Корень уравнения: {root}");
        }
        else
        {
            Console.WriteLine("Корней нет, результат: 0");
        }
    }

    //Часть 2

    static void task_2(ref Uravnenie first, ref Uravnenie second)
    {
        int choice;
        do
        {
            UserInterface.PrintMenu("\n### Часть 2. Перегрузка операций ###", "Ввод первого уравнения",
                "Ввод второго уравнения", "Печать уравнений", "Операция ++ (для первого уравнения)",
                "Операция -- (для первого уравнения)", "Приведение к double (корень первого уравнения)",
                "Приведение к bool (есть ли корни у первого уравнения)", "Сравнение уравнений (== и !=)", "Назад");
            choice = UserInterface.EnterInt("Выберите пункт: ", 1, 9);
            switch (choice)
            {
                case 1:
                    first = UserInterface.EnterUravnenie();
                    break;
                case 2:
                    second = UserInterface.EnterUravnenie();
                    break;
                case 3:
                    Console.Write("Первое уравнение: ");
                    UserInterface.PrintUravnenie(first);
                    Console.Write("Второе уравнение: ");
                    UserInterface.PrintUravnenie(second);
                    break;
                case 4:
                    if (first == null)
                    {
                        Console.WriteLine("Первое уравнение не задано!");
                    }
                    else
                    {
                        first++;
                        Console.WriteLine($"Результат ++: {first}");
                    }
                    break;
                case 5:
                    if (first == null)
                    {
                        Console.WriteLine("Первое уравнение не задано!");
                    }
                    else
                    {
                        first--;
                        Console.WriteLine($"Результат --: {first}");
                    }
                    break;
                case 6:
                    if (first == null)
                    {
                        Console.WriteLine("Первое уравнение не задано!");
                    }
                    else
                    {
                        double root = first; // неявное приведение
                        Console.WriteLine($"Корень (double): {root}");
                    }
                    break;
                case 7:
                    if (first == null)
                    {
                        Console.WriteLine("Первое уравнение не задано!");
                    }
                    else
                    {
                        bool exist = (bool)first; // явное приведение
                        Console.WriteLine($"Корни существуют: {exist}");
                    }
                    break;
                case 8:
                    if (first == null || second == null)
                    {
                        Console.WriteLine("Нужно задать оба уравнения!");
                    }
                    else
                    {
                        Console.WriteLine($"first == second: {first == second}");
                        Console.WriteLine($"first != second: {first != second}");
                    }
                    break;
            }
        } while (choice != 9);
    }

    // Часть 3

    static void task_3(ref UravnenieArray array)
    {
        int choice;
        do
        {
            UserInterface.PrintMenu("\n### Часть 3. Класс-коллекция ###", "Создание пустого массива",
                "Создание массива (ДСЧ)", "Создание массива (ввод с клавиатуры)", "Печать массива",
                "Элемент массива по индексу", "Уравнение с самым большим по модулю корнем",
                "Количество созданных объектов", "Назад");
            choice = UserInterface.EnterInt("Выберите пункт: ", 1, 8);
            switch (choice)
            {
                case 1:
                    array = new UravnenieArray();
                    Console.WriteLine("Массив создан");
                    break;
                case 2:
                    int size = UserInterface.EnterInt("Введите количество элементов: ", 1, UserInterface.maxSize);
                    array = new UravnenieArray(size);
                    Console.WriteLine("Массив сформирован");
                    break;
                case 3:
                    int n = UserInterface.EnterInt("Введите количество элементов: ", 1, UserInterface.maxSize);
                    array = new UravnenieArray(UserInterface.EnterUravnenia(n));
                    Console.WriteLine("Массив сформирован");
                    break;
                case 4:
                    UserInterface.PrintArray(array);
                    break;
                case 5:
                    ShowElement(array);
                    break;
                case 6:
                    PrintMaxRootEquation(array);
                    break;
                case 7:
                    Console.WriteLine($"Создано объектов Uravnenie: {Uravnenie.Count}");
                    Console.WriteLine($"Создано объектов UravnenieArray: {UravnenieArray.Count}");
                    break;
            }
        } while (choice != 8);
    }

    // доступ к элементу через индексатор, при ошибке индекса выводится сообщение
    static void ShowElement(UravnenieArray array)
    {
        if (array == null)
        {
            Console.WriteLine("Массив не создан!");
            return;
        }
        int index = UserInterface.EnterInt("Введите индекс элемента (нумерация с 0): ", -1000, 1000);
        try
        {
            Console.WriteLine(array[index].ToString()); // ToString, чтобы не было приведения к double
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Ошибка: " + e.Message);
        }
    }

    // поиск уравнения с самым большим по модулю корнем (уравнения без корней пропускаются)
    public static Uravnenie FindMaxRootEquation(UravnenieArray array)
    {
        Uravnenie result = null;
        double max = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null && (bool)array[i])
            {
                double root = array[i].MaxAbsRoot();
                if (result == null || root > max)
                {
                    result = array[i];
                    max = root;
                }
            }
        }
        return result;
    }

    static void PrintMaxRootEquation(UravnenieArray array)
    {
        if (array == null)
        {
            Console.WriteLine("Массив не создан!");
            return;
        }
        Uravnenie result = FindMaxRootEquation(array);
        if (result == null)
        {
            Console.WriteLine("В массиве нет уравнений с корнями");
        }
        else
        {
            Console.WriteLine($"Уравнение с самым большим по модулю корнем: {result}");
            Console.WriteLine($"Наибольший по модулю корень: {result.MaxAbsRoot()}");
        }
    }
}
