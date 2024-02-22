#region Ref Keyword

using System.Xml.Linq;

int b = 5;

Console.WriteLine(b);

X(ref b);

Console.WriteLine(b);

void X(ref int _b)
{
    _b++;
}
#endregion

#region Ref Return
//Performans gerektiren durumlarda kodu optimize etmek ve gereksiz değişken tanımları ve veri tekrarını engellemek için oldukça faydalıdır.
ref int Y(ref int _b)
{
    _b = 124;
    return ref _b;
}
#endregion

#region Ref Local
int x = 10;
ref int y = ref x;
//x ve y aynı bellek adresini tutuyorlar.
Console.WriteLine(x);
Console.WriteLine(y);
x = 20;
Console.WriteLine(x);
Console.WriteLine(y);

#endregion