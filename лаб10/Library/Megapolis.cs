using System;

// Мегаполис - наследник города: иерархия из трех уровней Mesto -> Gorod -> Megapolis.
// Из Gorod мегаполис получает название, область и количество жителей, а добавляет станции метро.
public class Megapolis : Gorod
{
    protected int metroStations; // количество станций метро

    // ограничение: от 0 до 500 станций
    public int MetroStations
    {
        get { return metroStations; }
        set
        {
            if (value >= 0 && value <= 500)
            {
                metroStations = value;
            }
            else
            {
                metroStations = 0;
            }
        }
    }


    public Megapolis() : base()
    {
        MetroStations = 0;
    }

    public Megapolis(string name, string oblastName, int population, int metroStations)
        : base(name, oblastName, population)
    {
        MetroStations = metroStations;
    }

    public Megapolis(Megapolis other) : base(other)
    {
        MetroStations = other.MetroStations;
    }


    public override void Show()
    {
        Console.WriteLine($"Мегаполис: {name}, область: {oblastName}, жителей: {population}, станций метро: {metroStations}");
    }

    public new void ShowNV()
    {
        Console.WriteLine($"[обычный метод] Мегаполис: {name}, область: {oblastName}, жителей: {population}, станций метро: {metroStations}");
    }

    public override void Init()
    {
        base.Init(); // вводит название, область и жителей (метод Gorod)
        MetroStations = UserInterface.EnterInt("Введите количество станций метро: ", 0, 500);
    }

    // Мегаполис - город с населением не меньше миллиона, поэтому диапазон жителей здесь другой
    public override void RandomInit()
    {
        Name = Data.Pick(Data.megapolisy);
        OblastName = Data.Pick(Data.oblasti);
        Population = UserInterface.rnd.Next(1000000, 20000001);
        MetroStations = UserInterface.rnd.Next(1, 301);
    }


    public override bool Equals(object obj)
    {
        if (!base.Equals(obj))
        {
            return false;
        }
        Megapolis m = (Megapolis)obj;
        return metroStations == m.metroStations;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ metroStations;
    }

    public override object Clone()
    {
        return new Megapolis(this);
    }
}
