using System;

public sealed partial class Trapeze
{
    private int a, b, h, c;

    public Trapeze(int baseA, int baseB, int height, int color)
    {
        a = baseA; b = baseB; h = height; c = color;
    }

    // Індексатор
    public int this[int index]
    {
        get
        {
            return index switch
            {
                0 => a,
                1 => b,
                2 => h,
                3 => c,
                _ => throw new IndexOutOfRangeException("Індекс має бути від 0 до 3!")
            };
        }

        set
        {
            switch (index)
            {
                case 0: a = value; break;
                case 1: b = value; break;
                case 2: h = value; break;
                case 3: c = value; break;
                default:
                    Console.WriteLine("Помилка: невірний індекс. Спробуйте ще раз. (Індекс має бути від 0 до 3!)");
                    break;
            }   
        }
    }
}