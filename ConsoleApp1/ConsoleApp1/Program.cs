namespace TriangleCalculator
{
    class Program
    {
        static void Task1() {
            Console.WriteLine("========= TASK 1 =========");
            Console.WriteLine("Обчислення радіуса вписаного кола в рівносторонній трикутник зі стороною а");
            Console.WriteLine("Введіть довжину сторони a: ");

            // Зчитуємо вхідні дані
            string input = Console.ReadLine();

            if (double.TryParse(input, out double a) && a > 0)
            {
                // Обчислення за формулою: r = (a * sqrt(3)) / 6
                double r = (a * Math.Sqrt(3)) / 6;

                Console.WriteLine("--------------------------------------");
                Console.WriteLine($"Радіус вписаного кола r = {r:F4}");
            }
            else
            {
                Console.WriteLine("Помилка: введіть коректне числове значення більше за нуль.");
            }
        }

        static void Task2() {
            Console.WriteLine("========= TASK 2 =========");
            Console.Write("Введіть сторону a: ");
            bool resA = double.TryParse(Console.ReadLine(), out double a);

            Console.Write("Введіть сторону b: ");
            bool resB = double.TryParse(Console.ReadLine(), out double b);

            Console.Write("Введіть сторону c: ");
            bool resC = double.TryParse(Console.ReadLine(), out double c);

            // Перевіряємо, чи всі введені дані є числами та чи вони додатні
            if (resA && resB && resC && a > 0 && b > 0 && c > 0)
            {
                // Основна умова існування трикутника
                if (a + b > c && a + c > b && b + c > a)
                {
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine("Результат: ТАК, трикутник із такими сторонами існує.");
                }
                else
                {
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine("Результат: НІ, трикутник із такими сторонами НЕ існує.");
                }
            }
            else
            {
                Console.WriteLine("Помилка: введіть коректні додатні числові значення.");
            }
        }

        static void Task3() {
            Console.WriteLine("========= TASK 3 =========");
            Console.Write("Введіть x: ");
            double.TryParse(Console.ReadLine(), out double x);
            Console.Write("Введіть y: ");
            double.TryParse(Console.ReadLine(), out double y);

            // Логіка згідно з графіком: 
            // Заштриховано ВСЕ, крім квадрата в 3-й чверті (від -15 до 0 по обох осях)

            // Межі порожнього квадрата
            bool inEmptySquare = (x > -15 && x < 0) && (y > -15 && y < 0);
            bool onBorder = ((x == -15 || x == 0) && (y >= -15 && y <= 0)) ||
                           ((y == -15 || y == 0) && (x >= -15 && x <= 0));

            if (onBorder)
                Console.WriteLine("Результат: На межі");
            else if (inEmptySquare)
                Console.WriteLine("Результат: Ні (поза областю)");
            else
                Console.WriteLine("Результат: Так (всередині області)");
        }

        static void Task4() {
            Console.WriteLine("========= TASK 4 =========");
            Console.WriteLine("Типи транспорту: а - автомобіль, в - велосипед, м - мотоцикл, с - літак, п - поїзд");
            Console.Write("Введіть ознаку транспортного засобу: ");

            // Зчитуємо символ і переводимо в нижній регістр, щоб працювало і з 'А', і з 'а'
            string input = Console.ReadLine().ToLower();

            Console.WriteLine("--------------------------------------");
            switch (input)
            {
                case "а":
                    Console.WriteLine("Максимальна швидкість автомобіля: 250 км/год");
                    break;
                case "в":
                    Console.WriteLine("Максимальна швидкість велосипеда: 50 км/год");
                    break;
                case "м":
                    Console.WriteLine("Максимальна швидкість мотоцикла: 300 км/год");
                    break;
                case "с":
                    Console.WriteLine("Максимальна швидкість літака: 900 км/год");
                    break;
                case "п":
                    Console.WriteLine("Максимальна швидкість поїзда: 200 км/год");
                    break;
                default:
                    Console.WriteLine("Помилка: невідома ознака транспорту.");
                    break;
            }
        }

        static long CalculateCube(int number) {
            return (long)number * number * number;
        }

        static void Task5() {
            Console.WriteLine("========= TASK 5 =========");
            Console.Write("Введіть ціле число: ");
            if (int.TryParse(Console.ReadLine(), out int n))
            {
                long result = CalculateCube(n);
                Console.WriteLine($"{n} у кубі = {result}");
            }
            else Console.WriteLine("Помилка: потрібно ввести ціле число.");

        }

        static void Task6() {
            Console.WriteLine("========= TASK 6 =========");
            Console.WriteLine("Обчислення виразу: (1/xy + 1/(x^2 + 1)) * (x + y)");
            Console.Write("Введіть x: "); double.TryParse(Console.ReadLine(), out double x);
            Console.Write("Введіть y: "); double.TryParse(Console.ReadLine(), out double y);

            if (x * y == 0)
            {
                Console.WriteLine("Помилка: ділення на нуль (xy не може бути 0).");
            }
            else
            {
                // Формула: (1/xy + 1/(x^2 + 1)) * (x + y)
                double part1 = 1.0 / (x * y);
                double part2 = 1.0 / (Math.Pow(x, 2) + 1);
                double result = (part1 + part2) * (x + y);

                Console.WriteLine($"Результат виразу: {result:F4}");
            }
        }

        static void CleanScreen() {
            Console.Clear();
        }

        static void Main(string[] args)
        {
            // Налаштування кодування для коректного відображення тексту
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool keepRunning = true;

            while (keepRunning)
            {
                // Console.Clear(); // Очищуємо консоль для охайності
                Console.WriteLine("========= ГОЛОВНЕ МЕНЮ =========");
                Console.WriteLine("1. Завдання 1: Радіус вписаного кола");
                Console.WriteLine("2. Завдання 2: Перевірка існування трикутника за трьома сторонами");
                Console.WriteLine("3. Завдання 3: Точка в певній зоні");
                Console.WriteLine("4. Завдання 4: Виводить максимальну швидкість транспорту залежно ві ознаки ");
                Console.WriteLine("5. Завдання 5: Число в кубі ");
                Console.WriteLine("6. Обчислення виразу (6.2)");
                Console.WriteLine("7. Очистити екран");
                Console.WriteLine("0. Вихід");
                Console.WriteLine("--------------------");
                Console.Write("Виберіть номер завдання: ");

                string choice = Console.ReadLine();

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
                        Task5();
                        break;
                    case "6":
                        Task6();
                        break;
                    case "7":
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