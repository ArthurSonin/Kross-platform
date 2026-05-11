using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// TASK 1 =====================================================================================================
class PostfixToPrefix
{
    // Метод для перевірки, чи є символ оператором
    static bool IsOperator(char c)
    {
        switch (c)
        {
            case '+':
            case '-':
            case '*':
            case '/':
            case '^':
                return true;
        }
        return false;
    }


    public static string Convert(string postfix)
    {
        Stack<string> stack = new Stack<string>();

        // Проходимо по кожному символу постфіксного виразу
        for (int i = 0; i < postfix.Length; i++)
        {
            char c = postfix[i];

            // Пропускаємо пробіли, якщо вони є
            if (c == ' ') continue;

            if (IsOperator(c))
            {
                // Якщо оператор — дістаємо два операнди
                // Важливо: перший виштовхнутий — це другий операнд
                string op1 = stack.Pop();
                string op2 = stack.Pop();

                // Формуємо префіксний запис: оператор + операнд2 + операнд1
                string temp = c + op2 + op1;

                // Кладемо результат назад у стек
                stack.Push(temp);
            }
            else
            {
                // Якщо операнд — кладемо у стек як рядок
                stack.Push(c.ToString());
            }
        }

        // В кінці у стеку залишиться один елемент — повний префіксний вираз
        return stack.Pop();
    }
}


// TASK 2 =====================================================================================================

class Employee
{
    public string? FullName { get; set; }
    public string? Gender { get; set; }
    public int Age { get; set; }
    public double Salary { get; set; }

    public override string ToString()
    {
        return $"{FullName}, Стать: {Gender}, Вік: {Age}, Зарплата: {Salary:F2}";
    }
}

// TASK 3.1 ====================================================================================================
class ExpressionConverter : ICloneable
{
    public string Postfix { get; set; }

    public ExpressionConverter(string postfix)
    {
        Postfix = postfix;
    }

    // Реалізація ICloneable
    public object Clone()
    {
        return new ExpressionConverter(this.Postfix);
    }

    public string Convert()
    {
        ArrayList stack = new ArrayList();

        foreach (char c in Postfix)
        {
            if (c == ' ') continue;

            if ("+-*/^".Contains(c))
            {
                // імітація Pop
                string op1 = (string)stack[stack.Count - 1]!;
                stack.RemoveAt(stack.Count - 1);

                string op2 = (string)stack[stack.Count - 1]!;
                stack.RemoveAt(stack.Count - 1);

                stack.Add(c + op2 + op1);
            }
            else
            {
                stack.Add(c.ToString());
            }
        }
        return (string)stack[0]!;
    }
}

// Реалізація IComparer для порівняння двох виразів за довжиною
class ExpressionLengthComparer : IComparer
{
    public int Compare(object? x, object? y)
    {
        string s1 = x as string ?? "";
        string s2 = y as string ?? "";
        return s1.Length.CompareTo(s2.Length);
    }
}

// TASK 3.2 ====================================================================================================

// 1. Клас Employee з реалізацією ICloneable
class EmployeeTask3 : ICloneable
{
    public string? FullName { get; set; }
    public string? Gender { get; set; }
    public int Age { get; set; }
    public double Salary { get; set; }

    // Реалізація ICloneable
    public object Clone()
    {
        // Повертаємо новий об'єкт з такими ж даними
        return new EmployeeTask3
        {
            FullName = this.FullName,
            Gender = this.Gender,
            Age = this.Age,
            Salary = this.Salary
        };
    }

    public override string ToString()
    {
        return $"{FullName}, {Gender}, {Age} років, Зарплата: {Salary:F2}";
    }
}

// 2. Реалізація IComparer для сортування за зарплатою (від меншої до більшої)
class SalaryComparer : IComparer
{
    public int Compare(object? x, object? y)
    {
        EmployeeTask3? e1 = x as EmployeeTask3;
        EmployeeTask3? e2 = y as EmployeeTask3;

        if (e1 == null || e2 == null) return 0;

        return e1.Salary.CompareTo(e2.Salary);
    }
}

// TASK 4 =====================================================================================================

class Song
{
    public string Title { get; set; }
    public string Artist { get; set; }

    public Song(string title, string artist)
    {
        Title = title;
        Artist = artist;
    }

    public override string ToString() => $"{Artist} - {Title}";
}

// Клас для музичного диска
class MusicDisk
{
    public string DiskName { get; set; }
    public List<Song> Songs { get; set; } = new List<Song>();

    public MusicDisk(string name)
    {
        DiskName = name;
    }

    public void AddSong(string title, string artist)
    {
        Songs.Add(new Song(title, artist));
    }
}

class Task4
{
    // Наш каталог на базі Hashtable
    // інтерфейс IDictionary дозволяє працювати з ним як з колекцією ключ-значення
    // Ключ - назва диска, Значення - об'єкт MusicDisk
    private static Hashtable catalog = new Hashtable();

    public static void AddDisk()
    {
        Console.Write("Введіть назву нового диска: ");
        string name = Console.ReadLine() ?? "0";
        if (!catalog.ContainsKey(name))
        {
            catalog.Add(name, new MusicDisk(name));
            Console.WriteLine("Диск додано.");
        }
        else Console.WriteLine("Такий диск вже існує.");
    }

    public static void RemoveDisk()
    {
        Console.Write("Назва диска для видалення: ");
        string name = Console.ReadLine() ?? "0";
        if (catalog.ContainsKey(name))
        {
            catalog.Remove(name);
            Console.WriteLine("Диск видалено.");
        }
        else Console.WriteLine("Диск не знайдено.");
    }

    public static void AddSongToDisk()
    {
        Console.Write("Назва диска: ");
        string diskName = Console.ReadLine() ?? "0";
        if (catalog.ContainsKey(diskName))
        {
            MusicDisk disk = (MusicDisk)catalog[diskName]!;
            Console.Write("Назва пісні: ");
            string title = Console.ReadLine() ?? "0";
            Console.Write("Виконавець: ");
            string artist = Console.ReadLine() ?? "0";
            disk.AddSong(title, artist);
            Console.WriteLine("Пісню додано.");
        }
        else Console.WriteLine("Диск не знайдено.");
    }

    public static void RemoveSongFromDisk()
    {
        Console.Write("Назва диска: ");
        string diskName = Console.ReadLine() ?? "0";
        if (catalog.ContainsKey(diskName))
        {
            MusicDisk disk = (MusicDisk)catalog[diskName]!;
            Console.Write("Назва пісні для видалення: ");
            string title = Console.ReadLine() ?? "0";
            disk.Songs.RemoveAll(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine("Пісню видалено (якщо вона була).");
        }
    }

    public static void ViewAll()
    {
        if (catalog.Count == 0) { Console.WriteLine("Каталог порожній."); return; }
        foreach (DictionaryEntry entry in catalog)
        {
            Console.WriteLine($"\nДиск: {entry.Key}");
            MusicDisk disk = (MusicDisk)entry.Value!;
            foreach (var song in disk.Songs) Console.WriteLine($"  - {song}");
        }
    }

    public static void ViewDisk()
    {
        Console.Write("Введіть назву диска: ");
        string name = Console.ReadLine() ?? "0";
        if (catalog.ContainsKey(name))
        {
            MusicDisk disk = (MusicDisk)catalog[name]!;
            Console.WriteLine($"Диск: {disk.DiskName}. Кількість пісень: {disk.Songs.Count}");
            foreach (var song in disk.Songs) Console.WriteLine($"  - {song}");
        }
        else Console.WriteLine("Диск не знайдено.");
    }

    public static void SearchByArtist()
    {
        Console.Write("Введіть ім'я виконавця: ");
        string artist = Console.ReadLine() ?? "0";
        bool found = false;
        foreach (DictionaryEntry entry in catalog)
        {
            MusicDisk disk = (MusicDisk)entry.Value!;
            foreach (var song in disk.Songs)
            {
                if (song.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Знайдено на диску '{entry.Key}': {song.Title}");
                    found = true;
                }
            }
        }
        if (!found) Console.WriteLine("Записів цього виконавця не знайдено.");
    }
}

class Program
{
    static void task1()
    {
        // Приклад: "AB+C*" -> "*(+AB)C" (в префіксній формі: *+ABC)
        string postfix = "ABC/-AK/L-*";

        Console.WriteLine($"Постфіксна форма: {postfix}");
        try
        {
            string prefix = PostfixToPrefix.Convert(postfix);
            Console.WriteLine($"Префіксна форма:  {prefix}");
        }
        catch (Exception)
        {
            Console.WriteLine("Помилка: Перевірте коректність постфіксного виразу.");
        }
    }
    static void CreateSampleFile(string path)
    {
        string[] lines = {
            "Іваненко Іван Іванович;Чол;30;12000",
            "Петренко Ганна Степанівна;Жін;25;8500",
            "Сидоренко Олег Петрович;Чол;45;15000",
            "Ковальчук Марія Ігорівна;Жін;22;9200",
            "Бондар Дмитро Васильович;Чол;35;9800"
        };
        File.WriteAllLines(path, lines);
    }
    static void task2()
    {
        string filePath = @"..\..\..\employees.txt";

        // Створюємо тестовий файл, якщо його не існує
        CreateSampleFile(filePath);

        // Черга для співробітників із зарплатою >= 10000
        Queue<Employee> highSalaryQueue = new Queue<Employee>();

        Console.WriteLine("--- Результати обробки файлу ---");

        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length < 4) continue;

                    Employee emp = new Employee
                    {
                        FullName = parts[0],
                        Gender = parts[1],
                        Age = int.Parse(parts[2]),
                        Salary = double.Parse(parts[3])
                    };

                    if (emp.Salary < 10000)
                    {
                        // Друкуємо одразу (ті, хто < 10000 мають бути першими)
                        Console.WriteLine(emp.ToString());
                    }
                    else
                    {
                        // Тимчасово зберігаємо в чергу
                        highSalaryQueue.Enqueue(emp);
                    }
                }
            }

            // Після завершення читання файлу виводимо решту з черги
            while (highSalaryQueue.Count > 0)
            {
                Console.WriteLine(highSalaryQueue.Dequeue().ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }

        Console.WriteLine("--------------------------------");
    }

    static void task3_1()
    {
        Console.WriteLine("ABC/-AK/L-*");
        ExpressionConverter original = new ExpressionConverter("ABC/-AK/L-*");

        // Демонстрація ICloneable
        ExpressionConverter copy = (ExpressionConverter)original.Clone();

        Console.WriteLine($"Перетворення клонованого виразу: {copy.Convert()}");

        // Демонстрація IEnumerable (ArrayList вже його має всередині)
        ArrayList results = new ArrayList { "ABC/-", "*+ABC", "A" };
        Console.WriteLine("\nСписок проміжних результатів (IEnumerable):");
        foreach (var res in results)
        {
            Console.WriteLine(res);
        }

        // Демонстрація IComparer
        results.Sort(new ExpressionLengthComparer());
        Console.WriteLine("\nРезультати, відсортовані за довжиною (IComparer):");
        foreach (var res in results) Console.WriteLine(res);
    }

    static void task3_2()
    {
        string filePath = @"..\..\..\employees.txt";

        // Використовуємо ArrayList замість Queue
        ArrayList highSalaryList = new ArrayList();

        Console.WriteLine("\n--- Обробка через ArrayList та інтерфейси ---");

        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length < 4) continue;

                    EmployeeTask3 emp = new EmployeeTask3
                    {
                        FullName = parts[0],
                        Gender = parts[1],
                        Age = int.Parse(parts[2]),
                        Salary = double.Parse(parts[3])
                    };

                    if (emp.Salary < 10000)
                    {
                        // Виводимо одразу тих, у кого низька зарплата
                        Console.WriteLine($"[Група <10000]: {emp}");
                    }
                    else
                    {
                        // Решту додаємо в ArrayList (емуляція черги)
                        highSalaryList.Add(emp);
                    }
                }
            }

            // 3. Демонстрація IEnumerable через foreach по ArrayList
            Console.WriteLine("\n--- Інші співробітники (збережений порядок): ---");
            foreach (EmployeeTask3 emp in highSalaryList)
            {
                Console.WriteLine($"[Група >=10000]: {emp}");
            }

            // 4. Демонстрація IComparer (сортування списку)
            Console.WriteLine("\n--- Сортування другої групи за зарплатою (IComparer): ---");
            highSalaryList.Sort(new SalaryComparer());

            foreach (EmployeeTask3 emp in highSalaryList)
            {
                Console.WriteLine(emp);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }

        Console.WriteLine("\nНатисніть будь-яку клавішу...");
        Console.ReadKey();
    }

    public static void task4()
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine("\n=== КАТАЛОГ МУЗИЧНИХ ДИСКІВ ===");
            Console.WriteLine("1. Додати диск");
            Console.WriteLine("2. Видалити диск");
            Console.WriteLine("3. Додати пісню на диск");
            Console.WriteLine("4. Видалити пісню з диска");
            Console.WriteLine("5. Переглянути весь каталог");
            Console.WriteLine("6. Переглянути диск");
            Console.WriteLine("7. Пошук за виконавцем");
            Console.WriteLine("0. Назад у головне меню");
            Console.Write("Вибір: ");

            string choice = Console.ReadLine() ?? "0";
            switch (choice)
            {
                case "1": Task4.AddDisk(); break;
                case "2": Task4.RemoveDisk(); break;
                case "3": Task4.AddSongToDisk(); break;
                case "4": Task4.RemoveSongFromDisk(); break;
                case "5": Task4.ViewAll(); break;
                case "6": Task4.ViewDisk(); break;
                case "7": Task4.SearchByArtist(); break;
                case "0": back = true; break;
                default: Console.WriteLine("Невірний вибір!"); break;
            }
        }
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        int e;
        do
        {
            Console.WriteLine("Введіть номер завдання (1-3):");
            Console.WriteLine("1. Завдання 1");
            Console.WriteLine("2. Завдання 2");
            Console.WriteLine("3. Завдання 3.1");
            Console.WriteLine("4. Завдання 3.2");
            Console.WriteLine("5. Завдання 4");
            Console.WriteLine("6. Вихід");
            e = int.Parse(Console.ReadLine() ?? "0");
            switch (e)
            {
                case 1: task1(); break;
                case 2: task2(); break;
                case 3: task3_1(); break;
                case 4: task3_2(); break;
                case 5: task4(); break;
                case 6: break;
                default: Console.WriteLine("Невірний номер завдання."); break;
            }
        } while (e != 6);
    }
}

