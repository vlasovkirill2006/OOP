using laba11_OOP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestClass]
public class Lab11Tests
{
    // 1. Конструкторы Person и вывод в строку
    [TestMethod]
    public void PersonConstructorTest()
    {
        Person p = new Person("Иванов", 18);
        Assert.AreEqual("Иванов", p.Name);
        Assert.AreEqual(18, p.Age);
        Assert.AreEqual("Иванов, 18", p.ToString());

        Person empty = new Person();
        Assert.AreEqual("Без имени", empty.Name);
        Assert.AreEqual(0, empty.Age);
    }

    // 2. Вывод студента и свойство BasePerson
    [TestMethod]
    public void StudentToStringAndBasePersonTest()
    {
        Student s = new Student("Петров", 19, 2, 5);
        Assert.AreEqual("Петров, 19, курс=2, рейтинг=5", s.ToString());

        Person basePart = s.BasePerson;
        Assert.AreEqual("Петров", basePart.Name);
        Assert.AreEqual(19, basePart.Age);
        Assert.AreEqual(typeof(Person), basePart.GetType());
    }

    // 3. Сравнение по содержимому: Equals и GetHashCode
    [TestMethod]
    public void EqualsAndHashCodeTest()
    {
        Student s1 = new Student("Иванов", 18, 1, 4);
        Student s2 = new Student("Иванов", 18, 1, 4);
        Student s3 = new Student("Иванов", 18, 1, 5);

        Assert.IsTrue(s1.Equals(s2));
        Assert.AreEqual(s1.GetHashCode(), s2.GetHashCode());
        Assert.IsFalse(s1.Equals(s3));
        Assert.IsFalse(s1.Equals(null));
        // Person и Student с одинаковыми именем и возрастом - разные объекты
        Assert.IsFalse(new Person("Иванов", 18).Equals(s1));
    }

    // 4. Сравнение для сортировки: CompareTo
    [TestMethod]
    public void CompareToTest()
    {
        Person a = new Person("Иванов", 30);
        Person b = new Person("Петров", 20);
        Assert.IsTrue(a.CompareTo(b) < 0);
        Assert.IsTrue(b.CompareTo(a) > 0);
        Assert.AreEqual(0, a.CompareTo(new Person("Иванов", 99)));
        Assert.ThrowsException<ArgumentException>(() => a.CompareTo("строка"));
    }

    // 5. Клонирование объекта
    [TestMethod]
    public void CloneTest()
    {
        Student s = new Student("Сидоров", 21, 5, 4.5);
        Student copy = (Student)s.Clone();
        Assert.IsTrue(s.Equals(copy));
        Assert.AreNotSame(s, copy);
        copy.Name = "Другой";
        Assert.AreEqual("Сидоров", s.Name);
    }

    // 6. Задание 1: запросы
    [TestMethod]
    public void Task1QueriesTest()
    {
        Task1 t = new Task1();          // 3 студента и 1 человек
        Assert.AreEqual(4, t.Count);
        Assert.AreEqual(3, t.CountStudents());
        Assert.AreEqual(2, t.GetGoodStudents(4.5).Count);
        Assert.AreEqual(0, t.GetGoodStudents(6).Count);
        Assert.AreEqual(2, t.GetOlderThan(20).Count);
    }

    // 7. Задание 1: добавление и удаление
    [TestMethod]
    public void Task1AddRemoveTest()
    {
        Task1 t = new Task1();
        t.Add(new Person("Новый", 30));
        Assert.AreEqual(5, t.Count);
        Assert.IsTrue(t.RemoveAt(0));
        Assert.AreEqual(4, t.Count);
        Assert.IsFalse(t.RemoveAt(100));
        Assert.IsFalse(t.RemoveAt(-1));
        Assert.AreEqual(4, t.Count);
    }

    // 8. Задание 1: сортировка и поиск
    [TestMethod]
    public void Task1SortAndFindTest()
    {
        Task1 t = new Task1();
        t.Sort();
        Assert.AreEqual("Иванов", t.Get(0).Name);
        Assert.AreEqual("Сидоров", t.Get(3).Name);

        int index = t.Find("Петров");
        Assert.IsTrue(index >= 0);
        Assert.AreEqual("Петров", t.Get(index).Name);
        Assert.AreEqual(-1, t.Find("Нет такого"));
    }

    // 9. Задание 1: клонирование коллекции
    [TestMethod]
    public void Task1CloneTest()
    {
        Task1 t = new Task1();
        ArrayList shallow = t.ShallowClone();
        ArrayList deep = t.DeepClone();
        Assert.AreEqual(4, shallow.Count);
        Assert.AreEqual(4, deep.Count);
        Assert.AreSame(t.Get(0), shallow[0]);      // те же объекты
        Assert.AreNotSame(t.Get(0), deep[0]);      // новые объекты
        Assert.IsTrue(t.Get(0).Equals(deep[0]));   // но с тем же содержимым
    }

    // 10. Задание 2: добавление, удаление, поиск по ключу
    [TestMethod]
    public void Task2AddRemoveTest()
    {
        Task2 t = new Task2();
        Assert.AreEqual(4, t.Count);
        int key = t.Add(new Person("Новый", 30));
        Assert.AreEqual(5, key);
        Assert.AreEqual(5, t.Count);

        Person p;
        Assert.IsTrue(t.TryGet(key, out p));
        Assert.AreEqual("Новый", p.Name);

        Assert.IsTrue(t.Remove(key));
        Assert.IsFalse(t.Remove(key));
        Assert.IsFalse(t.TryGet(key, out p));
        Assert.AreEqual(4, t.Count);
    }

    // 11. Задание 2: запросы
    [TestMethod]
    public void Task2QueriesTest()
    {
        Task2 t = new Task2();
        Assert.AreEqual(3, t.CountStudents());
        Assert.AreEqual(2, t.GetGoodStudents(4.5).Count);
        Assert.AreEqual(0, t.GetGoodStudents(6).Count);
        Assert.AreEqual(2, t.GetOlderThan(20).Count);
    }

    // 12. Задание 2: сортировка и поиск по фамилии
    [TestMethod]
    public void Task2SortAndSearchTest()
    {
        Task2 t = new Task2();
        List<Person> sorted = t.GetSorted();
        Assert.AreEqual("Иванов", sorted[0].Name);
        Assert.AreEqual("Петров", sorted[1].Name);
        Assert.AreEqual("Семенов", sorted[2].Name);
        Assert.AreEqual("Сидоров", sorted[3].Name);

        List<int> keys = t.FindKeysByName("Петров");
        Assert.AreEqual(1, keys.Count);
        Assert.AreEqual(2, keys[0]);
        Assert.AreEqual(0, t.FindKeysByName("Нет такого").Count);
    }

    // 13. Задание 2: клонирование коллекции
    [TestMethod]
    public void Task2CloneTest()
    {
        Task2 t = new Task2();
        Dictionary<int, Person> shallow = t.ShallowClone();
        Dictionary<int, Person> deep = t.DeepClone();
        Person original;
        t.TryGet(1, out original);
        Assert.AreEqual(4, shallow.Count);
        Assert.AreEqual(4, deep.Count);
        Assert.AreSame(original, shallow[1]);
        Assert.AreNotSame(original, deep[1]);
        Assert.IsTrue(original.Equals(deep[1]));
    }

    // 14. Задание 3: создание коллекций, добавление и удаление
    [TestMethod]
    public void TestCollectionsCreateAddRemoveTest()
    {
        TestCollections tc = new TestCollections(1000);
        Assert.AreEqual(1000, tc.CountList);
        Assert.AreEqual(1000, tc.CountStringList);
        Assert.AreEqual(1000, tc.CountPersonDict);
        Assert.AreEqual(1000, tc.CountStringDict);

        Student extra = new Student("Новый", 20, 2, 4.2);
        tc.AddStudent(extra);
        Assert.AreEqual(1001, tc.CountList);
        Assert.AreEqual(1001, tc.CountStringList);
        Assert.AreEqual(1001, tc.CountPersonDict);
        Assert.AreEqual(1001, tc.CountStringDict);

        Assert.IsTrue(tc.RemoveStudent(extra));
        Assert.AreEqual(1000, tc.CountList);
        Assert.AreEqual(1000, tc.CountStringList);
        Assert.AreEqual(1000, tc.CountPersonDict);
        Assert.AreEqual(1000, tc.CountStringDict);
    }

    // 15. Задание 3: поиск первого, центрального, последнего и отсутствующего элемента
    [TestMethod]
    public void TestCollectionsSearchTest()
    {
        TestCollections tc = new TestCollections(1000);
        Student[] present =
        {
            tc.Copy(tc.GetStudent(0)),
            tc.Copy(tc.GetStudent(500)),
            tc.Copy(tc.GetStudent(999))
        };

        foreach (Student s in present)
        {
            Assert.IsTrue(tc.ContainsInList(s));
            Assert.IsTrue(tc.ContainsInStringList(s));
            Assert.IsTrue(tc.ContainsKeyPerson(s));
            Assert.IsTrue(tc.ContainsKeyString(s));
            Assert.IsTrue(tc.ContainsValueStudent(s));
        }

        Student missing = new Student("Нет такого", 99, 5, 5.0);
        Assert.IsFalse(tc.ContainsInList(missing));
        Assert.IsFalse(tc.ContainsInStringList(missing));
        Assert.IsFalse(tc.ContainsKeyPerson(missing));
        Assert.IsFalse(tc.ContainsKeyString(missing));
        Assert.IsFalse(tc.ContainsValueStudent(missing));
    }
}
