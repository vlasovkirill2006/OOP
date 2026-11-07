using System;

class Program
{
    // максимальный размер массива
    const int maxSize = 20;

    static void Main()
    {
        Mesto[] places = null; // массив мест (в нем лежат объекты разных классов иерархии)
        IInit[] items = null;  // массив элементов типа интерфейса IInit 
        int choice;
        do
        {
            UserInterface.PrintMenu("\n### Главное меню ###", "Часть 1. Иерархия классов",
                "Часть 2. Динамическая идентификация типов", "Часть 3. Интерфейсы", "Выход");
            choice = UserInterface.EnterInt("Выберите пункт: ", 1, 4);
            switch (choice)
            {
                case 1:
                    task_1(ref places);
                    break;
                case 2:
                    task_2(ref places);
                    break;
                case 3:
                    task_3(ref places, ref items);
                    break;
            }
        } while (choice != 4);
    }


    // создание объекта нужного класса иерархии по номеру: 1 - место, 2 - область, 3 - город, 4 - мегаполис, 5 - адрес
    // Тип результата Mesto, хотя возвращаются и объекты наследников: наследник - это тоже место
    // (так можно присвоить объект производного класса переменной базового).
    static Mesto CreateByType(int type)
    {
        switch (type)
        {
            case 1:
                return new Mesto();
            case 2:
                return new Oblast();
            case 3:
                return new Gorod();
            case 4:
                return new Megapolis();
            default:
                return new Adres();
        }
    }

    // формирование массива датчиком случайных чисел
    // Первые 5 элементов - по одному объекту каждого класса (чтобы в массиве были все типы иерархии),
    // остальные - случайного класса.
    static Mesto[] MakePlaces(int size)
    {
        Mesto[] arr = new Mesto[size];
        for (int i = 0; i < size; i++)
        {
            int type;
            if (i < 5)
            {
                type = i + 1;
            }
            else
            {
                type = UserInterface.rnd.Next(1, 6);
            }
            arr[i] = CreateByType(type);
            // RandomInit виртуальный: у каждого объекта выполнится версия его собственного класса
            arr[i].RandomInit();
        }
        return arr;
    }

    // создание массива мест: запрашивается размер (не меньше 5, чтобы были объекты всех классов)
    static void CreatePlaces(ref Mesto[] places)
    {
        int size = UserInterface.EnterInt($"Введите количество элементов (от 5 до {maxSize}): ", 5, maxSize);
        places = MakePlaces(size);
        Console.WriteLine("Массив сформирован");
    }

    // печать массива мест через виртуальный метод Show
    static void PrintPlaces(Mesto[] places)
    {
        if (places == null)
        {
            Console.WriteLine("Массив не создан!");
            return;
        }
        for (int i = 0; i < places.Length; i++)
        {
            Console.Write($"{i + 1}. ");
            places[i].Show();
        }
    }

    // выбор одного значения из списка строк (чтобы не вводить его с клавиатуры)
    static string ChooseFromList(string title, string[] list)
    {
        Console.WriteLine(title);
        for (int i = 0; i < list.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {list[i]}");
        }
        int number = UserInterface.EnterInt("Выберите номер: ", 1, list.Length);
        return list[number - 1];
    }


    static void task_1(ref Mesto[] places)
    {
        int choice;
        do
        {
            UserInterface.PrintMenu("\n### Часть 1. Иерархия классов ###", "Формирование массива (ДСЧ)",
                "Просмотр массива (виртуальный метод Show)", "Просмотр массива (обычный метод ShowNV)",
                "Ввод объекта с клавиатуры (Init)", "Сравнение двух элементов (Equals)", "Назад");
            choice = UserInterface.EnterInt("Выберите пункт: ", 1, 6);
            switch (choice)
            {
                case 1:
                    CreatePlaces(ref places);
                    break;
                case 2:
                    PrintPlaces(places);
                    break;
                case 3:
                    PrintPlacesNV(places);
                    break;
                case 4:
                    InitObject();
                    break;
                case 5:
                    ComparePlaces(places);
                    break;
            }
        } while (choice != 6);
    }

    // Просмотр через обычный (невиртуальный) метод.
    // Переменная places[i] имеет тип Mesto, поэтому у ВСЕХ объектов вызывается версия ShowNV из класса Mesto
    static void PrintPlacesNV(Mesto[] places)
    {
        if (places == null)
        {
            Console.WriteLine("Массив не создан!");
            return;
        }
        for (int i = 0; i < places.Length; i++)
        {
            Console.Write($"{i + 1}. ");
            places[i].ShowNV();
        }
    }

    // ввод одного объекта с клавиатуры: выбираем класс, создаем объект, вызываем Init (виртуальный)
    static void InitObject()
    {
        Console.WriteLine("1. Место\n2. Область\n3. Город\n4. Мегаполис\n5. Адрес");
        int type = UserInterface.EnterInt("Выберите класс объекта: ", 1, 5);
        Mesto obj = CreateByType(type);
        obj.Init();
        Console.Write("Создан объект: ");
        obj.Show();
    }

    // сравнение двух элементов массива методом Equals
    static void ComparePlaces(Mesto[] places)
    {
        if (places == null)
        {
            Console.WriteLine("Массив не создан!");
            return;
        }
        int first = UserInterface.EnterInt("Введите номер первого элемента: ", 1, places.Length);
        int second = UserInterface.EnterInt("Введите номер второго элемента: ", 1, places.Length);
        Console.WriteLine($"Элементы равны: {places[first - 1].Equals(places[second - 1])}");
    }


    static void task_2(ref Mesto[] places)
    {
        int choice;
        do
        {
            UserInterface.PrintMenu("\n### Часть 2. Динамическая идентификация типов ###", "Формирование массива (ДСЧ)",
                "Печать массива", "Названия всех городов заданной области",
                "Суммарное количество жителей всех городов области", "Количество мегаполисов в области",
                "Количество адресов в заданном городе", "Назад");
            choice = UserInterface.EnterInt("Выберите пункт: ", 1, 7);
            switch (choice)
            {
                case 1:
                    CreatePlaces(ref places);
                    break;
                case 2:
                    PrintPlaces(places);
                    break;
                case 3:
                    if (places == null)
                    {
                        Console.WriteLine("Массив не создан!");
                    }
                    else
                    {
                        PrintCityNames(places);
                    }
                    break;
                case 4:
                    if (places == null)
                    {
                        Console.WriteLine("Массив не создан!");
                    }
                    else
                    {
                        string oblast = ChooseFromList("Области:", Data.oblasti);
                        Console.WriteLine($"Суммарное количество жителей: {Zaprosy.TotalPopulation(places, oblast)}");
                    }
                    break;
                case 5:
                    if (places == null)
                    {
                        Console.WriteLine("Массив не создан!");
                    }
                    else
                    {
                        string oblast = ChooseFromList("Области:", Data.oblasti);
                        Console.WriteLine($"Количество мегаполисов: {Zaprosy.CountMegapolis(places, oblast)}");
                    }
                    break;
                case 6:
                    if (places == null)
                    {
                        Console.WriteLine("Массив не создан!");
                    }
                    else
                    {
                        string city = ChooseFromList("Города:", Data.cities);
                        Console.WriteLine($"Количество адресов: {Zaprosy.CountAdresa(places, city)}");
                    }
                    break;
            }
        } while (choice != 7);
    }

    // вывод названий городов области
    static void PrintCityNames(Mesto[] places)
    {
        string oblast = ChooseFromList("Области:", Data.oblasti);
        string[] names = Zaprosy.CityNames(places, oblast);
        if (names.Length == 0)
        {
            Console.WriteLine("В этой области нет городов");
            return;
        }
        Console.WriteLine("Города: " + string.Join(", ", names));
    }


    static void task_3(ref Mesto[] places, ref IInit[] items)
    {
        int choice;
        do
        {
            UserInterface.PrintMenu("\n### Часть 3. Интерфейсы ###", "Формирование массива мест (ДСЧ)",
                "Печать массива мест", "Сортировка по названию (IComparable)",
                "Сортировка по количеству жителей (IComparer)", "Поиск места по названию (линейный)",
                "Поиск места по названию (бинарный)", "Формирование массива IInit (ДСЧ)", "Печать массива IInit",
                "Ввод элемента массива IInit с клавиатуры (Init)", "Клонирование и поверхностное копирование", "Назад");
            choice = UserInterface.EnterInt("Выберите пункт: ", 1, 11);
            switch (choice)
            {
                case 1:
                    CreatePlaces(ref places);
                    break;
                case 2:
                    PrintPlaces(places);
                    break;
                case 3:
                    SortByName(places);
                    break;
                case 4:
                    SortByPopulationDesc(places);
                    break;
                case 5:
                    LinearSearch(places);
                    break;
                case 6:
                    BinarySearchByName(places);
                    break;
                case 7:
                    items = MakeItems();
                    break;
                case 8:
                    PrintItems(items);
                    break;
                case 9:
                    InitItem(items);
                    break;
                case 10:
                    CloneDemo();
                    break;
            }
        } while (choice != 11);
    }

    // Сортировка по названию. Array.Sort сам вызывает CompareTo (интерфейс IComparable),
    // который реализован в классе Mesto.
    static void SortByName(Mesto[] places)
    {
        if (places == null)
        {
            Console.WriteLine("Массив не создан!");
            return;
        }
        Array.Sort(places);
        Console.WriteLine("Массив отсортирован по названию:");
        PrintPlaces(places);
    }

    // Сортировка по другому критерию: Array.Sort вторым аргументом получает объект с правилом сравнения (IComparer)
    static void SortByPopulationDesc(Mesto[] places)
    {
        if (places == null)
        {
            Console.WriteLine("Массив не создан!");
            return;
        }
        Array.Sort(places, new SortByPopulation());
        Console.WriteLine("Массив отсортирован по количеству жителей (по убыванию):");
        PrintPlaces(places);
    }

    // печать результата поиска по индексу (индекс -1 означает, что элемент не найден)
    static void PrintFound(Mesto[] places, int index)
    {
        if (index == -1)
        {
            Console.WriteLine("Место с таким названием не найдено");
        }
        else
        {
            Console.Write($"Найден элемент {index + 1}: ");
            places[index].Show();
        }
    }

    static void LinearSearch(Mesto[] places)
    {
        if (places == null)
        {
            Console.WriteLine("Массив не создан!");
            return;
        }
        string name = UserInterface.EnterString("Введите название места: ");
        PrintFound(places, Zaprosy.FindIndex(places, name));
    }

    // Бинарный поиск работает только в отсортированном массиве, поэтому сначала сортируем по названию
    static void BinarySearchByName(Mesto[] places)
    {
        if (places == null)
        {
            Console.WriteLine("Массив не создан!");
            return;
        }
        Array.Sort(places);
        Console.WriteLine("Массив предварительно отсортирован по названию:");
        PrintPlaces(places);
        string name = UserInterface.EnterString("Введите название места: ");
        PrintFound(places, Zaprosy.BinarySearch(places, name));
    }

    // Массив типа IInit: в него кладем объекты РАЗНЫХ классов - пять классов иерархии и класс Marshrut.
    // Объект Marshrut не связан с иерархией, но подходит в массив, потому что тоже реализует IInit.
    // RandomInit вызывается через интерфейс, одинаково для всех элементов.
    static IInit[] MakeItems()
    {
        int size = UserInterface.EnterInt($"Введите количество элементов (от 6 до {maxSize}): ", 6, maxSize);
        IInit[] arr = new IInit[size];
        for (int i = 0; i < size; i++)
        {
            int type;
            if (i < 6)
            {
                type = i + 1; // по одному объекту каждого класса
            }
            else
            {
                type = UserInterface.rnd.Next(1, 7);
            }
            if (type == 6)
            {
                arr[i] = new Marshrut();
            }
            else
            {
                arr[i] = CreateByType(type);
            }
            arr[i].RandomInit();
        }
        Console.WriteLine("Массив сформирован");
        return arr;
    }

    // У интерфейса IInit нет метода Show, поэтому смотрим, какого класса объект, и вызываем его Show
    static void ShowItem(IInit item)
    {
        if (item is Mesto)
        {
            ((Mesto)item).Show();
        }
        else if (item is Marshrut)
        {
            ((Marshrut)item).Show();
        }
    }

    static void PrintItems(IInit[] items)
    {
        if (items == null)
        {
            Console.WriteLine("Массив не создан!");
            return;
        }
        for (int i = 0; i < items.Length; i++)
        {
            Console.Write($"{i + 1}. ");
            ShowItem(items[i]);
        }
    }

    // ввод выбранного элемента с клавиатуры: Init() вызывается через интерфейс, для объекта нужного класса
    static void InitItem(IInit[] items)
    {
        if (items == null)
        {
            Console.WriteLine("Массив не создан!");
            return;
        }
        int number = UserInterface.EnterInt("Введите номер элемента: ", 1, items.Length);
        items[number - 1].Init();
        Console.Write("Элемент после ввода: ");
        ShowItem(items[number - 1]);
    }

    // Показ разницы между поверхностным копированием и клонированием.
    // У области есть поле-ссылка на объект (областной центр). После изменения центра в оригинале:
    //   - у поверхностной копии центр тоже изменился;
    //   - у клона не изменился.
    static void CloneDemo()
    {
        Oblast original = new Oblast();
        original.RandomInit();
        Oblast shallow = (Oblast)original.ShallowCopy(); // поверхностное копирование
        Oblast clone = (Oblast)original.Clone();         // клонирование

        Console.WriteLine("До изменения:");
        Console.Write("Оригинал:             ");
        original.Show();
        Console.Write("Поверхностная копия:  ");
        shallow.Show();
        Console.Write("Клон:                 ");
        clone.Show();

        original.Capital.Population = original.Capital.Population + 1000;

        Console.WriteLine("После увеличения количества жителей областного центра в оригинале на 1000:");
        Console.Write("Оригинал:             ");
        original.Show();
        Console.Write("Поверхностная копия:  ");
        shallow.Show();
        Console.Write("Клон:                 ");
        clone.Show();
    }
}
