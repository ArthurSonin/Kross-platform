using System;
using System.Collections.Generic;
using System.Linq;

// TASK 1 -----------------------------------------------------------------------------------------------------------------------
public class Trapeze
{
    private int a, b, h, c;

    public Trapeze(int baseA, int baseB, int height, int color)
    {
        a = baseA; b = baseB; h = height; c = color;
    }

    // 1. Індексатор
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
                _ => throw new IndexOutOfRangeException("Помилка: індекс має бути від 0 до 3!")
            };
        }
    }

    // 2. Перевантаження ++ та -- (збільшує/зменшує a та b на 1)
    public static Trapeze operator ++(Trapeze t)
    {
        t.a++;
        t.b++;
        return t;
    }

    public static Trapeze operator --(Trapeze t)
    {
        t.a--;
        t.b--;
        return t;
    }

    // 3. Перевантаження сталих true і false
    // Трапеція "існує" (true), якщо основи та висота більші за 0
    public static bool operator true(Trapeze t) => t.a > 0 && t.b > 0 && t.h > 0;
    public static bool operator false(Trapeze t) => t.a <= 0 || t.b <= 0 || t.h <= 0;

    // 4. Перевантаження операції * (множить a і h на скаляр)
    public static Trapeze operator *(Trapeze t, int scalar)
    {
        t.a *= scalar;
        t.h *= scalar;
        return t;
    }

    // 5. Перетворення типів (Trapeze в string і навпаки)
    public static implicit operator string(Trapeze t)
    {
        return $"Трапеція: a={t.a}, b={t.b}, h={t.h}, колір={t.c}";
    }

    public static explicit operator Trapeze(string s)
    {
        // Припускаємо формат "a b h c" через пробіл
        string[] parts = s.Split(' ');
        return new Trapeze(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
    }
}

// TASK 2 ---------------------------------------------------------------------------------------------------------------------

public class VectorFloat
{
    // ПОЛЯ (захищені) - точно як на зображенні
    protected float[] FArray; // масив
    protected uint num;       // розмір вектора
    protected int codeError;  // код помилки
    protected static uint num_vec = 0; // кількість векторів

    // КОНСТРУКТОРИ
    public VectorFloat()
    {
        num = 1;
        FArray = new float[1] { 0.0f };
        codeError = 0;
        num_vec++;
    }

    public VectorFloat(uint size)
    {
        this.num = size;
        FArray = new float[num];
        for (int i = 0; i < num; i++) FArray[i] = 0;
        codeError = 0;
        num_vec++;
    }

    public VectorFloat(uint size, float initValue)
    {
        this.num = size;
        FArray = new float[num];
        for (int i = 0; i < num; i++) FArray[i] = initValue;
        codeError = 0;
        num_vec++;
    }

    // ДЕСТРУКТОР
    ~VectorFloat()
    {
        Console.WriteLine("Вектор видалено.");
    }

    // МЕТОДИ
    public void Input()
    {
        for (int i = 0; i < num; i++)
        {
            Console.Write($"[{i}]: ");
            if (!float.TryParse(Console.ReadLine(), out FArray[i])) codeError = -2;
        }
    }

    public void Display()
    {
        foreach (var x in FArray) Console.Write(x + " ");
        Console.WriteLine();
    }

    public void SetAllValues(float value)
    {
        for (int i = 0; i < num; i++) FArray[i] = value;
    }

    public static uint GetTotalVectors() => num_vec;

    public void AssignValue(float val) => SetAllValues(val);

    // ВЛАСТИВОСТІ
    public uint Size => num; // тільки для читання
    public int ErrorCode
    {
        get => codeError;
        set => codeError = value;
    }

    // ІНДЕКСАТОР
    public float this[int index]
    {
        get
        {
            if (index < 0 || index >= num)
            {
                codeError = -1; // помилка індексу
                return 0;
            }
            return FArray[index];
        }
        set
        {
            if (index < 0 || index >= num) codeError = -1;
            else FArray[index] = value;
        }
    }

    // ПЕРЕВАНТАЖЕННЯ ОПЕРАТОРІВ

    // Унарні ++, --
    public static VectorFloat operator ++(VectorFloat v)
    {
        for (int i = 0; i < v.num; i++) v.FArray[i]++;
        return v;
    }

    public static VectorFloat operator --(VectorFloat v)
    {
        for (int i = 0; i < v.num; i++) v.FArray[i]--;
        return v;
    }

    // Сталі true, false, !
    public static bool operator true(VectorFloat v) => v.num != 0 && v.FArray.Any(x => x != 0);
    public static bool operator false(VectorFloat v) => v.num == 0 || v.FArray.All(x => x == 0);
    public static bool operator !(VectorFloat v) => v.num == 0 || v.FArray.All(x => x == 0);

    // Побітова інверсія ~ для float
    public static VectorFloat operator ~(VectorFloat v)
    {
        VectorFloat res = new VectorFloat(v.num);
        for (int i = 0; i < v.num; i++)
        {
            int bits = BitConverter.SingleToInt32Bits(v.FArray[i]);
            res.FArray[i] = BitConverter.Int32BitsToSingle(~bits);
        }
        return res;
    }

    // --- АРИФМЕТИКА (+, -, *, /, %) ---
    // Для двох векторів
    public static VectorFloat operator +(VectorFloat v1, VectorFloat v2) => ApplyOp(v1, v2, (a, b) => a + b);
    public static VectorFloat operator -(VectorFloat v1, VectorFloat v2) => ApplyOp(v1, v2, (a, b) => a - b);
    public static VectorFloat operator *(VectorFloat v1, VectorFloat v2) => ApplyOp(v1, v2, (a, b) => a * b);
    public static VectorFloat operator /(VectorFloat v1, VectorFloat v2) => ApplyOp(v1, v2, (a, b) => b != 0 ? a / b : 0);
    public static VectorFloat operator %(VectorFloat v1, VectorFloat v2) => ApplyOp(v1, v2, (a, b) => b != 0 ? a % b : 0);

    // Для вектора і скаляра float
    public static VectorFloat operator +(VectorFloat v, float s) => ApplyScalar(v, a => a + s);
    public static VectorFloat operator -(VectorFloat v, float s) => ApplyScalar(v, a => a - s);
    public static VectorFloat operator *(VectorFloat v, float s) => ApplyScalar(v, a => a * s);
    public static VectorFloat operator /(VectorFloat v, float s) => s != 0 ? ApplyScalar(v, a => a / s) : new VectorFloat(v.num);
    public static VectorFloat operator %(VectorFloat v, float s) => s != 0 ? ApplyScalar(v, a => a % s) : new VectorFloat(v.num);

    // --- ПОБІТОВІ БІНАРНІ (|, ^, &, >>, <<) ---
    // Виконуємо через BitConverter, як вказано в завданні
    public static VectorFloat operator |(VectorFloat v1, VectorFloat v2) => ApplyBitOp(v1, v2, (a, b) => a | b);
    public static VectorFloat operator ^(VectorFloat v1, VectorFloat v2) => ApplyBitOp(v1, v2, (a, b) => a ^ b);
    public static VectorFloat operator &(VectorFloat v1, VectorFloat v2) => ApplyBitOp(v1, v2, (a, b) => a & b);

    // Зі скаляром ubyte (byte в C#)
    public static VectorFloat operator |(VectorFloat v, byte s) => ApplyBitScalar(v, a => a | s);
    public static VectorFloat operator ^(VectorFloat v, byte s) => ApplyBitScalar(v, a => a ^ s);
    public static VectorFloat operator &(VectorFloat v, byte s) => ApplyBitScalar(v, a => a & s);

    // Зсуви зі скаляром uint
    public static VectorFloat operator >>(VectorFloat v, int s) => ApplyBitScalar(v, a => a >> s);
    public static VectorFloat operator <<(VectorFloat v, int s) => ApplyBitScalar(v, a => a << s);

    // --- ПОРІВНЯННЯ (==, !=, >, <, >=, <=) ---
    // Повертають true, якщо умова виконується для кожної пари
    public static bool operator ==(VectorFloat v1, VectorFloat v2)
    {
        if (v1.num != v2.num) return false;
        for (int i = 0; i < v1.num; i++) if (v1.FArray[i] != v2.FArray[i]) return false;
        return true;
    }
    public static bool operator !=(VectorFloat v1, VectorFloat v2) => !(v1 == v2);

    public static bool operator >(VectorFloat v1, VectorFloat v2) => CompareAll(v1, v2, (a, b) => a > b);
    public static bool operator <(VectorFloat v1, VectorFloat v2) => CompareAll(v1, v2, (a, b) => a < b);
    public static bool operator >=(VectorFloat v1, VectorFloat v2) => CompareAll(v1, v2, (a, b) => a >= b);
    public static bool operator <=(VectorFloat v1, VectorFloat v2) => CompareAll(v1, v2, (a, b) => a <= b);

    // --- ДОПОМІЖНІ МЕТОДИ (для скорочення коду) ---
    private static VectorFloat ApplyOp(VectorFloat v1, VectorFloat v2, Func<float, float, float> op)
    {
        uint resSize = Math.Max(v1.num, v2.num);
        VectorFloat res = new VectorFloat(resSize);
        for (int i = 0; i < Math.Min(v1.num, v2.num); i++) res.FArray[i] = op(v1.FArray[i], v2.FArray[i]);
        return res;
    }

    private static VectorFloat ApplyScalar(VectorFloat v, Func<float, float> op)
    {
        VectorFloat res = new VectorFloat(v.num);
        for (int i = 0; i < v.num; i++) res.FArray[i] = op(v.FArray[i]);
        return res;
    }

    private static VectorFloat ApplyBitOp(VectorFloat v1, VectorFloat v2, Func<int, int, int> op)
    {
        uint resSize = Math.Max(v1.num, v2.num);
        VectorFloat res = new VectorFloat(resSize);
        for (int i = 0; i < Math.Min(v1.num, v2.num); i++)
        {
            int a = BitConverter.SingleToInt32Bits(v1.FArray[i]);
            int b = BitConverter.SingleToInt32Bits(v2.FArray[i]);
            res.FArray[i] = BitConverter.Int32BitsToSingle(op(a, b));
        }
        return res;
    }

    private static VectorFloat ApplyBitScalar(VectorFloat v, Func<int, int> op)
    {
        VectorFloat res = new VectorFloat(v.num);
        for (int i = 0; i < v.num; i++)
        {
            int a = BitConverter.SingleToInt32Bits(v.FArray[i]);
            res.FArray[i] = BitConverter.Int32BitsToSingle(op(a));
        }
        return res;
    }

    private static bool CompareAll(VectorFloat v1, VectorFloat v2, Func<float, float, bool> cond)
    {
        if (v1.num != v2.num) return false;
        for (int i = 0; i < v1.num; i++) if (!cond(v1.FArray[i], v2.FArray[i])) return false;
        return true;
    }

    public override bool Equals(object? obj) => obj is VectorFloat v && this == v;
    public override int GetHashCode() => FArray.GetHashCode();
}

// TASK 3 ------------------------------------------------------------------------------------------------------------------------
struct EmployeeStruct
{
    public string FullName;
    public string Position;
    public int BirthYear;
    public decimal Salary;

    public override string ToString() =>
        $"{FullName} | {Position} | {BirthYear} р.н. | {Salary} грн";
}

// TASK 4 -------------------------------------------------------------------------------------------------------------------------
public class FloatMatrix
{
    // ПОЛЯ (захищені)
    protected float[,] FMArray;    // двовимірний масив
    protected uint n, m;           // розміри матриці
    protected int codeError;       // код помилки
    protected static int num_mf;   // кількість матриць

    // КОНСТРУКТОРІ
    public FloatMatrix()
    {
        n = 1; m = 1;
        FMArray = new float[n, m];
        FMArray[0, 0] = 0;
        codeError = 0;
        num_mf++;
    }

    public FloatMatrix(uint n, uint m)
    {
        this.n = n; this.m = m;
        FMArray = new float[n, m];
        codeError = 0;
        num_mf++;
    }

    public FloatMatrix(uint n, uint m, float initValue)
    {
        this.n = n; this.m = m;
        FMArray = new float[n, m];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                FMArray[i, j] = initValue;
        codeError = 0;
        num_mf++;
    }

    ~FloatMatrix() { Console.WriteLine("Матрицю видалено."); }

    // МЕТОДИ
    public void Input()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                Console.Write($"[{i},{j}]: ");
                if (!float.TryParse(Console.ReadLine(), out FMArray[i, j])) codeError = -2;
            }
        }
    }

    public void Display()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++) Console.Write(FMArray[i, j] + "\t");
            Console.WriteLine();
        }
    }

    public void SetValue(float val)
    {
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++) FMArray[i, j] = val;
    }

    public static int GetTotalMatrices() => num_mf;

    public override bool Equals(object? obj)
    {
        // Перевіряємо, чи є obj об'єктом FloatMatrix і чи вони рівні
        return obj is FloatMatrix matrix && this == matrix;
    }

    public override int GetHashCode()
    {
        // Повертаємо хеш-код, наприклад, від масиву
        return FMArray.GetHashCode();
    }

    // ВЛАСТИВОСТІ
    public string Size => $"{n}x{m}";
    public int ErrorCode { get => codeError; set => codeError = value; }

    // ІНДЕКСАТОРИ
    public float this[int i, int j]
    {
        get
        {
            if (i < 0 || i >= n || j < 0 || j >= m) { codeError = -1; return 0; }
            return FMArray[i, j];
        }
        set
        {
            if (i < 0 || i >= n || j < 0 || j >= m) codeError = -1;
            else FMArray[i, j] = value;
        }
    }

    // Індексатор k = i * m + j
    public float this[int k]
    {
        get
        {
            int i = k / (int)m;
            int j = k % (int)m;
            return this[i, j];
        }
        set
        {
            int i = k / (int)m;
            int j = k % (int)m;
            this[i, j] = value;
        }
    }

    // --- 1. Унарні операції ---

    // ++ та -- : одночасно збільшує/зменшує значення всіх елементів на 1
    public static FloatMatrix operator ++(FloatMatrix a)
    {
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++) a.FMArray[i, j]++;
        return a;
    }

    public static FloatMatrix operator --(FloatMatrix a)
    {
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++) a.FMArray[i, j]--;
        return a;
    }

    // Сталі true і false: true, якщо n, m != 0 та всі елементи матриці != 0
    public static bool operator true(FloatMatrix a)
    {
        if (a.n == 0 || a.m == 0) return false;
        foreach (var x in a.FMArray) if (x == 0) return false;
        return true;
    }

    public static bool operator false(FloatMatrix a)
    {
        if (a.n == 0 || a.m == 0) return true;
        foreach (var x in a.FMArray) if (x == 0) return true;
        return false;
    }

    // Унарна логічна операція ! (заперечення): true, якщо елементи n, m != 0, інакше false
    public static bool operator !(FloatMatrix a) => (a.n != 0 && a.m != 0);

    // Унарна побітова операція ~ : побітове заперечення для кожного елемента
    public static FloatMatrix operator ~(FloatMatrix a)
    {
        FloatMatrix res = new FloatMatrix(a.n, a.m);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
            {
                int bits = BitConverter.SingleToInt32Bits(a.FMArray[i, j]);
                res.FMArray[i, j] = BitConverter.Int32BitsToSingle(~bits);
            }
        return res;
    }

    // --- 2. Арифметичні бінарні операції (+, -, *, /, %) ---
    // Якщо розміри різні — повертаємо першу матрицю

    public static FloatMatrix operator +(FloatMatrix a, FloatMatrix b) => ApplyBinary(a, b, (x, y) => x + y);
    public static FloatMatrix operator +(FloatMatrix a, float s) => ApplyScalar(a, x => x + s);

    public static FloatMatrix operator -(FloatMatrix a, FloatMatrix b) => ApplyBinary(a, b, (x, y) => x - y);
    public static FloatMatrix operator -(FloatMatrix a, float s) => ApplyScalar(a, x => x - s);

    // Множення (*)
    public static FloatMatrix operator *(FloatMatrix a, FloatMatrix b) => ApplyBinary(a, b, (x, y) => x * y); // Поелементно
    public static FloatMatrix operator *(FloatMatrix a, VectorFloat v)
    {
        if (a.m != v.Size) return a; // Перевірка розмірності
        FloatMatrix res = new FloatMatrix(a.n, a.m);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++) res.FMArray[i, j] = a.FMArray[i, j] * v[j];
        return res;
    }
    public static FloatMatrix operator *(FloatMatrix a, float s) => ApplyScalar(a, x => x * s);

    public static FloatMatrix operator /(FloatMatrix a, FloatMatrix b) => ApplyBinary(a, b, (x, y) => x / y);
    public static FloatMatrix operator /(FloatMatrix a, float s) => s != 0 ? ApplyScalar(a, x => x / s) : a;

    public static FloatMatrix operator %(FloatMatrix a, FloatMatrix b) => ApplyBinary(a, b, (x, y) => x % y);
    public static FloatMatrix operator %(FloatMatrix a, float s) => s != 0 ? ApplyScalar(a, x => x % s) : a;

    // --- 3. Побітові бінарні операції (на кодах представлення) ---

    public static FloatMatrix operator |(FloatMatrix a, FloatMatrix b) => ApplyBitwise(a, b, (x, y) => x | y);
    public static FloatMatrix operator |(FloatMatrix a, float s) => ApplyBitScalar(a, s, (x, y) => x | y);

    public static FloatMatrix operator ^(FloatMatrix a, FloatMatrix b) => ApplyBitwise(a, b, (x, y) => x ^ y);
    public static FloatMatrix operator ^(FloatMatrix a, float s) => ApplyBitScalar(a, s, (x, y) => x ^ y);

    // Побітові зсуви (<<, >>) зі скаляром ushort
    public static FloatMatrix operator <<(FloatMatrix a, ushort s)
    {
        FloatMatrix res = new FloatMatrix(a.n, a.m);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
            {
                int bits = BitConverter.SingleToInt32Bits(a.FMArray[i, j]);
                res.FMArray[i, j] = BitConverter.Int32BitsToSingle(bits << s);
            }
        return res;
    }

    public static FloatMatrix operator >>(FloatMatrix a, ushort s)
    {
        FloatMatrix res = new FloatMatrix(a.n, a.m);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
            {
                int bits = BitConverter.SingleToInt32Bits(a.FMArray[i, j]);
                res.FMArray[i, j] = BitConverter.Int32BitsToSingle(bits >> s);
            }
        return res;
    }

    // --- 4. Операції рівності та порівняння ---
    // Повертають true, якщо умова виконується для кожної пари

    public static bool operator ==(FloatMatrix a, FloatMatrix b) => CompareAll(a, b, (x, y) => x == y);
    public static bool operator !=(FloatMatrix a, FloatMatrix b) => !CompareAll(a, b, (x, y) => x == y);

    public static bool operator >(FloatMatrix a, FloatMatrix b) => CompareAll(a, b, (x, y) => x > y);
    public static bool operator >=(FloatMatrix a, FloatMatrix b) => CompareAll(a, b, (x, y) => x >= y);
    public static bool operator <(FloatMatrix a, FloatMatrix b) => CompareAll(a, b, (x, y) => x < y);
    public static bool operator <=(FloatMatrix a, FloatMatrix b) => CompareAll(a, b, (x, y) => x <= y);

    // --- Допоміжні приватні методи для чистоти коду ---

    private static FloatMatrix ApplyBinary(FloatMatrix a, FloatMatrix b, Func<float, float, float> op)
    {
        if (a.n != b.n || a.m != b.m) return a;
        FloatMatrix res = new FloatMatrix(a.n, a.m);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++) res.FMArray[i, j] = op(a.FMArray[i, j], b.FMArray[i, j]);
        return res;
    }

    private static FloatMatrix ApplyScalar(FloatMatrix a, Func<float, float> op)
    {
        FloatMatrix res = new FloatMatrix(a.n, a.m);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++) res.FMArray[i, j] = op(a.FMArray[i, j]);
        return res;
    }

    private static FloatMatrix ApplyBitwise(FloatMatrix a, FloatMatrix b, Func<int, int, int> op)
    {
        if (a.n != b.n || a.m != b.m) return a;
        FloatMatrix res = new FloatMatrix(a.n, a.m);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
            {
                int x = BitConverter.SingleToInt32Bits(a.FMArray[i, j]);
                int y = BitConverter.SingleToInt32Bits(b.FMArray[i, j]);
                res.FMArray[i, j] = BitConverter.Int32BitsToSingle(op(x, y));
            }
        return res;
    }

    private static FloatMatrix ApplyBitScalar(FloatMatrix a, float s, Func<int, int, int> op)
    {
        FloatMatrix res = new FloatMatrix(a.n, a.m);
        int sBits = BitConverter.SingleToInt32Bits(s);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
            {
                int aBits = BitConverter.SingleToInt32Bits(a.FMArray[i, j]);
                res.FMArray[i, j] = BitConverter.Int32BitsToSingle(op(aBits, sBits));
            }
        return res;
    }

    private static bool CompareAll(FloatMatrix a, FloatMatrix b, Func<float, float, bool> cond)
    {
        if (a.n != b.n || a.m != b.m) return false;
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
                if (!cond(a.FMArray[i, j], b.FMArray[i, j])) return false;
        return true;
    }
}

class Program
{
    static void task1()
    {
        Trapeze t = new Trapeze(10, 20, 5, 1);
        string i = "-10 20 5 1";
        Trapeze t2 = (Trapeze)i;
        // Перевірка індексатора
        Console.WriteLine($"Основа а через індекс [0]: {t[0]}");

        // Перевірка ++
        t++;
        Console.WriteLine($"Після ++: {(string)t}");

        // Перевірка --
        t--;
        Console.WriteLine($"Після ++: {(string)t}");

        // Перевірка множення на скаляр
        t = t * 2;
        Console.WriteLine($"Після * 2: {(string)t}");

        // Перевірка true/false
        if (t) Console.WriteLine("Трапеція існує!");   
        else Console.WriteLine("Трапеція неіснує!");
        if (t2) Console.WriteLine("Трапеція існує!");
        else Console.WriteLine("Трапеція неіснує!");
    }
    static void task2()
    {
        // 1. ПЕРЕВІРКА ВСІХ КОНСТРУКТОРІВ
        Console.WriteLine("=== 1. Конструктори та статичні поля ===");
        VectorFloat vEmpty = new VectorFloat(); // без параметрів
        VectorFloat vSize = new VectorFloat(3); // з розміром
        VectorFloat vFull = new VectorFloat(3, 5.5f); // розмір + значення

        Console.WriteLine($"Створено векторів (num_vec): {VectorFloat.GetTotalVectors()}");

        // 2. МЕТОДИ ВВОДУ/ВИВОДУ ТА ПРИСВОЄННЯ
        Console.WriteLine("\n=== 2. Методи Input, Display, Assign ===");
        Console.WriteLine("Введіть значення для вектора vSize (3 елементи):");
        vSize.Input();
        Console.Write("vSize: "); vSize.Display();

        vEmpty.AssignValue(10.0f);
        Console.Write("vEmpty (після AssignValue 10): "); vEmpty.Display();

        // 3. ВЛАСТИВОСТІ ТА ІНДЕКСАТОР
        Console.WriteLine("\n=== 3. Властивості та Індексатор ===");
        Console.WriteLine($"Розмір vFull: {vFull.Size}");
        Console.Write("vFull: "); vFull.Display();
        vFull[0] = 99.9f; // Запис через індексатор
        Console.Write("vFull: "); vFull.Display();
        Console.WriteLine($"vFull[0] після запису: {vFull[0]}");

        // Тест помилки індексатора
        float dummy = vFull[100];
        Console.WriteLine($"Звернення до vFull[100]. Код помилки: {vFull.ErrorCode}");

        // 4. УНАРНІ ОПЕРАТОРИ
        Console.WriteLine("\n=== 4. Унарні оператори (++, --, !, ~) ===");
        vFull++;
        Console.Write("vFull++: "); vFull.Display();

        Console.WriteLine($"Оператор ! (vFull): {!vFull}");

        VectorFloat vNot = ~vFull; // Побітова інверсія float через BitConverter
        Console.Write("~vFull (інверсія бітів): "); vNot.Display();

        // 5. АРИФМЕТИКА (Вектор та Скаляр)
        Console.WriteLine("\n=== 5. Арифметика (Вектор + Скаляр float) ===");
        Console.Write("vFull + 10.0: ");(vFull + 10.0f).Display();
        Console.Write("vFull * 2.0: "); (vFull * 2.0f).Display();
        Console.Write("vFull / 2.0: "); (vFull / 2.0f).Display();
        Console.Write("vFull % 2.0: "); (vFull % 2.0f).Display();
        // 6. АРИФМЕТИКА (Вектор та Вектор)
        Console.WriteLine("\n=== 6. Арифметика (Вектор + Вектор) ===");
        VectorFloat v1 = new VectorFloat(2, 20.0f);
        VectorFloat v2 = new VectorFloat(2, 5.0f);
        Console.Write("v1: "); v1.Display();
        Console.Write("v2: "); v2.Display();
        Console.Write("v1 + v2: "); (v1 + v2).Display();
        Console.Write("v1 - v2: "); (v1 - v2).Display();
        Console.Write("v1 * v2: "); (v1 * v2).Display();
        Console.Write("v1 / v2: "); (v1 / v2).Display();
        Console.Write("v1 % v2: "); (v1 % v2).Display();

        // 7. ПОБІТОВІ БІНАРНІ ОПЕРАЦІЇ
        Console.WriteLine("\n=== 7. Побітові операції (|, ^, &, <<, >>) ===");
        // Вектор і Вектор
        Console.Write("v1 & v2: "); (v1 & v2).Display();
        // Вектор і скаляри ubyte(byte) та uint
        byte scalarByte = 0b00001111;
        uint scalarShift = 2;
        Console.Write("v1 | ubyte: "); (v1 | scalarByte).Display();
        Console.Write("v1 ^ ubyte: "); (v1 ^ scalarByte).Display();
        Console.Write("v1 << uint: "); (v1 << (int)scalarShift).Display();
        Console.Write("v1 >> uint: "); (v1 >> (int)scalarShift).Display();

        // 8. ПОРІВНЯННЯ
        Console.WriteLine("\n=== 8. Операції порівняння ===");
        VectorFloat va = new VectorFloat(2, 10.0f);
        VectorFloat vb = new VectorFloat(2, 10.0f);
        VectorFloat vc = new VectorFloat(2, 5.0f);

        Console.WriteLine($"va == vb: {va == vb}");
        Console.WriteLine($"va != vc: {va != vc}");
        Console.WriteLine($"va > vc: {va > vc}");
        Console.WriteLine($"vc < va: {vc < va}");
        Console.WriteLine($"va >= vb: {va >= vb}");
        Console.WriteLine($"vc <= va: {vc <= va}");

        // 9. TRUE / FALSE
        Console.WriteLine("\n=== 9. Перевірка на true/false ===");
        if (vFull) Console.WriteLine("vFull містить ненульові елементи (true)");
        VectorFloat vZero = new VectorFloat(3, 0.0f);
        if (!vZero) Console.WriteLine("vZero містить лише нулі або порожній (false/!)");

        Console.WriteLine("\n=== Тестування завершено. Натисніть клавішу... ===");
        Console.ReadKey();
    }
    static void task3()
    {
        List<EmployeeStruct> employees = new List<EmployeeStruct>();

        // Введення даних
        employees.Add(new EmployeeStruct { FullName = "Іванов І.І.", Position = "Інженер", BirthYear = 1990, Salary = 25000 });
        employees.Add(new EmployeeStruct { FullName = "Петров П.П.", Position = "Директор", BirthYear = 1985, Salary = 50000 });
        employees.Add(new EmployeeStruct { FullName = "Сидоров С.С.", Position = "Менеджер", BirthYear = 1995, Salary = 20000 });

        Console.WriteLine("Початковий список:");
        employees.ForEach(e => Console.WriteLine(e));

        // 1. Видалити за прізвищем (наприклад, "Іванов")
        string toRemove = "Іванов";
        employees.RemoveAll(e => e.FullName.StartsWith(toRemove));

        // 2. Додати елемент після елемента із номером 0
        var newEmp = new EmployeeStruct { FullName = "Коваленко О.О.", Position = "Аналітик", BirthYear = 1992, Salary = 30000 };
        employees.Insert(0, newEmp);

        Console.WriteLine("\nПісля редагування:");
        employees.ForEach(e => Console.WriteLine(e));
    }

    static void task4()
    {
        // --- 1. ТЕСТ КОНСТРУКТОРІВ ТА СТАТИЧНИХ ПОЛІВ ---
        Console.WriteLine("=== 1. Конструктори ===");
        FloatMatrix mDefault = new FloatMatrix(); // 1x1
        FloatMatrix mSize = new FloatMatrix(2, 2); // 2x2 (нулі)
        FloatMatrix mFull = new FloatMatrix(2, 2, 5.5f); // 2x2 (заповнена 5.5)

        Console.WriteLine($"Всього створено матриць: {FloatMatrix.GetTotalMatrices()}");
        Console.WriteLine($"Розмір mFull: {mFull.Size}");

        // --- 2. ВВІД / ВИВІД ТА ІНДЕКСАТОРИ ---
        Console.WriteLine("\n=== 2. Ввід/Вивід та Індексатори ===");
        Console.WriteLine("Введіть значення для матриці 2х2:");
        mSize.Input();

        Console.WriteLine("Матриця mSize:");
        mSize.Display();

        // Тест двомірного індексатора [i, j]
        mSize[0, 0] = 10.0f;
        Console.WriteLine($"mSize[0,0] після запису: {mSize[0, 0]}");

        // Тест одномірного індексатора [k] (k = i * m + j)
        mSize[3] = 99.9f; // Останній елемент матриці 2х2
        Console.WriteLine($"mSize[3] (через k) після запису: {mSize[3]}");

        // Тест помилки індексатора
        float val = mSize[5, 5];
        Console.WriteLine($"Звернення до [5,5], код помилки: {mSize.ErrorCode}");

        // --- 3. УНАРНІ ОПЕРАЦІЇ ---
        Console.WriteLine("\n=== 3. Унарні операції (++, --, true, false, !, ~) ===");
        mFull++;
        Console.Write("mFull++ (було 5.5): "); Console.WriteLine(mFull[0, 0]);

        if (mFull) Console.WriteLine("Оператор true: Матриця заповнена ненульовими елементами.");

        Console.WriteLine($"Оператор ! (логічне заперечення розмірності): {!mFull}");

        FloatMatrix mNot = ~mFull;
        Console.WriteLine("~mFull (побітова інверсія):");
        mNot.Display();

        // --- 4. АРИФМЕТИКА (Матриця та Матриця) ---
        Console.WriteLine("\n=== 4. Арифметика (Матриця та Матриця) ===");
        FloatMatrix mA = new FloatMatrix(2, 2, 10.0f);
        FloatMatrix mB = new FloatMatrix(2, 2, 2.0f);

        Console.WriteLine("mA + mB:"); (mA + mB).Display();
        Console.WriteLine("mA * mB (поелементно):"); (mA * mB).Display();
        Console.WriteLine("mA % mB:"); (mA % mB).Display();

        // --- 5. АРИФМЕТИКА (Матриця та Скаляр) ---
        Console.WriteLine("\n=== 5. Арифметика (Матриця та Скаляр float) ===");
        Console.WriteLine("mA * 3.0f:"); (mA * 3.0f).Display();
        Console.WriteLine("mA / 2.0f:"); (mA / 2.0f).Display();

        // --- 6. МНОЖЕННЯ НА ВЕКТОР (VectorFloat) ---
        Console.WriteLine("\n=== 6. Множення на VectorFloat ===");
        VectorFloat vec = new VectorFloat(2, 2.0f); // Вектор з двох елементів [2, 2]
        Console.WriteLine("mA * vec (2.0):");
        (mA * vec).Display();

        // --- 7. ПОБІТОВІ БІНАРНІ ОПЕРАЦІЇ ---
        Console.WriteLine("\n=== 7. Побітові операції (|, ^, <<, >>) ===");
        Console.WriteLine("mA | mB (побітове):"); (mA | mB).Display();

        ushort shift = 1;
        Console.WriteLine($"mA << {shift}:"); (mA << shift).Display();

        // --- 8. ПОРІВНЯННЯ ---
        Console.WriteLine("\n=== 8. Порівняння (для кожної пари) ===");
        FloatMatrix mComp1 = new FloatMatrix(2, 2, 10.0f);
        FloatMatrix mComp2 = new FloatMatrix(2, 2, 5.0f);

        Console.WriteLine($"mComp1 == mComp1: {mComp1 == mComp2}");
        Console.WriteLine($"mComp1 > mComp2: {mComp1 > mComp2}"); // True, бо 10 > 5 всюди
        Console.WriteLine($"mComp1 < mComp2: {mComp1 < mComp2}"); // False

        Console.WriteLine("\nТестування завершено. Натисніть клавішу...");
        Console.ReadKey();
    }
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        bool kR = true;

        while (kR)
        {
            Console.WriteLine("========= ГОЛОВНЕ МЕНЮ =========");
            Console.WriteLine("1. Завдання 1: Доповнення до класу з 3 лаби");
            Console.WriteLine("2. Завдання 2: Клас масив");
            Console.WriteLine("2. Завдання 3: Структури");
            Console.WriteLine("4. Завдання 4: Клас матриця");
            Console.WriteLine("5. Очистити екран");
            Console.WriteLine("0. Вихід");

            string choice = Console.ReadLine() ?? "0";
            switch (choice)
            {
                case "1":
                    task1();
                    break;
                case "2":
                    task2();
                    break;
                case "3":
                    task3();
                    break;
                case "4":
                    task4();
                    break;
                case "5":
                    Console.Clear();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Помилка: невірний вибір. Спробуйте ще раз.");
                    break;
            }

        }
    }
}