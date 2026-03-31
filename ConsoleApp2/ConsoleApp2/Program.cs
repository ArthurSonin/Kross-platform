
namespace ConsoleApp2
{
    class Program
    {
        static void Task1()
        {
            // --- СПОСІБ 1: ОДНОВИМІРНИЙ МАСИВ ---
            Console.WriteLine("=== Спосіб 1: Одновимірний масив ===");
            Console.Write("Введіть розмірність масиву (n): ");
            int n = int.Parse(Console.ReadLine() ?? "0");

            int[] array1D = new int[n];
            long product1D = 1;

            for (int i = 0; i < n; i++)
            {
                Console.Write($"Введіть елемент {i + 1}: ");
                array1D[i] = int.Parse(Console.ReadLine() ?? "0");
                product1D *= array1D[i];
            }

            CheckIfThreeDigit(product1D);

            Console.WriteLine("\n" + new string('-', 30) + "\n");

            // --- СПОСІБ 2: ДВОВИМІРНИЙ МАСИВ ---
            Console.WriteLine("=== Спосіб 2: Двовимірний масив ===");
            Console.Write("Введіть кількість рядків: ");
            int rows = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Введіть кількість стовпців: ");
            int cols = int.Parse(Console.ReadLine() ?? "0");

            int[,] array2D = new int[rows, cols];
            long product2D = 1;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"Введіть елемент [{i},{j}]: ");
                    array2D[i, j] = int.Parse(Console.ReadLine() ?? "0");
                    product2D *= array2D[i, j];
                }
            }

            CheckIfThreeDigit(product2D);
        }
        static void CheckIfThreeDigit(long value)
        {
            Console.WriteLine($"Добуток дорівнює: {value}");

            long absValue = Math.Abs(value); // Модуль

            if (absValue >= 100 && absValue <= 999)
            {
                Console.WriteLine("Результат: ТАК, добуток є тризначним числом.");
            }
            else
            {
                Console.WriteLine("Результат: НІ, добуток не є тризначним числом.");
            }
        }

        static void Task2()
        {
            Console.Write("Введіть n: ");
            int n = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Введіть m: ");
            int m = int.Parse(Console.ReadLine() ?? "0");

            double[,] numbers = new double[n, m];
            int count = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write($"Число [{i},{j}]: ");
                    numbers[i, j] = double.Parse(Console.ReadLine() ?? "0");
                }
            }

            // ВИПРАВЛЕННЯ ТУТ:
            double previous = numbers[0, 0]; // Беремо найперше число як стартове
            bool first = true;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (first)
                    {
                        first = false;
                        continue; // Пропускаємо перший елемент, бо його нема з чим порівнювати
                    }

                    if (numbers[i, j] > previous)
                    {
                        count++;
                    }
                    previous = numbers[i, j]; // Тепер поточне число стає "попереднім" для наступного кроку
                }
            }

            Console.WriteLine($"\nКількість елементів, більших за попередній: {count}");
        }

        static void Task3()
        {
            Console.Write("Введіть розмірність квадратної матриці (n): ");
            int n = int.Parse(Console.ReadLine() ?? "0");

            int[,] matrix = new int[n, n];
            long sum = 0;

            // 2. Заповнення матриці цілими числами
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"Введіть елемент [{i},{j}]: ");
                    matrix[i, j] = int.Parse(Console.ReadLine() ?? "0");
                }
            }

            // 3. Підрахунок суми побічної діагоналі
            // Ми проходимо одним циклом, бо для кожного рядка i 
            // нам потрібен лише один конкретний стовпець: (n - 1 - i)
            Console.Write("\nЕлементи побічної діагоналі:");
            for (int i = 0; i < n; i++)
            {
                int targetCol = n - 1 - i;
                int value = matrix[i, targetCol];

                Console.Write(" " + value);
                sum += value;
            }

            // 4. Вивід результату
            Console.WriteLine($"\n\nСума елементів побічної діагоналі: {sum}");
        }

        static void Task4()
        {
            Console.WriteLine("=== Завдання 4.2: Східчастий масив ===");
            Console.Write("Введіть кількість рядків (n): ");
            int n = int.Parse(Console.ReadLine() ?? "0");

            // Створюємо східчастий масив
            int[][] jaggedArray = new int[n][];

            // 1. Заповнюємо східчастий масив даними
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Введіть кількість елементів у рядку {i + 1}: ");
                int m = int.Parse(Console.ReadLine() ?? "0");
                jaggedArray[i] = new int[m];

                for (int j = 0; j < m; j++)
                {
                    Console.Write($"Рядок {i + 1}, елемент {j + 1}: ");
                    jaggedArray[i][j] = int.Parse(Console.ReadLine() ?? "0");
                }
            }

            // 2. Створюємо НОВИЙ МАСИВ для збереження результатів (кількостей)
            int[] resultsArray = new int[n];

            // 3. Рахуємо додатні елементи в кожному рядку і записуємо в новий масив
            for (int i = 0; i < n; i++)
            {
                int count = 0;
                foreach (int element in jaggedArray[i])
                {
                    if (element > 0) // Перевірка на додатне число
                    {
                        count++;
                    }
                }
                // Записуємо результат підрахунку в новий масив
                resultsArray[i] = count;
            }

            // 4. Виводимо дані з нового масиву для перевірки
            Console.WriteLine("\nДані з нового масиву (кількість додатних по рядках):");
            Console.Write("Новий масив: ");
            for (int i = 0; i < resultsArray.Length; i++)
                {
                    Console.Write($"{resultsArray[i]} ");
                }
            }

        static void CleanScreen()
        {
            Console.Clear();
        }
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool keepRunning = true;

            while (keepRunning)
            {
                // Console.Clear(); // Очищуємо консоль для охайності
                Console.WriteLine("========= ГОЛОВНЕ МЕНЮ =========");
                Console.WriteLine("1. Завдання 1: Добуток елементів одновимірного і двовимірного масивів");
                Console.WriteLine("2. Завдання 2: рахує кількість елементів які більші попереднього в масиві");
                Console.WriteLine("3. Завдання 3: Сума елементів побічної діагоналі матриці n*n ");
                Console.WriteLine("4. Завдання 4: Виводить максимальну швидкість транспорту залежно ві ознаки ");
                Console.WriteLine("5. Очистити екран");
                Console.WriteLine("0. Вихід");
                Console.WriteLine("--------------------");
                Console.Write("Виберіть номер завдання: ");

                string choice = Console.ReadLine() ?? "0";

                switch (choice)
                {
                    case "1":
                        Task1();
                        break;
                    case "2":
                        Task2();
                        break;
                    case "3":
                        Task3();
                        break;
                    case "4":
                        Task4();
                        break;
                    case "5":
                        CleanScreen();
                        break;
                    case "0":
                        keepRunning = false;
                        Console.WriteLine("До побачення!");
                        continue;
                    default:
                        Console.WriteLine("Помилка: невірний вибір. Спробуйте ще раз.");
                        break;
                }

                if (keepRunning)
                {
                    Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися в меню...");
                    Console.ReadKey();
                }
            }

            Console.ReadKey();
        }
    }
}