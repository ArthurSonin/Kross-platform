using System;

// Друга частина часткового класу
public sealed partial class Trapeze
{
    // Перевантаження операторів ++ та --
    public static Trapeze operator ++(Trapeze t)
    {
        t.a++; t.b++;
        return t;
    }

    public static Trapeze operator --(Trapeze t)
    {
        t.a--; t.b--;
        return t;
    }

    // Перевантаження true/false
    public static bool operator true(Trapeze t) => t.a > 0 && t.b > 0 && t.h > 0;
    public static bool operator false(Trapeze t) => t.a <= 0 || t.b <= 0 || t.h <= 0;

    // Метод для виводу
    public void Show()
    {
        Console.WriteLine($"Трапеція: a={a}, b={b}, h={h}, колір={c}");
    }
}