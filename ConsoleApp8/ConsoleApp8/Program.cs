using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

class Program
{
    static void task1()
    {
        string inputFilePath = @"D:\універ\2 куркурс\kross_platform\ConsoleApp8\ConsoleApp8\input1.txt";
        string outputFilePath = @"D:\універ\2 куркурс\kross_platform\ConsoleApp8\ConsoleApp8\found_ips1.txt";
        string processedFilePath = @"D:\універ\2 куркурс\kross_platform\ConsoleApp8\ConsoleApp8\processed_text1.txt";

        // Створимо тестовий файл, якщо його не існує
        if (!File.Exists(inputFilePath))
        {
            File.WriteAllText(inputFilePath, "Сервер 1: 192.168.0.1, Сервер 2: 127.0.0.1. Помилка на 256.0.0.1 (невірна).");
        }

        try
        {
            string content = File.ReadAllText(inputFilePath);

            // 1. Пошук IP-адрес (регулярний вираз для d.d.d.d у діапазоні 0-255)
            // Вираз перевіряє, щоб кожне число було від 0 до 255
            string pattern = @"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
            // \b - межа слова, (?:...) - група без збереження, {3} - повторити 3 рази, \. - крапка
            // @ - \ сприймається як звичайний символ, а не спеціальний
            // 25[0-5] - числа від 250 до 255,
            // 2[0-4][0-9] - числа від 200 до 249,
            // [01]?[0-9][0-9]? - числа від 0 до 199
            // [01]? - може бути 0 або 1, але не обов'язково, тому дозволяє числа від 0 до 199
            // [0-9]? - може бути будь-яка цифра від 0 до 9, але не обов'язково
            // | - логічне "або" для вибору між різними варіантами чисел
            // \.) - тут нема ?, бо крапка обов'язкова між числами

            MatchCollection matches = Regex.Matches(content, pattern);
            List<string> foundIps = matches.Cast<Match>().Select(m => m.Value).ToList();
            // MatchCollection - це колекція об'єктів Match, які містять інформацію про кожен знайдений збіг.
            // Regex Matches повертає колекцію збігів, ми перетворюємо її в список рядків з допомогою LINQ
            // LINQ - це мова запитів, яка дозволяє легко працювати з колекціями даних.

            // Cast<Match>() - перетворює кожен елемент MatchCollection в тип Match, що дозволяє використовувати LINQ.

            // 2. Підрахунок кількості
            Console.WriteLine($"Знайдено IP-адрес: {foundIps.Count}");
            foreach (var ip in foundIps)
            {
                Console.WriteLine($"- {ip}");
            }

            // Запис знайдених адрес у новий файл
            File.WriteAllLines(outputFilePath, foundIps);
            Console.WriteLine($"\nСписок IP збережено у: {outputFilePath}");

            // 3. Пошук із заміною або видаленням
            Console.WriteLine("\nВведіть IP-адресу, яку потрібно замінити:");
            string targetIp = Console.ReadLine() ?? "0.0.0.0";

            if (foundIps.Contains(targetIp))
            {
                Console.WriteLine("Введіть нову IP-адресу (або залиште порожнім, щоб просто видалити):");
                string replacementIp = Console.ReadLine() ?? "0.0.0.0";

                // Заміна або видалення (якщо replacementIp порожній)
                string newContent = content.Replace(targetIp, replacementIp);

                File.WriteAllText(processedFilePath, newContent);
                Console.WriteLine($"Результат обробки збережено у: {processedFilePath}");
            }
            else
            {
                Console.WriteLine("Таку адресу не знайдено в тексті.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }

        Console.WriteLine("\nРоботу завершено. Натисніть будь-яку клавішу...");
    }

    static void task2()
    {
        string inputPath = @"D:\універ\2 куркурс\kross_platform\ConsoleApp8\ConsoleApp8\input2.txt";
        string resultPath = @"D:\універ\2 куркурс\kross_platform\ConsoleApp8\ConsoleApp8\words_result2.txt";
        string editedPath = @"D:\універ\2 куркурс\kross_platform\ConsoleApp8\ConsoleApp8\text_after_replacement2.txt";

        try
        {
            if (!File.Exists(inputPath))
            {
                File.WriteAllText(inputPath, "Це тестовий текст. Сонце світить яскраво, небо синє.");
            }

            string text = File.ReadAllText(inputPath);
            Console.WriteLine("Вміст файлу прочитано.");

            // 1. Пошук слів заданої довжини
            Console.Write("Введіть довжину слова для пошуку (n): ");
            if (!int.TryParse(Console.ReadLine(), out int n))
            //  ! для перевірки успішності конвертації рядка в ціле число. Якщо користувач введе нечислове значення, TryParse поверне false, і ми зможемо обробити цю помилку.
            {
                Console.WriteLine("Помилка: введіть ціле число.");
                return;
            }

            // Регулярний вираз для пошуку слів точною довжиною n
            // \b - межа слова, \w{n} - рівно n букв/цифр
            string pattern = $@"\b\w{{{n}}}\b";
            // $ - для вставки значення змінної n у рядок шаблону.
            // @ - для того шоб писати \ а не \\
            // \b - означає "межа слова". Це гарантує, що ми знайдемо тільки слова, які мають точну довжину n, а не частини других слов.
            // \w{{{n}}} - шукає послідовність з n букв або цифр. Подвійні фігурні дужки {{ і }} використовуються для того,
            // щоб вставити фігурні дужки в рядок шаблону, оскільки одинарні { і } мають спеціальне значення в форматуванні рядків.
            // Це треба щоб в память прийшло значення в {n} як частина регулярного виразу, а не як форматування рядка.
            MatchCollection matches = Regex.Matches(text, pattern);

            List<string> foundWords = matches.Cast<Match>().Select(m => m.Value).ToList();

            // 2. Підрахунок кількості
            Console.WriteLine($"Знайдено слів довжиною {n}: {foundWords.Count}");
            foreach (var word in foundWords.Distinct())
            // Distinct() - для виведення унікальних слів, щоб не повторювати одне і те саме слово кілька разів,
            // якщо воно зустрічається більше одного разу.
            {
                Console.WriteLine($"- {word}");
            }

            // Запис результату пошуку у новий файл
            File.WriteAllLines(resultPath, foundWords);
            Console.WriteLine($"\nСписок знайдених слів збережено у: {resultPath}");

            // 3. Пошук із заміною
            if (foundWords.Count > 0)
            {
                Console.Write("\nВведіть слово з перелічених вище, яке хочете замінити: ");
                string targetWord = Console.ReadLine() ?? "0";

                if (foundWords.Contains(targetWord))
                {
                    Console.Write("Введіть нове значення: ");
                    string replacement = Console.ReadLine() ?? "0";

                    // Заміна (використовуємо Regex, щоб замінити саме окреме слово, а не частину іншого слова)
                    string updatedText = Regex.Replace(text, $@"\b{targetWord}\b", replacement);

                    File.WriteAllText(editedPath, updatedText);
                    Console.WriteLine($"Змінений текст збережено у: {editedPath}");
                }
                else
                {
                    Console.WriteLine("Такого слова немає у списку знайдених.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Сталася помилка: {ex.Message}");
        }

        Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
    }

    static void task3()
    {
        string inputPath = @"D:\універ\2 куркурс\kross_platform\ConsoleApp8\ConsoleApp8\input3.txt";
        string resultPath = @"D:\універ\2 куркурс\kross_platform\ConsoleApp8\ConsoleApp8\resultPath3.txt";
        string editedPath = @"D:\універ\2 куркурс\kross_platform\ConsoleApp8\ConsoleApp8\editedPath3.txt";

        try
        {
            // Перевірка наявності файлу
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("Файл input.txt не знайдено! Створюю зразок...");
                File.WriteAllText(inputPath, "Один два три чотири п'ять шість сім вісім.");
            }

            string text = File.ReadAllText(inputPath);

            // 1. Пошук слів непарної довжини
            // Використовуємо Regex: \b - межа слова, \w - буква
            // Знаходимо всі слова, а потім фільтруємо за довжиною
            // \b\w+\b - знайде всі слова, а потім ми перевіримо їх довжину
            // w+ - означає "одна або більше букв/цифр"
            // Cast<Match>() - перетворює кожен елемент MatchCollection в тип Match, що дозволяє використовувати LINQ.
            var allWords = Regex.Matches(text, @"\b\w+\b")
                                .Cast<Match>()
                                .Select(m => m.Value)
                                .ToList();

            List<string> oddWords = allWords.Where(w => w.Length % 2 != 0).ToList();

            // 2. Підрахунок кількості
            Console.WriteLine($"Усього слів у тексті: {allWords.Count}");
            Console.WriteLine($"Знайдено слів непарної довжини: {oddWords.Count}");

            foreach (var word in oddWords.Distinct())
            {
                Console.WriteLine($"- {word} (довжина: {word.Length})");
            }

            // Записуємо список знайдених непарних слів у файл
            File.WriteAllLines(resultPath, oddWords);
            Console.WriteLine($"\nСписок непарних слів збережено у: {resultPath}");

            // 3. Вилучення непарних слів
            // Використовуємо Regex.Replace з функцією перевірки довжини
            // Regex.Replace з функцією заміни, яка приймає Match і повертає заміну.
            // Якщо слово має непарну довжину, повертаємо порожній рядок, інакше залишаємо слово.
            string cleanedText = Regex.Replace(text, @"\b\w+\b", m => 
            {
                return m.Value.Length % 2 != 0 ? "" : m.Value;
            });//                               true   false

            // Видаляємо зайві пробіли, що залишилися після вилучення слів
            cleanedText = Regex.Replace(cleanedText, @"\s+", " ").Trim();
            // \s+ - шукає послідовність з одного або більше пробілів, замінюючи їх на один пробіл.
            // Trim() - видаляє пробіли на початку та в кінці рядка.
            // " " - кажа на шо заміняти

            File.WriteAllText(editedPath, cleanedText);
            Console.WriteLine($"Текст без непарних слів збережено у: {editedPath}");

            // Додатково: Пошук із заміною (згідно з загальними вимогами)
            Console.WriteLine("\nБажаєте замінити певне слово на нове? (так/ні)");
            if (Console.ReadLine()?.ToLower() == "так")
            {
                Console.Write("Яке слово замінити: ");
                string target = Console.ReadLine() ?? "0";

                Console.Write("На яке слово: ");
                string replacement = Console.ReadLine() ?? "0";

                // \b означає "межа слова". 
                // Це гарантує, що "дає" буде знайдено тільки як окреме слово,
                // і не зачепить слово "подає".
                string pattern = $@"\b{Regex.Escape(target)}\b";
                // Regex.Escape використовується для того, щоб екранувати будь-які спеціальні символи в target,
                // які можуть бути інтерпретовані як частина регулярного виразу.

                string finalEdit = Regex.Replace(text, pattern, replacement);
                File.WriteAllText(@"..\..\..\manual_replace3.txt", finalEdit);
                Console.WriteLine("Результат ручної заміни збережено у manual_replace3.txt");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }

        Console.WriteLine("\nГотово! Натисніть клавішу для виходу.");
        Console.ReadKey();
    }

    static void task4()
    {
        // Шляхи до файлів
        string binaryFilePath = @"..\..\..\data.bin";
        string resultFilePath = @"..\..\..\binary_results.txt";

        try
        {
            // 1. Створення двійкового файлу
            string[] initialWords = { "Садок", "вишневий", "коло", "хати", "хрущі", "над", "вишнями", "гудуть" };

            // using - це конструкція, яка забезпечує автоматичне звільнення ресурсів після використання.
            // У цьому випадку, коли ми закінчуємо запис у файл, BinaryWriter буде автоматично закритий і звільнить всі ресурси, пов'язані з файлом.
            using (BinaryWriter writer = new BinaryWriter(File.Open(binaryFilePath, FileMode.Create)))
            // File.Open з FileMode.Create створює новий файл або перезаписує існуючий, а BinaryWriter дозволяє записувати дані у двійковому форматі.
            {
                foreach (string s in initialWords)
                {
                    writer.Write(s); // Записує рядок у префіксному форматі (довжина + символи)
                }
            }
            Console.WriteLine("Двійковий файл успішно створено.");

            // 2. Читання з двійкового файлу та пошук
            List<string> allWords = new List<string>();
            using (BinaryReader reader = new BinaryReader(File.Open(binaryFilePath, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                // BaseStream.Position - поточна позиція в потоці, BaseStream.Length - загальна довжина потока
                // Цикл продовжується, поки ми не досягнемо кінця файлу. Це дозволяє нам послідовно читати всі рядки, які ми записали раніше.
                {
                    allWords.Add(reader.ReadString());
                }
            }

            Console.Write("\nВведіть довжину слова для пошуку: ");
            if (!int.TryParse(Console.ReadLine(), out int targetLength))
            {
                Console.WriteLine("Некоректне число.");
                return;
            }

            // Пошук слів заданої довжини
            var foundWords = allWords.Where(w => w.Length == targetLength).ToList();

            // 3. Підрахунок та виведення на екран
            Console.WriteLine($"\nЗнайдено слів з довжиною {targetLength}: {foundWords.Count}");
            foreach (var word in foundWords)
            {
                Console.WriteLine($"- {word}");
            }

            // Запис результатів у текстовий файл (як вимагає загальне завдання)
            File.WriteAllLines(resultFilePath, foundWords);
            Console.WriteLine($"\nРезультати пошуку збережено у: {resultFilePath}");

            // 4. Пошук із заміною (логіка для двійкового файлу)
            if (foundWords.Count > 0)
            {
                Console.Write("\nЯке слово хочете замінити? ");
                string toReplace = Console.ReadLine() ?? "0";

                if (allWords.Contains(toReplace))
                {
                    Console.Write("Введіть нове слово: ");
                    string replacement = Console.ReadLine() ?? "0";

                    // Оновлюємо список
                    for (int i = 0; i < allWords.Count; i++)
                    {
                        if (allWords[i] == toReplace) allWords[i] = replacement;
                    }

                    // Перезаписуємо двійковий файл новими даними
                    using (BinaryWriter writer = new BinaryWriter(File.Open(binaryFilePath, FileMode.Create)))
                    {
                        foreach (string s in allWords) writer.Write(s);
                    }
                    Console.WriteLine("Двійковий файл оновлено.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }

        Console.WriteLine("\nРоботу завершено. Натисніть клавішу...");
        Console.ReadKey();
    }

    static void task5()
    {
        string studentName = "Сонін_Артур";
        string rootPath = @"D:\ttemp";

        try
        {
            // 1. Створення папок <прізвище>1 і <прізвище>2
            string dir1 = Path.Combine(rootPath, studentName + "1");
            string dir2 = Path.Combine(rootPath, studentName + "2");

            Directory.CreateDirectory(dir1);
            Directory.CreateDirectory(dir2);
            Console.WriteLine("Папки створено.");

            // 2. Створення файлів t1.txt та t2.txt у першій папці
            string t1Path = Path.Combine(dir1, "t1.txt");
            string t2Path = Path.Combine(dir1, "t2.txt");

            File.WriteAllText(t1Path, "Шевченко Степан Іванович, 2001 року народження, місце проживання м. Суми");
            File.WriteAllText(t2Path, "Комар Сергій Федорович, 2000 року народження, місце проживання м. Київ");
            Console.WriteLine("Файли t1.txt та t2.txt створено.");

            // 3. Створення t3.txt у другій папці (зміст t1 + t2)
            string t3Path = Path.Combine(dir2, "t3.txt");
            string contentT1 = File.ReadAllText(t1Path);
            string contentT2 = File.ReadAllText(t2Path);
            File.WriteAllText(t3Path, contentT1 + Environment.NewLine + contentT2);
            Console.WriteLine("Файл t3.txt створено.");

            // 4. Виведення розгорнутої інформації про створені файли
            PrintFileInfo(t1Path);
            PrintFileInfo(t2Path);
            PrintFileInfo(t3Path);

            // 5. Перенесення t2.txt у другу папку
            string t2NewPath = Path.Combine(dir2, "t2.txt");
            if (File.Exists(t2NewPath)) File.Delete(t2NewPath); // Видалити, якщо вже є
            File.Move(t2Path, t2NewPath);
            Console.WriteLine("Файл t2.txt перенесено.");

            // 6. Копіювання t1.txt у другу папку
            string t1CopyPath = Path.Combine(dir2, "t1.txt");
            File.Copy(t1Path, t1CopyPath, true);
            Console.WriteLine("Файл t1.txt скопійовано.");

            // 7. Перейменування папки <прізвище>2 в ALL, та вилучення <прізвище>1
            string allPath = Path.Combine(rootPath, "ALL");
            if (Directory.Exists(allPath)) Directory.Delete(allPath, true);
            Directory.Move(dir2, allPath);

            Directory.Delete(dir1, true);
            Console.WriteLine("Папку 2 перейменовано в ALL. Папку 1 видалено.");

            // 8. Вивести повну інформацію про файли папки ALL
            Console.WriteLine("\n--- Інформація про файли в папці ALL ---");
            string[] allFiles = Directory.GetFiles(allPath);
            foreach (string file in allFiles)
            {
                PrintFileInfo(file);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }

        Console.WriteLine("\nНатисніть будь-яку клавішу...");
        Console.ReadKey();
    }

    static void PrintFileInfo(string path)
    {
        FileInfo info = new FileInfo(path);
        Console.WriteLine($"Файл: {info.Name} | Розмір: {info.Length} байт | Створено: {info.CreationTime}");
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
            Console.WriteLine("3. Завдання 3");
            Console.WriteLine("4. Завдання 4");
            Console.WriteLine("5. Завдання 5");
            Console.WriteLine("6. Вихід");
            e = int.Parse(Console.ReadLine() ?? "0");
            switch (e)
            {
                case 1: task1(); break;
                case 2: task2(); break;
                case 3: task3(); break;
                case 4: task4(); break;
                case 5: task5(); break;
                case 6: break;
                default: Console.WriteLine("Невірний номер завдання."); break;
            }
        } while (e != 6);
    }
}
