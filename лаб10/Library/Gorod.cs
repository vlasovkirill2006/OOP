using System;

// Город - наследник места.
// Из Mesto город получает поле name и свойство Name, а добавляет область и количество жителей.
public class Gorod : Mesto
{
    protected string oblastName; // название области, в которой находится город
    protected int population;    // количество жителей

    public string OblastName
    {
        get { return oblastName; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                oblastName = value;
            }
            else
            {
                oblastName = "Без области";
            }
        }
    }

    // ограничение: количество жителей от 0 до 30 000 000
    public int Population
    {
        get { return population; }
        set
        {
            if (value >= 0 && value <= 30000000)
            {
                population = value;
            }
            else
            {
                population = 0;
            }
        }
    }

    public Gorod() : base()
    {
        OblastName = "Без области";
        Population = 0;
    }

    public Gorod(string name, string oblastName, int population) : base(name)
    {
        OblastName = oblastName;
        Population = population;
    }
    public Gorod(Gorod other) : base(other)
    {
        OblastName = other.OblastName;
        Population = other.Population;
    }


    // override - переопределение виртуального метода базового класса
    public override void Show()
    {
        Console.WriteLine($"Город: {name}, область: {oblastName}, жителей: {population}");
    }

    // new - скрытие невиртуального метода.
    // Новая версия будет вызвана только для переменной типа Gorod или его наследников.
    public new void ShowNV()
    {
        Console.WriteLine($"[обычный метод] Город: {name}, область: {oblastName}, жителей: {population}");
    }

    public override void Init()
    {
        base.Init(); // base. - вызов метода базового класса: он вводит название
        OblastName = UserInterface.EnterString("Введите название области: ");
        Population = UserInterface.EnterInt("Введите количество жителей: ", 0, 30000000);
    }

    // base.RandomInit() не вызываем: у города свой список названий, а не список названий мест
    public override void RandomInit()
    {
        Name = Data.Pick(Data.cities);
        OblastName = Data.Pick(Data.oblasti);
        Population = UserInterface.rnd.Next(1000, 1000001);
    }

    public override bool Equals(object obj)
    {
        if (!base.Equals(obj))
        {
            return false;
        }
        Gorod g = (Gorod)obj;
        return oblastName == g.oblastName && population == g.population;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ oblastName.GetHashCode() ^ population;
    }

    public override object Clone()
    {
        return new Gorod(this);
    }
}
