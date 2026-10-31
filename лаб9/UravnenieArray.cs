using System;
using System.Text;

// Класс-коллекция: одномерный массив уравнений
public class UravnenieArray
{
    // границы значений коэффициентов при формировании ДСЧ
    const int minRandom = -10;
    const int maxRandom = 10;

    Uravnenie[] arr; // одномерный массив элементов типа Uravnenie

    // статическая переменная для подсчета созданных массивов
    static int count = 0;

    public static int Count
    {
        get { return count; }
    }

    public int Length
    {
        get { return arr.Length; }
    }

    // без параметров: пустой массив
    public UravnenieArray()
    {
        arr = new Uravnenie[0];
        count++;
    }

    // с параметрами: размер элементов, заполненных случайными значениями
    public UravnenieArray(int size)
    {
        if (size < 0)
        {
            throw new ArgumentException("Размер массива не может быть отрицательным");
        }
        arr = new Uravnenie[size];
        Random rnd = new Random();
        for (int i = 0; i < size; i++)
        {
            arr[i] = new Uravnenie(rnd.Next(minRandom, maxRandom + 1),
                rnd.Next(minRandom, maxRandom + 1), rnd.Next(minRandom, maxRandom + 1));
        }
        count++;
    }

    // с параметрами: элементы заданы пользователем (ввод выполняет класс UserInterface)
    public UravnenieArray(Uravnenie[] items)
    {
        if (items == null)
        {
            throw new ArgumentException("Массив элементов не задан");
        }
        arr = new Uravnenie[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            arr[i] = items[i];
        }
        count++;
    }

    // индексатор с проверкой выхода индекса за пределы массива
    public Uravnenie this[int index]
    {
        get
        {
            if (index < 0 || index >= arr.Length)
            {
                throw new ArgumentException("Индекс выходит за пределы массива");
            }
            return arr[index];
        }
        set
        {
            if (index < 0 || index >= arr.Length)
            {
                throw new ArgumentException("Индекс выходит за пределы массива");
            }
            arr[index] = value;
        }
    }

    // просмотр элементов массива (строка для вывода, печатает класс UserInterface)
    public string View()
    {
        if (arr.Length == 0)
        {
            return "Массив пустой!";
        }
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < arr.Length; i++)
        {
            sb.AppendLine($"{i + 1}. {arr[i]}");
        }
        return sb.ToString().TrimEnd();
    }
}
