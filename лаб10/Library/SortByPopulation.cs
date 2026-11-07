using System;
using System.Collections;
public class SortByPopulation : IComparer
{
    // Количество жителей есть только у города и мегаполиса. У остальных мест считаем 0.
    // as приводит объект к типу Gorod, а если это не город - вернет null.
    static int GetPopulation(object ob)
    {
        Gorod g = ob as Gorod;
        if (g != null)
        {
            return g.Population;
        }
        return 0;
    }

    // Метод Compare возвращает: число меньше 0, если ob1 должен стоять РАНЬШЕ ob2; 0 - если равны;
    // число больше 0, если ob1 должен стоять ПОЗЖЕ ob2.
    // Если жителей поровну, места упорядочиваются по названию (через CompareTo из IComparable).
    int IComparer.Compare(object ob1, object ob2)
    {
        int p1 = GetPopulation(ob1);
        int p2 = GetPopulation(ob2);
        if (p1 > p2)
        {
            return -1;
        }
        if (p1 < p2)
        {
            return 1;
        }
        return ((Mesto)ob1).CompareTo(ob2);
    }
}
