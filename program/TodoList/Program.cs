using Syste
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
        public static string ProceStr(string text)
        {
            if (text is null) return "NULL";

            text = text.Trim();

            if (text == "q" || text == "Q") Environment.Exit(0);
            else if (text == "") return "NULL";

            return text;
        }
        
        public static int survey(string text, int min, int max)
        /*метод выводящий сообшение и
        создаюший цикл пока пользователь не введет допустимые значения */
        {
            int result = -1; // сродникоду об ошибке
            string num_str;
            do
            {
                Console.Write(text); // перветственное сообщение 
                num_str = Console.ReadLine() ?? "NULL";
                if (!int.TryParse(num_str, out result)) num_str = ProceStr(num_str);
            }
            while (result < min || result > max); //условия выхода
            return result;
        }
        public static string survey(string text)
        {
            Console.Write(text);
            string str = Console.ReadLine() ?? "Неизвестно";
            str = ProceStr(str);
            return str;
        }

        public static void Main()
        {
            Console.Write("Нужны ли титры(y/N): "); //Преветственная строка
            string? answer = Console.ReadLine();
            string Captions = @"Работу сделали: 
Ответственный по исходному коду и README: Шевченко Э. 3831.9
Ответственный по .gitignore, всей git составляющей и некоторыми частями исходного кода: Титов М. 3831.9
";
            if (answer == "y") Console.WriteLine(Captions);
            var UserFirstName = survey("Введите ваше имя: ");
            var UserLastName = survey("Введите вашу фамилию: ");

            int OldestPersonYear = 123; //возраст самого старого человека на момент 2025 г.
            int NowYear = DateTime.Now.Year; // сегодняшнее время 

            int year = survey("Введите ваш год рождения: ", NowYear - OldestPersonYear, NowYear);
            int month = survey("Введите ваш месяц рождения: ", 1, 12);
            int day = survey("Введите ваш день рождения: ", 1, 31);
            // опрос с допуcтимыми значениями для выхода из опроса

            DateTime BirthDate = new DateTime(year, month, day); // Переводим полученые значения в класс DateTime
            var UserName = UserFirstName + " " + UserLastName; // Объединение для дальнейшего вывода 
            var text = $"\n{DateTime.Now}: добавлен пользователь {UserName}, день рождения {BirthDate.ToLongDateString()}\nвозраст - {DateTime.Now.Year - BirthDate.Year}";
            string WriteTextStart01 = $"Date;UserLastName;UserFirstName;BirthDate"; // создание верха таблици по формату Markdown
            string WriteText = $"{DateTime.Now};{UserLastName};{UserFirstName};{BirthDate.ToShortDateString()}"; // Вводимые значения
            string dataPath = "/.config/RKIS-TodoList/"; // Расположение файла для UNIX и MacOSX
            string winDataPath = "\\RKIS-todoList\\"; // Расположение файла для Win32NT

            string? homePath = (Environment.OSVersion.Platform == PlatformID.Unix || // Если платформа UNIX или MacOSX, то homePath = $HOME
                   Environment.OSVersion.Platform == PlatformID.MacOSX)
                   ? Environment.GetEnvironmentVariable("HOME")
                   : Environment.ExpandEnvironmentVariables("%APPDATA%");   // Если платформа Win32NT, то homepath = \users\<username>\Documents 
            string fullPath;
            if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
                fullPath = Path.Join(homePath, dataPath); // Если платформа UNIX или MacOSX, то мы соединяем homePath и dataPath
            else
                fullPath = Path.Join(homePath, winDataPath); // Если платформа Win32NT, то мы соединяем homePath и winDataPath
            string FilePath = Path.Join(fullPath, "data.csv"); // Полный путь к файлу, используется для его создания и/или чтения

            bool StartText = true; // флаг наличия или отсутствия заголовка таблици
            FileStream? file = null; // инициализация класса файла
            DirectoryInfo? directory = new DirectoryInfo(fullPath); // Инициализируем объект класса для создания директории
            if (!directory.Exists) Directory.CreateDirectory(fullPath); // Если директория не существует, то мы её создаём по пути fullPath
            try
            {
                using (file = new FileStream(FilePath, FileMode.OpenOrCreate)) {}
                // Проверка на наличие файла и если его нет создает новый 

                using (StreamReader reader = new StreamReader(FilePath, Encoding.UTF8))
                // Поиск заголовка таблици
                {
                    string? line = reader.ReadLine();
                    if (line != WriteTextStart01) StartText = false;
                }

                using (StreamWriter writer = new StreamWriter(FilePath, true, Encoding.UTF8))
                // добовление строк в таблицу 
                {
                    if (!StartText) writer.WriteLine(WriteTextStart01);
                    writer.WriteLine(WriteText);
                    writer.Close();
                }
                Console.WriteLine(text); // оповещение о завершении операции 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при записи файла: {ex.Message}");
            }
        }
    }
}
