using System;
using System.Text.RegularExpressions;

class Program
{
    // ограничения для размера массивов
    const int maxSize = 20;
    // ограничения для элементов массивов
    const int minValue = -100;
    const int maxValue = 100;

    // тестовый массив строк
    static string[] testStrings =
    {
        "Метод static void нужен для запуска программы, а цикл for повторяет действия. Оператор if проверяет условие, иначе выполняется else!",
        "В лесу родилась елочка. В лесу она росла! Зимой и летом стройная, зеленая была.",
        "Цикл while работает, пока условие true. Команда break выходит из цикла, а return возвращает значение."
    };

    // ключевые слова C#
    static string[] keywords =
    {
        "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked",
        "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else",
        "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for",
        "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock",
        "long", "namespace", "new", "null", "object", "operator", "out", "override", "params",
        "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed",
        "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this",
        "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort",
        "using", "virtual", "void", "volatile", "while"
    };

    static void Main()
    {
        int[,] matrix = null;
        int[][] jagged = null;
        string text = null;
        int choice;
        do
        {
            PrintMenu("\n### Главное меню ###", "Двумерный массив", "Рваный массив", "Строка", "Выход");
            choice = EnterInt("Выберите пункт: ", 1, 4);
            switch (choice)
            {
                case 1:
                    task_1(ref matrix);
                    break;
                case 2:
                    task_2(ref jagged);
                    break;
                case 3:
                    task_3(ref text);
                    break;
            }
        } while (choice != 4);
    }

    // ввод целого числа, по умолчанию - из диапазона значений элементов массива
    static int EnterInt(string text, int min = minValue, int max = maxValue)
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

    // печать меню, пунктов может быть разное количество
    static void PrintMenu(string title, params string[] items)
    {
        Console.WriteLine(title);
        for (int i = 0; i < items.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }
    }

    // выбор способа формирования: 1 - ДСЧ, 2 - с клавиатуры
    static int ChooseMethod()
    {
        Console.WriteLine("1. Формирование ДСЧ");
        Console.WriteLine("2. Ввод с клавиатуры");
        return EnterInt("Выберите способ: ", 1, 2);
    }

    // ---------- Двумерный массив ----------

    static void task_1(ref int[,] matrix)
    {
        int choice;
        do
        {
            PrintMenu("\n### Двумерный массив ###", "Формирование массива", "Печать массива",
                "Добавление столбца в начало массива", "Назад");
            choice = EnterInt("Выберите пункт: ", 1, 4);
            switch (choice)
            {
                case 1:
                    int rows = EnterInt("Введите количество строк: ", 1, maxSize);
                    int cols = EnterInt("Введите количество столбцов: ", 1, maxSize);
                    matrix = MakeMatrix(rows, cols);
                    break;
                case 2:
                    PrintArray(matrix);
                    break;
                case 3:
                    AddColumnToStart(ref matrix);
                    break;
            }
        } while (choice != 4);
    }

    static int[,] MakeMatrix(int rows, int cols)
    {
        int[,] matrix = new int[rows, cols];
        int method = ChooseMethod();
        Random rnd = new Random();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (method == 1)
                {
                    matrix[i, j] = rnd.Next(minValue, maxValue + 1);
                }
                else
                {
                    matrix[i, j] = EnterInt($"Введите элемент [{i + 1},{j + 1}] (от {minValue} до {maxValue}): ");
                }
            }
        }
        Console.WriteLine("Массив сформирован");
        return matrix;
    }

    static void PrintArray(int[,] matrix)
    {
        if (matrix == null)
        {
            Console.WriteLine("Массив пустой!");
            return;
        }
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    static void AddColumnToStart(ref int[,] matrix)
    {
        if (matrix == null)
        {
            Console.WriteLine("Массив пустой, добавление невозможно!");
            return;
        }
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        if (cols + 1 > maxSize)
        {
            Console.WriteLine($"Добавление невозможно, в массиве не может быть больше {maxSize} столбцов!");
            return;
        }
        int method = ChooseMethod();
        Random rnd = new Random();
        int[,] temp = new int[rows, cols + 1]; // новый массив на один столбец больше
        for (int i = 0; i < rows; i++)
        {
            if (method == 1)
            {
                temp[i, 0] = rnd.Next(minValue, maxValue + 1);
            }
            else
            {
                temp[i, 0] = EnterInt($"Введите элемент нового столбца, строка {i + 1} (от {minValue} до {maxValue}): ");
            }
            for (int j = 0; j < cols; j++)
            {
                temp[i, j + 1] = matrix[i, j]; // старые элементы сдвигаются вправо
            }
        }
        matrix = temp;
        Console.WriteLine("Столбец добавлен");
        PrintArray(matrix);
    }

    // ---------- Рваный массив ----------

    static void task_2(ref int[][] jagged)
    {
        int choice;
        do
        {
            PrintMenu("\n### Рваный массив ###", "Формирование массива", "Печать массива",
                "Удаление строк с номера K1 по номер K2", "Назад");
            choice = EnterInt("Выберите пункт: ", 1, 4);
            switch (choice)
            {
                case 1:
                    int rows = EnterInt("Введите количество строк: ", 1, maxSize);
                    jagged = MakeJagged(rows);
                    break;
                case 2:
                    PrintArray(jagged);
                    break;
                case 3:
                    DeleteRows(ref jagged);
                    break;
            }
        } while (choice != 4);
    }

    static int[][] MakeJagged(int rows)
    {
        int[][] jagged = new int[rows][];
        int method = ChooseMethod();
        Random rnd = new Random();
        for (int i = 0; i < rows; i++)
        {
            int cols = EnterInt($"Введите количество элементов в строке {i + 1}: ", 1, maxSize);
            jagged[i] = new int[cols];
            for (int j = 0; j < cols; j++)
            {
                if (method == 1)
                {
                    jagged[i][j] = rnd.Next(minValue, maxValue + 1);
                }
                else
                {
                    jagged[i][j] = EnterInt($"Введите элемент [{i + 1}][{j + 1}] (от {minValue} до {maxValue}): ");
                }
            }
        }
        Console.WriteLine("Массив сформирован");
        return jagged;
    }

    // рекурсивная сумма элементов строки
    static int SumRow(int[] row, int i)
    {
        if (i == row.Length)
        {
            return 0;
        }
        return row[i] + SumRow(row, i + 1);
    }

    static void PrintArray(int[][] jagged)
    {
        if (jagged == null)
        {
            Console.WriteLine("Массив пустой!");
            return;
        }
        for (int i = 0; i < jagged.Length; i++)
        {
            for (int j = 0; j < jagged[i].Length; j++)
            {
                Console.Write(jagged[i][j] + "\t");
            }
            Console.WriteLine($"(сумма: {SumRow(jagged[i], 0)})");
        }
    }

    static void DeleteRows(ref int[][] jagged)
    {
        if (jagged == null)
        {
            Console.WriteLine("Удаление невозможно, массив пустой!");
            return;
        }
        int size = jagged.Length;
        int k1 = EnterInt("Введите номер строки K1, с которой нужно удалять строки: ", 1, maxSize);
        // проверяем корректность данных для удаления
        if (k1 > size)
        {
            Console.WriteLine("Не правильно задан номер K1, с которого нужно удалять строки!");
            return;
        }
        int k2 = EnterInt("Введите номер строки K2, до которой нужно удалять строки (включительно): ", 1, maxSize);
        if (k2 < k1)
        {
            Console.WriteLine("Не правильно задан номер K2, он не может быть меньше K1!");
            return;
        }
        if (k2 > size)
        {
            Console.WriteLine("Не правильно задан номер K2, строк в массиве меньше!");
            return;
        }
        int count = k2 - k1 + 1; // сколько строк удаляется
        if (count == size) // удаляются все строки
        {
            jagged = null;
            Console.WriteLine("Удалены все строки, массив пустой");
            return;
        }
        int[][] temp = new int[size - count][];
        Array.Copy(jagged, 0, temp, 0, k1 - 1); // строки до K1
        Array.Copy(jagged, k2, temp, k1 - 1, size - k2); // строки после K2
        jagged = temp;
        Console.WriteLine("Строки удалены");
        PrintArray(jagged);
    }

    // ---------- Строка ----------

    static void task_3(ref string text)
    {
        int choice;
        do
        {
            PrintMenu("\n### Строка ###", "Ввод строки с клавиатуры", "Выбор строки из тестового массива",
                "Печать строки", "Поиск ключевых слов C#", "Назад");
            choice = EnterInt("Выберите пункт: ", 1, 5);
            switch (choice)
            {
                case 1:
                    ReadText(ref text);
                    break;
                case 2:
                    ChooseTestString(ref text);
                    break;
                case 3:
                    PrintText(text);
                    break;
                case 4:
                    FindKeywords(text);
                    break;
            }
        } while (choice != 5);
    }

    static void ReadText(ref string text)
    {
        Console.Write("Введите строку: ");
        string line = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(line))
        {
            Console.WriteLine("Строка пустая, ввод не выполнен!");
            return;
        }
        text = line;
    }

    static void ChooseTestString(ref string text)
    {
        for (int i = 0; i < testStrings.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {testStrings[i]}");
        }
        int number = EnterInt("Введите номер строки: ", 1, testStrings.Length);
        text = testStrings[number - 1];
        Console.WriteLine("Строка выбрана");
    }

    static void PrintText(string text)
    {
        if (text == null)
        {
            Console.WriteLine("Строка пустая!");
        }
        else
        {
            Console.WriteLine(text);
        }
    }

    // поиск ключевых слов C# в строке и подсчет, сколько раз встречается каждое
    static void FindKeywords(string text)
    {
        if (text == null)
        {
            Console.WriteLine("Строка пустая!");
            return;
        }
        bool found = false;
        for (int i = 0; i < keywords.Length; i++)
        {
            // \b - граница слова, чтобы не находить слова внутри других слов (info, 1234info)
            int count = Regex.Matches(text, @"\b" + keywords[i] + @"\b").Count;
            if (count > 0)
            {
                Console.WriteLine($"{keywords[i]} – {count}");
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("Ключевые слова C# в строке не найдены");
        }
    }
}