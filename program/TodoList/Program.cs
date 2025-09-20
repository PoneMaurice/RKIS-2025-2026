using System;

namespace TodoList
{
    class MainClass
    {
        public static void Main()
        {
            Console.Write("Введите ваше имя: "); 
            var UserFirstName = Console.ReadLine();
            Console.Write("Введите вашу фамилию: ");
            var UserLastName = Console.ReadLine();

            int year;
            int month;
            int day;
            int OldestPersonYear = 123;

            do
            {
                Console.Write("Введите ваш год рождения: ");
                year = short.Parse(Console.ReadLine());
            }
            while (year < DateTime.Now.Year-OldestPersonYear || year > DateTime.Now.Year);

            do
            {
                Console.Write("Введите ваш месяц рождения: ");
                month = int.Parse(Console.ReadLine());
            }
            while (month > 12 || month < 0);

            do
            {
                Console.Write("Введите ваш день рождения: ");
                day = int.Parse(Console.ReadLine());
            }
            while (day > 31 || day < 0);

            DateTime BirthDate = new DateTime(year, month, day);
            var UserName = UserFirstName + " " + UserLastName;

            var text = $"\n{DateTime.Now}: добавлен пользователь {UserName}, день рождения {BirthDate.ToLongDateString()}\nвозраст - {DateTime.Now.Year-BirthDate.Year}";

            Console.WriteLine(text);
        }
    }
}
