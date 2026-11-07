using System;

public class Marshrut : IInit
{
    string departure;   // пункт отправления
    string destination; // пункт назначения
    int length;   // длина маршрута в километрах

    public string From
    {
        get { return departure; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                departure = value;
            }
            else
            {
                departure = "Не задано";
            }
        }
    }

    public string To
    {
        get { return destination; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                destination = value;
            }
            else
            {
                destination = "Не задано";
            }
        }
    }

    // ограничение: длина от 1 до 20 000 км
    public int Length
    {
        get { return length; }
        set
        {
            if (value >= 1 && value <= 20000)
            {
                length = value;
            }
            else
            {
                length = 1;
            }
        }
    }


    public Marshrut()
    {
        From = "Не задано";
        To = "Не задано";
        Length = 1;
    }

    public Marshrut(string departure, string destination, int length)
    {
        From = departure;
        To = destination;
        Length = length;
    }

    public Marshrut(Marshrut other)
    {
        From = other.From;
        To = other.To;
        Length = other.Length;
    }


    public void Show()
    {
        Console.WriteLine($"Маршрут: {departure} - {destination}, длина: {length} км");
    }

    // Реализация методов интерфейса IInit (public обязателен)
    public void Init()
    {
        From = UserInterface.EnterString("Введите пункт отправления: ");
        To = UserInterface.EnterString("Введите пункт назначения: ");
        Length = UserInterface.EnterInt("Введите длину маршрута (км): ", 1, 20000);
    }

    public void RandomInit()
    {
        From = Data.Pick(Data.cities);
        do
        {
            To = Data.Pick(Data.cities); // пункт назначения должен отличаться от пункта отправления
        } while (To == From);
        Length = UserInterface.rnd.Next(10, 3001);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Marshrut m = (Marshrut)obj;
        return departure == m.departure && destination == m.destination && length == m.length;
    }

    public override int GetHashCode()
    {
        return departure.GetHashCode() ^ destination.GetHashCode() ^ length;
    }
}
