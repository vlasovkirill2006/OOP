using System;

public static class Zaprosy
{
    // Запрос 1. Названия всех городов заданной области.
    public static string[] CityNames(Mesto[] places, string oblast)
    {
        int count = 0;
        for (int i = 0; i < places.Length; i++)
        {
            if (places[i] is Gorod && ((Gorod)places[i]).OblastName == oblast)
            {
                count++;
            }
        }
        string[] names = new string[count];
        int k = 0;
        for (int i = 0; i < places.Length; i++)
        {
            if (places[i] is Gorod && ((Gorod)places[i]).OblastName == oblast)
            {
                names[k] = places[i].Name;
                k++;
            }
        }
        return names;
    }

    // Запрос 2. Суммарное количество жителей всех городов заданной области.
    public static int TotalPopulation(Mesto[] places, string oblast)
    {
        int sum = 0;
        for (int i = 0; i < places.Length; i++)
        {
            Gorod g = places[i] as Gorod;
            if (g != null && g.OblastName == oblast)
            {
                sum += g.Population;
            }
        }
        return sum;
    }

    // Запрос 3. Количество мегаполисов в заданной области.
    public static int CountMegapolis(Mesto[] places, string oblast)
    {
        int count = 0;
        for (int i = 0; i < places.Length; i++)
        {
            Megapolis m = places[i] as Megapolis;
            if (m != null && m.OblastName == oblast)
            {
                count++;
            }
        }
        return count;
    }

    // Запрос 4. Количество адресов в заданном городе (у адреса в поле Name хранится название города).
    public static int CountAdresa(Mesto[] places, string city)
    {
        int count = 0;
        for (int i = 0; i < places.Length; i++)
        {
            if (places[i] is Adres && places[i].Name == city)
            {
                count++;
            }
        }
        return count;
    }

    // Линейный (последовательный) поиск места по названию.
    public static int FindIndex(Mesto[] places, string name)
    {
        for (int i = 0; i < places.Length; i++)
        {
            if (places[i].Name == name)
            {
                return i;
            }
        }
        return -1;
    }

    // Бинарный (двоичный) поиск места по названию.
    public static int BinarySearch(Mesto[] places, string name)
    {
        int left = 0;                  // левая граница области поиска
        int right = places.Length - 1; // правая граница
        while (left <= right)
        {
            int middle = (left + right) / 2;
            int result = string.Compare(places[middle].Name, name, StringComparison.Ordinal);
            if (result == 0)
            {
                return middle;
            }
            if (result < 0)
            {
                left = middle + 1;  // элемент в середине меньше искомого - ищем справа
            }
            else
            {
                right = middle - 1; // элемент в середине больше искомого - ищем слева
            }
        }
        return -1;
    }
}
