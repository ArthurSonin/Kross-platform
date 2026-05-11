using System;
using System.Collections; 
using System.Collections.Generic;

// TASK 1 =========================================================================
// Інтерфейс для відображення даних
interface IShowable
{
    void Show();
}

// Інтерфейс для робочих функцій
interface IWorker
{
    string Occupation { get; set; }
    void Work();
}

// --- 2. БАЗОВИЙ АБСТРАКТНИЙ КЛАС ---

// Реалізує IShowable (наш), IComparable (для сортування) та ICloneable (для копіювання)
abstract class Persona : IShowable, IComparable, ICloneable
{
    public string LastName { get; set; }
    public int Age { get; set; }

    public Persona(string lastName, int age)
    {
        LastName = lastName;
        Age = age;
    }

    // Абстрактний метод, який буде реалізовано в нащадках
    public abstract void Show();

    // Реалізація IComparable (сортування за прізвищем)
    public int CompareTo(object? obj)
    {
        if (obj is Persona other)
            return string.Compare(this.LastName, other.LastName);
        throw new ArgumentException("Об'єкт не є персонажем");
    }

    // Реалізація ICloneable (поверхневе копіювання)
    public object Clone()
    {
        return this.MemberwiseClone();
    }
}

// --- 3. ПОХІДНІ КЛАСИ ---

// Клас Службовець
class Employee : Persona, IWorker
{
    public string Occupation { get; set; }

    public Employee(string name, int age, string occupation) : base(name, age)
    {
        Occupation = occupation;
    }

    public override void Show()
    {
        Console.WriteLine($"[Службовець] Прізвище: {LastName}, Вік: {Age}, Посада: {Occupation}");
    }

    public void Work()
    {
        Console.WriteLine($"{LastName} виконує адміністративні завдання.");
    }
}

// Клас Інженер
class Engineer : Persona, IWorker
{
    public string Occupation { get; set; }
    public string Specialization { get; set; }

    public Engineer(string name, int age, string occupation, string spec) : base(name, age)
    {
        Occupation = occupation;
        Specialization = spec;
    }

    public override void Show()
    {
        Console.WriteLine($"[Інженер] Прізвище: {LastName}, Вік: {Age}, Спец: {Specialization}, Посада: {Occupation}");
    }

    public void Work()
    {
        Console.WriteLine($"{LastName} працює над технічним проектом.");
    }
}

// TASK 2 ========================================================================

interface ITovar : IComparable
{
    string Name { get; set; }
    double Price { get; set; }
    void Show();
    bool IsType(string type); // Метод для пошуку за типом
}

// 2. Похідний клас: Іграшка
class Toy : ITovar
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Manufacturer { get; set; }
    public string Material { get; set; }
    public int TargetAge { get; set; }

    public Toy(string name, double price, string man, string mat, int age)
    {
        Name = name; Price = price; Manufacturer = man; Material = mat; TargetAge = age;
    }

    public void Show()
    {
        Console.WriteLine($"[Іграшка] {Name}, Ціна: {Price} грн, Виробник: {Manufacturer}, Матеріал: {Material}, Вік: {TargetAge}+");
    }

    public bool IsType(string type) => type.ToLower() == "іграшка";

    // Реалізація IComparable
    public int CompareTo(object? obj)
    {
        if (obj is ITovar other) return Price.CompareTo(other.Price);
        return 0;
    }
}

// 3. Похідний клас: Книга
class Book : ITovar
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int TargetAge { get; set; }

    public Book(string name, string author, double price, string pub, int age)
    {
        Name = name; Author = author; Price = price; Publisher = pub; TargetAge = age;
    }

    public void Show() 
    {
        Console.WriteLine($"[Книга] \"{Name}\", Автор: {Author}, Ціна: {Price} грн, Видавництво: {Publisher}, Вік: {TargetAge}+");
    }

    public bool IsType(string type) => type.ToLower() == "книга";

    public int CompareTo(object? obj)
    {
        if (obj is ITovar other) return Price.CompareTo(other.Price);
        return 0;
    }
}

// 4. Похідний клас: Спорт-інвентар
class SportInventory : ITovar
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Manufacturer { get; set; }
    public int TargetAge { get; set; }

    public SportInventory(string name, double price, string man, int age)
    {
        Name = name; Price = price; Manufacturer = man; TargetAge = age;
    }

    public void Show()
    {
        Console.WriteLine($"[Спорт] {Name}, Ціна: {Price} грн, Виробник: {Manufacturer}, Вік: {TargetAge}+");
    }

    public bool IsType(string type) => type.ToLower() == "спорт";

    public int CompareTo(object? obj)
    {
        if (obj is ITovar other) return Price.CompareTo(other.Price);
        return 0;
    }
}

// TASK 3 ========================================================================

public class TovarException : Exception
{
    public TovarException(string message) : base(message) { }
}

// TASK 4 ========================================================================

abstract class Persona1
{
    public string LastName { get; set; }
    public int Age { get; set; }

    protected Persona1(string lastName, int age)
    {
        LastName = lastName;
        Age = age;
    }

    public abstract void Show();
}

class Employee1 : Persona1
{
    public string Department { get; set; }
    public Employee1(string lastName, int age, string department) : base(lastName, age) { Department = department; }
    public override void Show() => Console.WriteLine($"[Службовець] {LastName}, Вік: {Age}, Відділ: {Department}");
}

class Worker1 : Persona1
{
    public string Shift { get; set; }
    public Worker1(string lastName, int age, string shift) : base(lastName, age) { Shift = shift; }
    public override void Show() => Console.WriteLine($"[Робітник] {LastName}, Вік: {Age}, Зміна: {Shift}");
}

class StaffCollection : IEnumerable
{
    private Persona1[] staff;

    public StaffCollection(Persona1[] staffArray)
    {
        staff = staffArray;
    }

    // Реалізація інтерфейсу IEnumerable
    public IEnumerator GetEnumerator()
    {
        return staff.GetEnumerator();
    }
}


class Program
{
    static void task1()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Створюємо масив різних об'єктів через базовий тип
        Persona[] staff = new Persona[]
        {
                new Engineer("Ковальчук", 28, "Провідний інженер", "Механіка"),
                new Employee("Авраменко", 35, "Менеджер"),
                new Engineer("Бондар", 24, "Молодший інженер", "IT"),
                new Employee("Дмитрук", 40, "Директор")
        };

        Console.WriteLine("--- Список до сортування ---");
        foreach (var p in staff) p.Show();

        // Сортування (працює завдяки IComparable)
        Array.Sort(staff);

        Console.WriteLine("\n--- Список після сортування (IComparable: за прізвищем) ---");
        foreach (var p in staff) p.Show();

        Console.WriteLine("\n--- Демонстрація IWorker та Work() ---");
        foreach (var p in staff)
        {
            if (p is IWorker worker)
            {
                worker.Work();
            }
        }

        // Демонстрація ICloneable
        Console.WriteLine("\n--- Демонстрація ICloneable ---");
        Engineer original = new Engineer("Тестовий", 20, "Стажер", "Тести");
        Engineer clone = (Engineer)original.Clone();
        clone.LastName = "Клон-Ковальчук";

        Console.Write("Оригінал: "); original.Show();
        Console.Write("Копія: "); clone.Show();

        Console.ReadKey();
    }

    static void task2() {
        // Створюємо масив товарів (базу)
        ITovar[] database = new ITovar[]
        {
                new Toy("LEGO Star Wars", 1500, "LEGO", "Пластик", 9),
                new Book("Кобзар", "Т. Шевченко", 450, "А-ба-ба-га-ла-ма-га", 12),
                new SportInventory("М'яч футбольний", 800, "Adidas", 6),
                new Toy("Лялька Barbie", 1200, "Mattel", "Пластик", 5),
                new Book("C# для професіоналів", "Д. Скіт", 900, "Williams", 18)
        };

        Console.WriteLine("--- ПОВНА БАЗА ТОВАРІВ (відсортована за ціною) ---");
        Array.Sort(database); // Використовує IComparable
        foreach (var item in database) item.Show();

        // Організація пошуку
        Console.Write("\nВведіть тип товару для пошуку (іграшка, книга, спорт): ");
        string searchType = Console.ReadLine() ?? "";

        Console.WriteLine($"\nРезультати пошуку для '{searchType}':");
        bool found = false;
        foreach (var item in database)
        {
            if (item.IsType(searchType))
            {
                item.Show();
                found = true;
            }
        }

        if (!found) Console.WriteLine("Товарів такого типу не знайдено.");

        Console.ReadKey();
    }

    static void task3()
    {
        // Власний виняток ---
        try
        {
            Console.WriteLine("--- Тест 1: Власний виняток ---");
            int price = -100;
            if (price < 0)
                throw new TovarException("Ціна не може бути від'ємною!");
        }
        catch (TovarException ex)
        {
            Console.WriteLine($"Спіймано: {ex.Message}");
        }

        // IndexOutOfRangeException ---
        try
        {
            Console.WriteLine("\n--- Тест 2: (IndexOutOfRangeException) ---");

            // Створюємо масив на 3 елементи
            int[] numbers = { 10, 20, 30 };

            Console.WriteLine("Намагаємося звернутися до елемента з індексом 5...");

            // Помилка: індексу 5 не існує (максимальний індекс — 2)
            int myNumber = numbers[5];

            Console.WriteLine($"Це повідомлення не виведеться: {myNumber}");
        }
        catch (IndexOutOfRangeException ex)
        {
            // Тут ми ловимо помилку виходу за межі масиву
            Console.WriteLine("Помилка: Ви звернулися до індексу, якого не існує в масиві!");
            Console.WriteLine($"Системне повідомлення: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("\nБлок finally: Виконано незалежно від помилок.");
        }

        Console.ReadKey();
    }

    static void task4()
    {
        Persona1[] people = {
            new Employee1("Коваль", 30, "IT"),
            new Worker1("Бондар", 25, "Нічна"),
            new Employee1("Сидорчук", 40, "Бухгалтерія")
        };

        StaffCollection myStaff = new StaffCollection(people);

        Console.WriteLine("Перелік персоналу через foreach:");
        Console.WriteLine("-------------------------------------------");

        foreach (Persona1 p in myStaff)
        {
            p.Show();
        }

        Console.ReadKey();
    }
    static void Main(string[] args)
    {
        // Налаштування для виводу тексту в консоль
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Налаштування для коректного зчитування українських літер з клавіатури
        Console.InputEncoding = System.Text.Encoding.UTF8;

        int e;
        do
        {
            Console.WriteLine("Виберіть завдання");
            Console.WriteLine("1. Завдання 1");
            Console.WriteLine("2. Завдання 2");
            Console.WriteLine("3. Завдання 3");
            Console.WriteLine("4. Завдання 4");
            Console.WriteLine("5. Вихід");
            e = int.Parse(Console.ReadLine() ?? "0");
            switch (e)
            {
                case 1: task1(); break;
                case 2: task2(); break;
                case 3: task3(); break;
                case 4: task4(); break;
                case 5: break;
            }
        } while (e != 5);
    }
}