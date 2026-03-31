using System;
using System.Collections.Generic;
using System.Linq;

// TASK 1 -------------------------------------------------------------------------------
public class Trapeze
{
    // Поля
    private int a, b, h; // основи та висота
    private int c;       // колір

    // Конструктор
    public Trapeze(int baseA, int baseB, int height, int color)
    {
        a = baseA;
        b = baseB;
        h = height;
        c = color;
    }

    // Властивості
    public (int A, int B, int H) Dimensions
    {
        get => (a, b, h);
        set
        {
            a = value.A;
            b = value.B;
            h = value.H;
        }
    }

    public int Color => c; // Тільки для читання

    // Методи
    public void PrintDimensions()
    {
        Console.WriteLine($"Основи: {a}, {b}; Висота: {h}; Колір ID: {c}");
    }

    public double GetPerimeter()
    {
        // корінь з ((|a-b|/2)^2 + h^2) для обчислення довжини бічної сторони
        double side = Math.Sqrt(Math.Pow(Math.Abs(a - b) / 2.0, 2) + Math.Pow(h, 2));
        return a + b + 2 * side;
    }

    public double GetArea()
    {
        return (a + b) * h / 2.0;
    }

    public bool IsSquare => (a == b && b == h);
}

// TASK 2 -------------------------------------------------------------------------------

// 1. Базовий клас
public class Persona
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Persona(string name, int age)
    {
        Name = name;
        Age = age;
    }

    // virtual дозволяє перекривати метод у похідних класах
    public virtual void Show()
    {
        Console.Write($"[Персона] Ім'я: {Name}, Вік: {Age}");
    }
}

// 2. Похідний клас: Службовець
public class Employee : Persona
{
    public string Organization { get; set; }

    public Employee(string name, int age, string org) : base(name, age)
    {
        Organization = org;
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($", Організація: {Organization}");
    }
}

// 3. Похідний клас: Робітник
public class Worker : Persona
{
    public int Rank { get; set; }

    public Worker(string name, int age, int rank) : base(name, age)
    {
        Rank = rank;
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($", Робочий розряд: {Rank}");
    }
}

// 4. Похідний клас: Інженер
public class Engineer : Persona
{
    public string Field { get; set; }

    public Engineer(string name, int age, string field) : base(name, age)
    {
        Field = field;
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($", Спеціальність: {Field}");
    }
}

class Program
{
    static void task1()
    {
        // Створюємо масив трапецій
        List<Trapeze> trapezoids = new List<Trapeze>
        {
            new Trapeze(10, 10, 10, 1), // Квадрат
            new Trapeze(5, 8, 4, 3),
            new Trapeze(12, 6, 5, 2),
            new Trapeze(4, 4, 4, 1),    // Квадрат
            new Trapeze(15, 10, 8, 2)
        };

        // 1. Сортування за кольором
        Console.WriteLine("--- Впорядковано за кольором ---");
        var byColor = trapezoids.OrderBy(t => t.Color);
        PrintList(byColor);

        // 2. Сортування за площею
        Console.WriteLine("\n--- Впорядковано за площею ---");
        var byArea = trapezoids.OrderBy(t => t.GetArea());
        foreach (var t in byArea)
        {
            Console.WriteLine($"Площа: {t.GetArea():F2} | Колір: {t.Color}");
        }

        // 3. Сортування за периметром
        Console.WriteLine("\n--- Впорядковано за периметром ---");
        var byPerimeter = trapezoids.OrderBy(t => t.GetPerimeter());
        foreach (var t in byPerimeter)
        {
            Console.WriteLine($"Периметр: {t.GetPerimeter():F2} | Колір: {t.Color}");
        }

        // 4. Визначення кількості квадратів
        int squareCount = trapezoids.Count(t => t.IsSquare);
        Console.WriteLine($"\nКількість квадратів у масиві: {squareCount}");

        trapezoids[2].Dimensions = (12, 12, 12);

        // 5. Визначення кількості квадратів після зміни розмірів одної трапеції
        Console.WriteLine("\n--- Зміна розмірів одної трапеції ---");

        Console.WriteLine(trapezoids[2].Dimensions);

        int newSquareCount = trapezoids.Count(t => t.IsSquare);
        Console.WriteLine($"Кількість квадратів у масиві після зміни: {newSquareCount} \n");
    }

    static void task2()
    {
        // Створюємо масив базового класу
        List<Persona> people = new List<Persona>();

        // Наповнюємо масив різними об'єктами
        FillArray(people);

        Console.WriteLine("=== Список, впорядкований за типом об'єкта ===\n");

        // Впорядковуємо за типом (спочатку Персони, потім Інженери і т.д.)
        var sortedPeople = people.OrderBy(p => p.GetType().Name);

        foreach (var p in sortedPeople)
        {
            p.Show();
            if (p.GetType() == typeof(Persona))
            {
                Console.WriteLine();
            }
        }
    }

    // Функція для наповнення масиву
    static void FillArray(List<Persona> list)
    {
        list.Add(new Engineer("Коваленко", 35, "Будівництво"));
        list.Add(new Worker("Петренко", 25, 4));
        list.Add(new Employee("Іванов", 40, "ПриватБанк"));
        list.Add(new Persona("Сидоренко", 19));
        list.Add(new Engineer("Зайцев", 28, "IT-архітектор"));
        list.Add(new Worker("Кузьменко", 30, 5));
    }
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        bool kR = true;

        while (kR)
        {
            Console.WriteLine("========= ГОЛОВНЕ МЕНЮ =========");
            Console.WriteLine("1. Завдання 1: Клас масив трапецій");
            Console.WriteLine("2. Завдання 2: Ієрархія класів");
            Console.WriteLine("3. Очистити екран");
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

    static void PrintList(IEnumerable<Trapeze> list)
    {
        foreach (var t in list)
        {
            t.PrintDimensions();
        }
    }
}