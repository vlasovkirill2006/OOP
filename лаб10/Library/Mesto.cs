using System;

// Базовый класс иерархии: место.
// Класс реализует три интерфейса:
//   IInit       - умеет вводить данные с клавиатуры и формировать их ДСЧ;
//   IComparable - объекты можно сравнивать и сортировать (Array.Sort);
//   ICloneable  - объекты можно клонировать (метод Clone).
public class Mesto : IInit, IComparable, ICloneable
{
    protected string name;

    // Свойство с проверкой ограничения: название не может быть пустым.
    // Если значение не подходит, записываем значение по умолчанию.
    public string Name
    {
        get { return name; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                name = value;
            }
            else
            {
                name = "Без названия";
            }
        }
    }


    // без параметров
    public Mesto()
    {
        Name = "Без названия";
    }

    // с параметрами
    public Mesto(string name)
    {
        Name = name;
    }

    // копирования: создает новый объект с такими же данными, как у other
    public Mesto(Mesto other)
    {
        Name = other.Name;
    }

    public virtual void Show()
    {
        Console.WriteLine($"Место: {name}");
    }
    public void ShowNV()
    {
        Console.WriteLine($"[обычный метод] Место: {name}");
    }

    // ввод данных с клавиатуры (метод интерфейса IInit)
    public virtual void Init()
    {
        Name = UserInterface.EnterString("Введите название: ");
    }

    // формирование данных датчиком случайных чисел (метод интерфейса IInit)
    public virtual void RandomInit()
    {
        Name = Data.Pick(Data.places);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Mesto m = (Mesto)obj;
        return name == m.name;
    }

    // вместе с Equals нужно переопределять GetHashCode: равные объекты должны давать одинаковый хеш-код
    public override int GetHashCode()
    {
        return name.GetHashCode();
    }

    public int CompareTo(object obj)
    {
        Mesto m = obj as Mesto; // as при неудаче вернет null, а не бросит исключение
        if (m == null)
        {
            throw new ArgumentException("Сравнивать можно только с объектом типа Mesto");
        }
        return string.Compare(name, m.name, StringComparison.Ordinal);
    }

    public virtual object Clone()
    {
        return new Mesto(this);
    }

    public Mesto ShallowCopy()
    {
        return (Mesto)MemberwiseClone();
    }
}
