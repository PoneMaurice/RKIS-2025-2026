using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace TodoList
{
    class MainClass
    {
        
        public static int survey(string text, int min, int max)
        /*метод выводящий сообшение и
        создаюший цикл пока пользователь не введет допустимые значения */
        {
            int result = -1; // сродникоду об ошибке
            do
            {
                Console.Write(text); // перветственное сообщение 
                result = int.Parse(Console.ReadLine());
            }
            while (result < min || result > max); //условия выхода
            return result;
        }

        public static void Main()
        {
            Console.Write("Нужны ли титры(y/N): "); //Преветственная строка
            string? ansver = Console.ReadLine();
            if (ansver == "y")
            {
                Console.WriteLine(@"Работу сделали: 
Отвецтвенный по исходному коду и README: Шевченко Э. 3831.9
Отвецтвенный по .gitignore и всей git состовляющей: Титов М. 3831.9
");

            }
            Console.Write("Введите ваше имя: ");
            var UserFirstName = Console.ReadLine();
            Console.Write("Введите вашу фамилию: ");
            var UserLastName = Console.ReadLine();

            int OldestPersonYear = 123; //возраст самого старого человека на момент 2025 г.
            int NowYear = DateTime.Now.Year; // сегодняшнее время 

            int year = survey("Введите ваш год рождения: ", NowYear - OldestPersonYear, NowYear);
            int month = survey("Введите ваш месяц рождения: ", 1, 12);
            int day = survey("Введите ваш день рождения: ", 1, 31); 
            // опрос с допутимыми значениями для выхода из опроса

            DateTime BirthDate = new DateTime(year, month, day); // Переводим полученые значения в класс DateTime
            var UserName = UserFirstName + " " + UserLastName; // Объединение для дальнейшего вывода 
            var text = $"\n{DateTime.Now}: добавлен пользователь {UserName}, день рождения {BirthDate.ToLongDateString()}\nвозраст - {DateTime.Now.Year - BirthDate.Year}";
            string WriteTextStart01 = $"Date;UserLastName;UserFirstName;BirthDate"; // создание верха таблици по формату Markdown
            string WriteText = $"{DateTime.Now};{UserLastName};{UserFirstName};{BirthDate.ToShortDateString()}"; // Вводимые значения
            string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.csv"); // относительный путь к файлу. По умолчанию он находиться в папке bin

            bool StartText = true; // флаг наличия или отсутствия заголовка таблици
            FileStream? file = null; // инициализация класса файла
            try
            {

                file = new FileStream(FilePath, FileMode.OpenOrCreate);
                // Проверка на наличие файла и если его нет создает новый 

                using (StreamReader reader = new StreamReader(FilePath, Encoding.UTF8))
                // Поиск заголовка таблици
                {
                    string? line = reader.ReadLine();
                    if (line != WriteTextStart01)
                    {
                        StartText = false;
                    }
                }

                using (StreamWriter writer = new StreamWriter(FilePath, true, Encoding.UTF8))
                // добовление строк в таблицу 
                {
                    if (!StartText)
                    {
                        writer.WriteLine(WriteTextStart01);
                    }
                    writer.WriteLine(WriteText);
                    writer.Close();
                }
                Console.WriteLine(text); // оповещение о завершении операции 
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при записи файла: {ex.Message}");
            }
            finally
            {
                file?.Close(); // после всех операций мы его закрываем
            }
        }
    }
}
