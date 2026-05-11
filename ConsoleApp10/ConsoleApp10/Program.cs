using System;

namespace StudentLifeEvents
{
    // Клас для передачі деталей події (наприклад, назва предмета або сума)
    public class StudentEventArgs : EventArgs
    {
        public string Message { get; }
        public StudentEventArgs(string message) => Message = message;
    }

    // Клас-видавець (Джерело подій)
    public class University
    {
        public event EventHandler<StudentEventArgs>? OnLessonStarted;
        public event EventHandler<StudentEventArgs>? OnScholarshipPaid;
        public event EventHandler<StudentEventArgs>? OnExamAnnounced;

        public void StartLesson(string subject)
        {
            Console.WriteLine($"\n[Університет] Починається пара з предмету: {subject}");
            OnLessonStarted?.Invoke(this, new StudentEventArgs(subject));
        }

        public void PayScholarship(int amount)
        {
            Console.WriteLine($"\n[Університет] Нараховано стипендію!");
            OnScholarshipPaid?.Invoke(this, new StudentEventArgs(amount.ToString()));
        }

        public void ScheduleExam(string subject)
        {
            Console.WriteLine($"\n[Університет] Увага! Призначено іспит: {subject}");
            OnExamAnnounced?.Invoke(this, new StudentEventArgs(subject));
        }
    }

    // Клас-підписник (Отримувач подій)
    public class Student
    {
        public string Name { get; }

        public Student(string name) => Name = name;

        // Обробники подій
        public void ReactToLesson(object? sender, StudentEventArgs? e)
        {
            Console.WriteLine($"[Студент {Name}] О ні, знову {e?.Message}... Дістаю зошит.");
        }

        public void ReactToMoney(object? sender, StudentEventArgs? e)
        {
            Console.WriteLine($"[Студент {Name}] Ура! {e?.Message} грн! Йду в буфет.");
        }

        public void ReactToExam(object? sender, StudentEventArgs? e)
        {
            Console.WriteLine($"[Студент {Name}] Починаю панікувати через іспит з {e?.Message}!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            // Створюємо об'єкти
            University kpi = new University();
            Student ivan = new Student("Іван");
            Student olena = new Student("Олена");

            // ПІДПИСКА на події
            kpi.OnLessonStarted += ivan.ReactToLesson;
            kpi.OnLessonStarted += olena.ReactToLesson;

            kpi.OnScholarshipPaid += ivan.ReactToMoney;
            kpi.OnScholarshipPaid += olena.ReactToMoney;

            kpi.OnExamAnnounced += olena.ReactToExam; // Тільки Олена підписалася на новини про іспит

            kpi.StartLesson("Програмування C#");
            kpi.PayScholarship(2000);
            kpi.ScheduleExam("Вища математика");

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}