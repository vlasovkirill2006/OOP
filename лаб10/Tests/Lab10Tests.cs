using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

[TestClass]
public class Lab10Tests
{
    // 1. Проверка ограничений в свойствах: неверное значение заменяется значением по умолчанию
    [TestMethod]
    public void PropertiesValidationTest()
    {
        Mesto m = new Mesto("");
        Assert.AreEqual("Без названия", m.Name);

        Gorod g = new Gorod("Тюмень", "Тюменская область", -5);
        Assert.AreEqual(0, g.Population);
        g.Population = 100;
        Assert.AreEqual(100, g.Population);

        Megapolis mp = new Megapolis("Москва", "Московская область", 1000000, 600);
        Assert.AreEqual(0, mp.MetroStations);

        Oblast o = new Oblast("Курганская область", 0, null);
        Assert.AreEqual(1, o.Area);
        Assert.IsTrue(o.Capital != null);

        Adres a = new Adres("Омск", "Мира", 0);
        Assert.AreEqual(1, a.House);         // номер дома не меньше 1
    }

    // 2. Проверка конструкторов с параметрами: поля наследников и базового класса заполняются
    [TestMethod]
    public void ConstructorTest()
    {
        Megapolis mp = new Megapolis("Москва", "Московская область", 12000000, 300);

        Assert.AreEqual("Москва", mp.Name);
        Assert.AreEqual("Московская область", mp.OblastName); 
        Assert.AreEqual(12000000, mp.Population);
        Assert.AreEqual(300, mp.MetroStations);
    }

    // 3. Проверка конструктора копирования: копия равна оригиналу по содержимому, но это другой объект
    [TestMethod]
    public void CopyConstructorTest()
    {
        Gorod g = new Gorod("Пермь", "Пермский край", 1000);
        Gorod copy = new Gorod(g);

        Assert.IsTrue(g.Equals(copy));
        Assert.IsFalse(ReferenceEquals(g, copy));
    }

    // 4. Проверка метода Equals
    [TestMethod]
    public void EqualsTest()
    {
        Gorod g1 = new Gorod("Омск", "Омская область", 1000);
        Gorod g2 = new Gorod("Омск", "Омская область", 1000);
        Gorod g3 = new Gorod("Омск", "Омская область", 2000);
        Mesto m = new Mesto("Омск");

        Assert.IsTrue(g1.Equals(g2));   
        Assert.IsFalse(g1.Equals(g3)); 
        Assert.IsFalse(m.Equals(g1));   
        Assert.IsFalse(g1.Equals(null));
    }

    // 5. Разница между виртуальным и обычным методом
    [TestMethod]
    public void VirtualAndNonVirtualTest()
    {
        Mesto m = new Gorod("Омск", "Омская область", 1000);
        StringWriter writer = new StringWriter();
        TextWriter old = Console.Out;
        Console.SetOut(writer);

        m.Show();
        string virtualResult = writer.ToString();
        writer.GetStringBuilder().Clear();
        m.ShowNV();
        string normalResult = writer.ToString();

        Console.SetOut(old); 
        Assert.IsTrue(virtualResult.Contains("Город: Омск"));
        Assert.IsTrue(normalResult.Contains("[обычный метод] Место: Омск"));
    }

    // 6. Сортировка по названию (интерфейс IComparable)
    [TestMethod]
    public void SortByNameTest()
    {
        Mesto[] places = { new Gorod("Уфа", "Башкортостан", 1), new Mesto("Долина"), new Adres("Омск", "Мира", 1) };

        Array.Sort(places);

        Assert.AreEqual("Долина", places[0].Name);
        Assert.AreEqual("Омск", places[1].Name);
        Assert.AreEqual("Уфа", places[2].Name);
    }

    // 7. Сортировка по количеству жителей по убыванию (интерфейс IComparer);
    [TestMethod]
    public void SortByPopulationTest()
    {
        Mesto[] places =
        {
            new Mesto("Долина"),
            new Gorod("Омск", "Омская область", 100),
            new Megapolis("Москва", "Московская область", 12000000, 300)
        };

        Array.Sort(places, new SortByPopulation());

        Assert.AreEqual("Москва", places[0].Name);
        Assert.AreEqual("Омск", places[1].Name);
        Assert.AreEqual("Долина", places[2].Name);
    }

    // 8. Запросы: названия городов, суммарное население, количество мегаполисов, количество адресов
    [TestMethod]
    public void QueriesTest()
    {
        Mesto[] places =
        {
            new Gorod("Омск", "Омская область", 1000),
            new Megapolis("Москва", "Омская область", 5000, 10),
            new Gorod("Тюмень", "Тюменская область", 700),
            new Adres("Омск", "Мира", 5),
            new Adres("Омск", "Ленина", 7),
            new Mesto("Долина")
        };

        string[] names = Zaprosy.CityNames(places, "Омская область");
        Assert.AreEqual(2, names.Length);
        Assert.AreEqual("Омск", names[0]);
        Assert.AreEqual("Москва", names[1]);

        Assert.AreEqual(6000, Zaprosy.TotalPopulation(places, "Омская область"));
        Assert.AreEqual(1, Zaprosy.CountMegapolis(places, "Омская область"));
        Assert.AreEqual(0, Zaprosy.CountMegapolis(places, "Тюменская область"));
        Assert.AreEqual(2, Zaprosy.CountAdresa(places, "Омск"));
    }

    // 9. Линейный и бинарный поиск по названию (для бинарного массив должен быть отсортирован)
    [TestMethod]
    public void SearchTest()
    {
        Mesto[] places = { new Mesto("Урочище"), new Mesto("Долина"), new Mesto("Перевал"), new Mesto("Плато") };

        Assert.AreEqual(2, Zaprosy.FindIndex(places, "Перевал"));
        Assert.AreEqual(-1, Zaprosy.FindIndex(places, "Озеро"));

        Array.Sort(places);
        int index = Zaprosy.BinarySearch(places, "Перевал");
        Assert.AreEqual("Перевал", places[index].Name);
        Assert.AreEqual(-1, Zaprosy.BinarySearch(places, "Озеро"));
    }

    // 10. Клонирование и поверхностное копирование
    [TestMethod]
    public void CloneAndShallowCopyTest()
    {
        Oblast original = new Oblast("Омская область", 140000, new Gorod("Омск", "Омская область", 1000));
        Oblast shallow = (Oblast)original.ShallowCopy();
        Oblast clone = (Oblast)original.Clone();

        original.Capital.Population = 2000;

        Assert.AreEqual(2000, shallow.Capital.Population);
        Assert.AreEqual(1000, clone.Capital.Population);
    }

    // 11. Клонирование возвращает объект того же класса (Clone виртуальный)
    [TestMethod]
    public void CloneTypeTest()
    {
        Mesto m = new Megapolis("Москва", "Московская область", 12000000, 300);

        object clone = m.Clone();

        Assert.IsTrue(clone is Megapolis);
        Assert.IsTrue(m.Equals(clone));
    }

    // 12. Массив типа IInit: в нем объекты разных классов, RandomInit вызывается через интерфейс
    [TestMethod]
    public void IInitArrayTest()
    {
        IInit[] items = { new Mesto(), new Oblast(), new Gorod(), new Megapolis(), new Adres(), new Marshrut() };

        for (int i = 0; i < items.Length; i++)
        {
            items[i].RandomInit();
        }

        Assert.IsTrue(((Mesto)items[0]).Name != "Без названия");
        Assert.IsTrue(((Gorod)items[2]).Population >= 1000);
        Assert.IsTrue(((Megapolis)items[3]).Population >= 1000000);
        Assert.IsTrue(((Marshrut)items[5]).From != ((Marshrut)items[5]).To);
    }
}
