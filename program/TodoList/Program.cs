using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;

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

            int year = survey("Введите ваш год рождения: ", NowYear - OldestPersonYear, NowYear);
            int month = survey("Введите ваш месяц рождения: ", 1, 12);
            int day = survey("Введите ваш день рождения: ", 1, 31);

            DateTime BirthDate = new DateTime(year, month, day);
            var UserName = UserFirstName + " " + UserLastName;
            var text = $"\n{DateTime.Now}: добавлен пользователь {UserName}, день рождения {BirthDate.ToLongDateString()}\nвозраст - {DateTime.Now.Year - BirthDate.Year}";
            string WriteTextStart01 = $"|Date|UserLastName|UserFirstName|BirthDate|";
            string WriteTextStart02 = "|:-|:-|:-|:-|";
            string WriteText = $"|{DateTime.Now}|{UserLastName}|{UserFirstName}|{BirthDate.ToShortDateString()}|";
            string FilePath = "/home/edward/Ed/MARK/RKIS-2025-2026/program/TodoList/data.md";


            bool StartText = true;
            FileStream? file = null;
            try
            {

                file = new FileStream(FilePath, FileMode.OpenOrCreate);

                using (StreamReader reader = new StreamReader(FilePath, Encoding.UTF8))
                {
                    string? line = reader.ReadLine();
                    if (line != WriteTextStart01)
                    {
                        StartText = false;
                    }
                }

                using (StreamWriter writer = new StreamWriter(FilePath, true, Encoding.UTF8))
                {
                    if (!StartText)
                    {
                        writer.WriteLine(WriteTextStart01);
                        writer.WriteLine(WriteTextStart02);
                    }
                    writer.WriteLine(WriteText);
                    writer.Close();
                }
                Console.WriteLine(text);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при записи файла: {ex.Message}");
            }
            finally
            {
                file?.Close();
            }
        }
    }
}
