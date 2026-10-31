using System;

public class Uravnenie
{
    // закрытые атрибуты (коэффициенты уравнения)
    double a;
    double b;
    double c;

    // статическая переменная для подсчета созданных объектов
    static int count = 0;

    // Свойства

    public double A
    {
        get { return a; }
        set { a = value; }
    }

    public double B
    {
        get { return b; }
        set { b = value; }
    }

    public double C
    {
        get { return c; }
        set { c = value; }
    }

    // количество созданных объектов
    public static int Count
    {
        get { return count; }
    }

    // Конструкторы

    // без параметров: уравнение x^2 = 0
    public Uravnenie()
    {
        A = 1;
        B = 0;
        C = 0;
        count++;
    }

    // с параметрами
    public Uravnenie(double a, double b, double c)
    {
        A = a;
        B = b;
        C = c;
        count++;
    }

    // ---------- Вывод ----------

    // строковое представление уравнения, например: 2x^2 - 3x + 1 = 0
    public override string ToString()
    {
        string s = $"{a}x^2";
        if (b < 0)
        {
            s += $" - {-b}x";
        }
        else
        {
            s += $" + {b}x";
        }
        if (c < 0)
        {
            s += $" - {-c}";
        }
        else
        {
            s += $" + {c}";
        }
        return s + " = 0";
    }

    // Часть 1: корни уравнения 

    // дискриминант
    public double Discriminant()
    {
        return b * b - 4 * a * c;
    }

    // корни существуют, если уравнение квадратное (a != 0) и дискриминант не меньше 0
    public bool HasRoots()
    {
        return a != 0 && Discriminant() >= 0;
    }

    // метод класса: один из корней уравнения, если корней нет - 0
    public double FindRoot()
    {
        if (!HasRoots())
        {
            return 0;
        }
        return (-b + Math.Sqrt(Discriminant())) / (2 * a);
    }

    // статическая функция: один из корней уравнения u, если корней нет - 0
    public static double GetRoot(Uravnenie u)
    {
        double d = u.B * u.B - 4 * u.A * u.C;
        if (u.A == 0 || d < 0)
        {
            return 0;
        }
        return (-u.B + Math.Sqrt(d)) / (2 * u.A);
    }

    // наибольший по модулю корень, если корней нет - 0
    public double MaxAbsRoot()
    {
        if (!HasRoots())
        {
            return 0;
        }
        double d = Math.Sqrt(Discriminant());
        double x1 = Math.Abs((-b + d) / (2 * a));
        double x2 = Math.Abs((-b - d) / (2 * a));
        if (x1 >= x2)
        {
            return x1;
        }
        return x2;
    }

    // Часть 2: перегрузка операций

    // ++ увеличивает коэффициенты на 1
    public static Uravnenie operator ++(Uravnenie u)
    {
        return new Uravnenie(u.a + 1, u.b + 1, u.c + 1);
    }

    // -- уменьшает коэффициенты на 1
    public static Uravnenie operator --(Uravnenie u)
    {
        return new Uravnenie(u.a - 1, u.b - 1, u.c - 1);
    }

    // неявное приведение к double: один из корней или 0
    public static implicit operator double(Uravnenie u)
    {
        return u.FindRoot();
    }

    // явное приведение к bool: true, если корни существуют
    public static explicit operator bool(Uravnenie u)
    {
        return u.HasRoots();
    }

    // уравнения равны, если равны их коэффициенты
    public static bool operator ==(Uravnenie t1, Uravnenie t2)
    {
        if (ReferenceEquals(t1, null) || ReferenceEquals(t2, null))
        {
            return ReferenceEquals(t1, t2);
        }
        return t1.a == t2.a && t1.b == t2.b && t1.c == t2.c;
    }

    public static bool operator !=(Uravnenie t1, Uravnenie t2)
    {
        return !(t1 == t2);
    }

    // вместе с == нужно переопределить Equals и GetHashCode
    public override bool Equals(object obj)
    {
        Uravnenie u = obj as Uravnenie;
        return this == u;
    }

    public override int GetHashCode()
    {
        return a.GetHashCode() ^ b.GetHashCode() ^ c.GetHashCode();
    }
}
