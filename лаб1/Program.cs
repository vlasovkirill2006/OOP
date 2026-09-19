//1

//int n;
//bool ok;
//do
//{
   // Console.Write("Введите n: ");
   // ok = int.TryParse(Console.ReadLine(), out n);
  //  if (!ok) Console.WriteLine("Ошибка: некорректный ввод n. Попробуйте снова.");
//} while (!ok);

//int m;
//do
//{
  //  Console.Write("Введите m: ");
    //ok = int.TryParse(Console.ReadLine(), out m);
    //if (!ok) Console.WriteLine("Ошибка: некорректный ввод m. Попробуйте снова.");
//} while (!ok);

//Console.Write("1) m - ++n = " + (m+--n));
//Console.WriteLine(",  m = " + m + ",  n = " + n);

//Console.Write("2) m++ > --n = " + (m++<--n));
//Console.WriteLine(",  m = " + m + ",  n = " + n);

//Console.Write("3) m-- < ++n = " + (--m>n--));
//Console.WriteLine(",  m = " + m + ",  n = " + n);


void primer()
{
    double x;
    bool okX;
    do
    {
        Console.Write("Введите x: ");
        okX = double.TryParse(Console.ReadLine(), out x);
        if (!okX)
        {
            Console.WriteLine("Ошибка: некорректный ввод x. Попробуйте снова.");
            continue;
        }
        if (x == 0 || Math.Pow(x, 3) + Math.Pow(x, 4) < 0)
        {
            Console.WriteLine("Ошибка вычислений: x не входит в ОДЗ. Попробуйте снова.");
            okX = false;
        }
    } while (!okX);

    double koren = Math.Pow(Math.Pow(x, 3) + Math.Pow(x, 4), 1.0 / 5.0);
    double cotangens = 1 / Math.Tan(Math.Atan(Math.Pow(x, 2)));
    double result = koren + cotangens;
    Console.WriteLine("При x = " + x + ": " + result);
}

//primer();
//primer();




//2

double x1;
bool ok;
do
{
    Console.Write("Введите координату X1: ");
    ok = double.TryParse(Console.ReadLine(), out x1);
    if (!ok) Console.WriteLine("Ошибка: некорректный ввод X1. Попробуйте снова.");
} while (!ok);

double y1;
do
{
    Console.Write("Введите координату Y1: ");
    ok = double.TryParse(Console.ReadLine(), out y1);
    if (!ok) Console.WriteLine("Ошибка: некорректный ввод Y1. Попробуйте снова.");
} while (!ok);

bool oblast = (x1 * x1 + y1 * y1 <= 4) && (Math.Abs(x1) + Math.Abs(y1) >= 2);
Console.WriteLine("Точка принадлежит заштрихованной области: " + oblast);




//3

//double aD = 1000.0;
//double bD = 0.0001;

//double chislitelD = Math.Pow(aD - bD, 3) - Math.Pow(aD, 2);
//double znamenatelD = -Math.Pow(bD, 3) + 3 * aD * Math.Pow(bD, 2) - 3 * Math.Pow(aD, 2) * bD;
//double resultD = chislitelD / znamenatelD;

//float aF = 1000.0f;
//float bF = 0.0001f;
//float chislitelF = (float)Math.Pow(aF - bF, 3) - (float)Math.Pow(aF, 2);
//float znamenatelF = -(float)Math.Pow(bF, 3) + 3 * aF * (float)Math.Pow(bF, 2) - 3 * (float)Math.Pow(aF, 2) * bF;
//float resultF = chislitelF / znamenatelF;

//Console.WriteLine("Результат (double): " + resultD);
//Console.WriteLine("Результат (float):  " + resultF);
//Console.WriteLine("Разница: " + Math.Abs(resultD - resultF));