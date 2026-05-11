using System;
using System.Collections.Generic;
using System.Linq;

// TASK 1 =========================================================================

// Базовий абстрактний клас
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

// Похідний клас: Службовець
class Employee1 : Persona1
{
    public string Department { get; set; }

    public Employee1(string lastName, int age, string department)
        : base(lastName, age)
    {
        Department = department;
    }

    public override void Show()
    {
        Console.WriteLine($"[Службовець] Прізвище: {LastName}, Вік: {Age}, Відділ: {Department}");
    }
}

// Похідний клас: Робітник
class Worker1 : Persona1
{
    public string Shift { get; set; }

    public Worker1(string lastName, int age, string shift)
        : base(lastName, age)
    {
        Shift = shift;
    }

    public override void Show()
    {
        Console.WriteLine($"[Робітник] Прізвище: {LastName}, Вік: {Age}, Зміна: {Shift}");
    }
}

// Похідний клас: Інженер
class Engineer1 : Persona1
{
    public string Specialization { get; set; }

    public Engineer1(string lastName, int age, string specialization)
        : base(lastName, age)
    {
        Specialization = specialization;
    }

    public override void Show()
    {
        Console.WriteLine($"[Інженер] Прізвище: {LastName}, Вік: {Age}, Спеціалізація: {Specialization}");
    }
}

// TASK 2 =========================================================================


// Базовий абстрактний клас
abstract class Persona2
{
    public string LastName { get; set; }
    public int Age { get; set; }

    // 1. Конструктор без параметрів
    public Persona2()
    {
        LastName = "Unknown";
        Age = 0;
        Console.WriteLine("Викликано: Persona (без параметрів)");
    }

    // 2. Конструктор з одним параметром
    public Persona2(string lastName)
    {
        LastName = lastName;
        Age = 18;
        Console.WriteLine($"Викликано: Persona (прізвище: {LastName})");
    }

    // 3. Конструктор з усіма параметрами
    public Persona2(string lastName, int age)
    {
        LastName = lastName;
        Age = age;
        Console.WriteLine($"Викликано: Persona (повний: {LastName}, {Age})");
    }

    // Деструктор
    ~Persona2()
    {
        Console.WriteLine($"Об'єкт Persona ({LastName}) видалений з пам'яті.");
    }

    public abstract void Show();
}

class Employee2 : Persona2
{
    public string Department { get; set; }

    public Employee2() : base()
    {
        Department = "General";
        Console.WriteLine("Викликано: Employee (без параметрів)");
    }

    public Employee2(string lastName, int age) : base(lastName, age)
    {
        Department = "General";
        Console.WriteLine("Викликано: Employee (базові параметри)");
    }

    public Employee2(string lastName, int age, string dept) : base(lastName, age)
    {
        Department = dept;
        Console.WriteLine("Викликано: Employee (повний)");
    }

    ~Employee2() { Console.WriteLine("Деструктор Employee"); }

    public override void Show() => Console.WriteLine($"[Службовець] {LastName}, {Age}, Відділ: {Department}");
}

class Worker2 : Persona2
{
    public string Shift { get; set; }

    public Worker2() : base() { Shift = "Day"; Console.WriteLine("Викликано: Worker (пусто)"); }

    public Worker2(string lastName) : base(lastName) { Shift = "Day"; Console.WriteLine("Викликано: Worker (прізвище)"); }

    public Worker2(string lastName, int age, string shift) : base(lastName, age)
    {
        Shift = shift;
        Console.WriteLine("Викликано: Worker (повний)");
    }

    ~Worker2() { Console.WriteLine("Деструктор Worker"); }

    public override void Show() => Console.WriteLine($"[Робітник] {LastName}, {Age}, Зміна: {Shift}");
}

class Engineer2 : Persona2
{
    public string Rank { get; set; }

    public Engineer2() : base() { Rank = "Junior"; Console.WriteLine("Викликано: Engineer (пусто)"); }

    public Engineer2(string lastName, int age) : base(lastName, age) { Rank = "Middle"; Console.WriteLine("Викликано: Engineer (вік)"); }

    public Engineer2(string lastName, int age, string rank) : base(lastName, age)
    {
        Rank = rank;
        Console.WriteLine("Викликано: Engineer (повний)");
    }

    ~Engineer2() { Console.WriteLine("Деструктор Engineer"); }

    public override void Show() => Console.WriteLine($"[Інженер] {LastName}, {Age}, Рівень: {Rank}");
}

// TASK 3 =========================================================================

abstract class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int TargetAge { get; set; }

        protected Product(string name, decimal price, int targetAge)
        {
            Name = name;
            Price = price;
            TargetAge = targetAge;
        }

        // Абстрактний метод для виводу інформації
        public abstract void ShowInfo();

        // Метод для перевірки типу товару
        public abstract bool IsType(string typeName);
    }

    // Похідний клас: Іграшка
    class Toy : Product
    {
        public string Manufacturer { get; set; }
        public string Material { get; set; }

        public Toy(string name, decimal price, string manufacturer, string material, int age) 
            : base(name, price, age)
        {
            Manufacturer = manufacturer;
            Material = material;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"[Іграшка] Назва: {Name}, Ціна: {Price} грн, Виробник: {Manufacturer}, Матеріал: {Material}, Вік: {TargetAge}+");
        }

        public override bool IsType(string typeName) => typeName.ToLower() == "іграшка";
    }

    // Похідний клас: Книга
    class Book : Product
    {
        public string Author { get; set; }
        public string Publisher { get; set; }

        public Book(string name, string author, decimal price, string publisher, int age) 
            : base(name, price, age)
        {
            Author = author;
            Publisher = publisher;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"[Книга] Назва: '{Name}', Автор: {Author}, Ціна: {Price} грн, Видавництво: {Publisher}, Вік: {TargetAge}+");
        }

        public override bool IsType(string typeName) => typeName.ToLower() == "книга";
    }

    // Похідний клас: Спорт-інвентар
    class SportsEquipment : Product
    {
        public string Manufacturer { get; set; }

        public SportsEquipment(string name, decimal price, string manufacturer, int age) 
            : base(name, price, age)
        {
            Manufacturer = manufacturer;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"[Спорт] Назва: {Name}, Ціна: {Price} грн, Виробник: {Manufacturer}, Вік: {TargetAge}+");
        }

        public override bool IsType(string typeName) => typeName.ToLower() == "спорт" || typeName.ToLower() == "спорт-інвентар";
    }

class Program
{
    static void task1()
    {
        // Створення масиву об'єктів різних типів
        Persona1[] people = new Persona1[]
        {
            new Employee1("Іваненко", 35, "Бухгалтерія"),
            new Employee1("Сидоренко", 28, "HR"),
            new Worker1("Петренко", 40, "Нічна"),
            new Worker1("Василенко", 25, "Денна"),
            new Engineer1("Коваленко", 30, "Програмна інженерія"),
            new Engineer1("Бондар", 45, "Механіка")
        };

        // Впорядкування масиву за полем LastName (базовий клас)
        var sortedPeople = people.OrderBy(p => p.LastName).ToArray();

        Console.WriteLine("Список персоналу (відсортований за прізвищем):");
        Console.WriteLine(new string('-', 60));

        // Виклик методу Show() у циклі
        foreach (var person in sortedPeople)
        {
            person.Show();
        }
    }
    static void task2()
    {
        Console.WriteLine("--- ДЕМОНСТРАЦІЯ КОНСТРУКТОРІВ ---");

        // Створюємо масив, використовуючи різні конструктори
        Persona2[]? staff = new Persona2[]
        {
                new Employee2(),                          // Конструктор 1
                new Employee2("Петров", 30, "IT"),        // Конструктор 3
                new Worker2("Іванов"),                    // Конструктор 2
                new Worker2("Коваль", 45, "Нічна"),       // Конструктор 3
                new Engineer2(),                          // Конструктор 1
                new Engineer2("Сидорчук", 22, "Senior")   // Конструктор 3
        };

        Console.WriteLine("\n--- СПИСОК ОБ'ЄКТІВ ---");
        foreach (var p in staff) p.Show();

        Console.WriteLine("\n--- ОЧИЩЕННЯ ТА ДЕСТРУКТОРИ ---");
        // Обнуляємо масив, щоб об'єкти стали доступні для збирання сміття
        staff = null;

        Console.WriteLine("Програму завершено. Натисніть Enter.");
    }

    static void task3()
    {
        // Створення масиву товарів (бази)
        Product[] catalog = new Product[]
        {
                new Toy("Конструктор LEGO", 1200, "LEGO", "Пластик", 6),
                new Book("Кобзар", "Т. Шевченко", 450, "Книгарня", 12),
                new Toy("М'яка іграшка Ведмедик", 300, "KidsJoy", "Хутро", 3),
                new SportsEquipment("Футбольний м'яч", 800, "Adidas", 8),
                new Book("Гаррі Поттер", "Дж. Роулінг", 600, "Книгарня", 10),
                new SportsEquipment("Гантелі 5кг", 1500, "PowerGym", 16)
        };

        Console.WriteLine("--- ПОВНИЙ КАТАЛОГ ТОВАРІВ ---");
        foreach (var item in catalog)
        {
            item.ShowInfo();
        }

        // Організація пошуку
        Console.Write("\nВведіть тип товару для пошуку (іграшка, книга, спорт): ");
        string searchType = Console.ReadLine() ?? "";

        Console.WriteLine($"\nРезультати пошуку для '{searchType}':");
        bool found = false;
        foreach (var item in catalog)
        {
            if (item.IsType(searchType))
            {
                item.ShowInfo();
                found = true;
            }
        }

        if (!found) Console.WriteLine("Товарів такого типу не знайдено.");

        Console.WriteLine("\nНатисніть Enter для виходу...");
    }

    static void task4()
    {
        // Створюємо об'єкт
        Trapeze? myTrap = new Trapeze(10, 20, 5, 1);

        Console.WriteLine("Початкові дані:");
        myTrap.Show();

        if (myTrap) Console.WriteLine("Трапеція коректна.");

        myTrap++; // Збільшуємо основи
        Console.WriteLine("Після операції ++:");
        myTrap.Show();

        myTrap[1] = 30;

        Console.WriteLine($"myTrap[1]: {myTrap[1]}");

        // Очищення для демонстрації
        myTrap = null;

        Console.WriteLine("\nНатисніть Enter для виходу...");
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
                case 2: task2(); GC.Collect(); GC.WaitForPendingFinalizers(); break;
                case 3: task3(); break;
                case 4: task4(); break;
                case 5: break;
            }
        } while (e != 5);
            
    }
}

