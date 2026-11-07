using System;

public class Oblast : Mesto
{
    protected double area;   // площадь в квадратных километрах
    protected Gorod capital; // областной центр

    // ограничение: площадь от 1 до 2 000 000 кв. км
    public double Area
    {
        get { return area; }
        set
        {
            if (value >= 1 && value <= 2000000)
            {
                area = value;
            }
            else
            {
                area = 1;
            }
        }
    }

    // ограничение: областной центр не может быть null, иначе подставляем город по умолчанию
    public Gorod Capital
    {
        get { return capital; }
        set
        {
            if (value != null)
            {
                capital = value;
            }
            else
            {
                capital = new Gorod();
            }
        }
    }


    public Oblast() : base()
    {
        Area = 1;
        Capital = new Gorod();
    }

    // Здесь объект capital не копируется: область будет хранить ту же ссылку, которую ей передали.
    public Oblast(string name, double area, Gorod capital) : base(name)
    {
        Area = area;
        Capital = capital;
    }

    // Конструктор копирования создает НОВЫЙ областной центр (new Gorod(other.Capital)),
    // поэтому копия не зависит от оригинала. На нем построен метод Clone.
    public Oblast(Oblast other) : base(other)
    {
        Area = other.Area;
        Capital = new Gorod(other.Capital);
    }


    public override void Show()
    {
        Console.WriteLine($"Область: {name}, площадь: {area} кв. км, областной центр: {capital.Name} ({capital.Population} жителей)");
    }

    public new void ShowNV()
    {
        Console.WriteLine($"[обычный метод] Область: {name}, площадь: {area} кв. км, областной центр: {capital.Name} ({capital.Population} жителей)");
    }

    public override void Init()
    {
        base.Init();
        Area = UserInterface.EnterDouble("Введите площадь (кв. км): ", 1, 2000000);
        Console.WriteLine("Введите данные областного центра:");
        Capital = new Gorod();
        Capital.Init();
        Capital.OblastName = Name; // областной центр находится в этой же области
    }

    public override void RandomInit()
    {
        Name = Data.Pick(Data.oblasti);
        Area = UserInterface.rnd.Next(10000, 500001);
        Capital = new Gorod();
        Capital.RandomInit();
        Capital.OblastName = Name;
    }

    // Capital.Equals сравнивает центры по содержимому, а не по ссылке
    public override bool Equals(object obj)
    {
        if (!base.Equals(obj))
        {
            return false;
        }
        Oblast o = (Oblast)obj;
        return area == o.area && capital.Equals(o.capital);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ area.GetHashCode() ^ capital.GetHashCode();
    }

    public override object Clone()
    {
        return new Oblast(this);
    }
}
