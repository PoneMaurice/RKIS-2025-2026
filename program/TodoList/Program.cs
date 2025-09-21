using System;
using System.Data;

namespace TodoList
{
    class MainClass
    {
        public static int survey(string text, int min, int max)
        {
            int result = -1;
            do
            {
                Console.Write(text);
                ?string test = Console.ReadLine();
                Console.WriteLine(test);
                result = int.Parse(Console.ReadLine());
            }
            while (result < min || result > max);

            return result;
        }
        
        public static void Main()
        {
            Console.Write("Введите ваше имя: ");
            var UserFirstName = Console.ReadLine();
            Console.Write("Введите вашу фамилию: ");
            var UserLastName = Console.ReadLine();

            int OldestPersonYear = 123;
            int NowYear = DateTime.Now.Year;

            // int year = int.Parse(survey("Введите ваш год рождения: ", NowYear - OldestPersonYear, NowYear));
            int year = survey("Введите ваш год рождения: ", NowYear - OldestPersonYear, NowYear);
            int month = survey("Введите ваш месяц рождения: ", 1, 12);
            int day = survey("Введите ваш день рождения: ", 1, 31);

            DateTime BirthDate = new DateTime(year, month, day);
            var UserName = UserFirstName + " " + UserLastName;

            var text = $"\n{DateTime.Now}: добавлен пользователь {UserName}, день рождения {BirthDate.ToLongDateString()}\nвозраст - {DateTime.Now.Year - BirthDate.Year}";

            Console.WriteLine(text);
        }
    }
}
