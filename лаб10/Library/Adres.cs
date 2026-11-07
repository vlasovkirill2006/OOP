using System;

// Адрес - наследник места: в поле Name хранится название города, а добавляются улица и номер дома.
public class Adres : Mesto
{
    protected string street; // улица
    protected int house;     // номер дома

    public string Street
    {
        get { return street; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                street = value;
            }
            else
            {
                street = "Без улицы";
            }
        }
    }

    // ограничение: номер дома от 1 до 999
    public int House
    {
        get { return house; }
        set
        {
            if (value >= 1 && value <= 999)
            {
                house = value;
            }
            else
            {
                house = 1;
            }
        }
    }


    public Adres() : base()
    {
        Street = "Без улицы";
        House = 1;
    }

    public Adres(string city, string street, int house) : base(city)
    {
        Street = street;
        House = house;
    }

    public Adres(Adres other) : base(other)
    {
        Street = other.Street;
        House = other.House;
    }


    public override void Show()
    {
        Console.WriteLine($"Адрес: г. {name}, ул. {street}, д. {house}");
    }

    public new void ShowNV()
    {
        Console.WriteLine($"[обычный метод] Адрес: г. {name}, ул. {street}, д. {house}");
    }

    public override void Init()
    {
        Name = UserInterface.EnterString("Введите название города: ");
        Street = UserInterface.EnterString("Введите название улицы: ");
        House = UserInterface.EnterInt("Введите номер дома: ", 1, 999);
    }

    public override void RandomInit()
    {
        Name = Data.Pick(Data.cities);
        Street = Data.Pick(Data.streets);
        House = UserInterface.rnd.Next(1, 200);
    }


    public override bool Equals(object obj)
    {
        if (!base.Equals(obj))
        {
            return false;
        }
        Adres a = (Adres)obj;
        return street == a.street && house == a.house;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ street.GetHashCode() ^ house;
    }

    public override object Clone()
    {
        return new Adres(this);
    }
}
