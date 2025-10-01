using System;
using System.Data;
using System.Formats.Asn1;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;
using Microsoft.VisualBasic;
using Task;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Reflection.Metadata;

namespace Task
{
    public class Commands
    {
        const string StringChar = "s";
        public const string IntegerChar = "i";
        public const string DoubleChar = "f";
        public const string TimeChar = "t";
        public const string DateChar = "d";
        public const string DateAndTime = "dt";
        public const string NowDateTime = "ndt";
        public string InputDataType(string text)
        {

            /*Выводит на экран текст и запрашивает у пользователя 
            ввести тип данных и вводит его в бесконечный цикл 
            вводимая пользователем строка проверяеться на наличие 
            такого типа и если он есть возвращает его сокращение*/

            Dictionary<string, string> DataTypePros = new Dictionary<string, string>()
            {
                {"string", StringChar}, {"str", StringChar}, {"s", StringChar},
                {"integer", IntegerChar}, {"int", IntegerChar}, {"i", IntegerChar},
                {"double", DoubleChar}, {"float", DoubleChar}, {"f", DoubleChar},
                {"date", DateChar}, {"d", DateChar},
                {"time", TimeChar}, {"t", TimeChar},
                {"date and time", DateAndTime}, {"dt", DateAndTime}, {"datetime", DateAndTime},
                {"now date time", NowDateTime}, {"ndt", NowDateTime}, {"nowdatetime", NowDateTime}
            };
            while (true)
            {
                Console.Write(text);
                string input = Console.ReadLine() ?? "NULL";
                input = input.Trim();
                string inputLow = input.ToLower();
                foreach (var dataType in DataTypePros)
                {
                    if (inputLow == dataType.Key)
                    {
                        return dataType.Value;
                    }
                }
                Console.WriteLine("Вы ввели неподдерживаемый тип данных");
            }
        }
        public static string InputString(string text)
        {
            /*выводит текст пользователю и запрашивает 
            ввести строковые данные, они проверяются на
            наличие и если строка пуста то возвращаеться 
            "NULL" если нет то возвращается обработаная 
            версия строки*/
            Console.Write(text);
            string input = Console.ReadLine() ?? "NULL";
            input = input.Trim();
            if (input == "") input = "NULL";
            return input;
        }
        public static int InputDate(string text, int min, int max)
        {
            /*Запрашивает у пользователя дату, проверяется
            на миниммальное и максимальное допустимое значение,
            а так же возвращает простые цифры с нулем.
            Пример: 02, 00, 09 и тд.s*/
            int result = -1; // сродникоду об ошибке
            string input;
            do
            {
                input = InputString(text);
                int.TryParse(input, out result);
            }
            while (result < min || result > max); //условия выхода
            return result;
        }
        public static int InputDate(string text)
        {
            /*Перегрузка метода InputDate только без лимитов*/
            int result = -1; // сродникоду об ошибке
            do
            {
                string input = InputString(text);
                int.TryParse(input, out result);
            }
            while (result <= 0);
            return result;
        }
        private static string GetModeDateTime()
        {
            /*Запрашивает всю дату в двух вариантах опросом и 
            когда пользователя спрашивают по пунктам, 
            а так же если он не выберет какой-то из вариантов 
            ввода даты то программа автоматически введет "NULL"*/
            string modeDate = InputString($"Выберете метод ввода даты: (Стандартный('S'), Попунктный('P')): ");
            modeDate = modeDate.ToLower();
            if (modeDate == "s")
            {
                string exampleDate = FormatRows.GetNowDateTime();
                string dateString = InputString($"Введите дату (Пример {exampleDate}): ");
                return dateString;
            }
            else if (modeDate == "p")
            {
                int year = InputDate("Введите год: ");
                int month = InputDate("Введите месяц: ", 1, 12);
                int day = InputDate("Введите день: ", 1,
                    DateTime.DaysInMonth(year, month));
                DateOnly yearMonthDay = new(year, month, day);
                string date = yearMonthDay.ToShortDateString();
                int hour = InputDate("Введите час: ", 0, 23);
                int minute = InputDate("Введите минуты: ", 0, 59);
                TimeOnly hourAndMinute = new(hour, minute);
                string time = hourAndMinute.ToShortTimeString();
                string dateString = $"{date} {time}";
                return dateString;
            }
            else
            {
                Console.WriteLine("Вы не выбрали режим, все даты по default будут 'NULL'");
            }
            return "NULL";
        }
        private static string GetModeDate()
        {
            /*Запрашивает всю дату в двух вариантах опросом и 
            когда пользователя спрашивают по пунктам, 
            а так же если он не выберет какой-то из вариантов 
            ввода даты то программа автоматически введет "NULL"*/
            string modeDate = InputString($"Выберете метод ввода даты: (Стандартный('S'), Попунктный('P')): ");
            modeDate = modeDate.ToLower();
            if (modeDate == "s")
            {
                string exampleDate = DateTime.Now.ToShortDateString();
                string dateString = InputString($"Введите дату (Пример {exampleDate}): ");
                return dateString;
            }
            else if (modeDate == "p")
            {
                int year = InputDate("Введите год: ");
                int month = InputDate("Введите месяц: ", 1, 12);
                int day = InputDate("Введите день: ", 1,
                    DateTime.DaysInMonth(year, month));
                DateOnly yearMonthDay = new(year, month, day);
                return yearMonthDay.ToShortDateString();
            }
            else
            {
                Console.WriteLine("Вы не выбрали режим, все даты по default будут 'NULL'"); 
            }
            return "NULL";
        }
        private static string GetModeTime()
        {
            /*Запрашивает всю дату в двух вариантах опросом и 
            когда пользователя спрашивают по пунктам, 
            а так же если он не выберет какой-то из вариантов 
            ввода даты то программа автоматически введет "NULL"*/
            string modeDate = InputString($"Выберете метод ввода даты: (Стандартный('S'), Попунктный('P')): ");
            modeDate = modeDate.ToLower();
            if (modeDate == "s")
            {
                string exampleDate = DateTime.Now.ToShortTimeString();
                string dateString = InputString($"Введите время (Пример {exampleDate}): ");
                return dateString;
            }
            else if (modeDate == "p")
            {
                int hour = InputDate("Введите час: ", 0, 23);
                int minute = InputDate("Введите минуты: ", 0, 59);
                TimeOnly hourAndMinute = new(hour, minute);
                return hourAndMinute.ToShortTimeString();
            }
            else
            {
                Console.WriteLine("Вы не выбрали режим, все даты по default будут 'NULL'");
            }
            return "NULL";
        }
        public static void AddTask()
        {
            /*программа запрашивает у пользователя все необходимые ей данные
            и записывает их в файл tasks.csv с нужным форматированием*/
            string nameTask = InputString("Введите название задания: ");
            string description = InputString("Введите описание задания: ");
            System.Console.WriteLine("--- Ввод крайнего срока выполнения ---");
            string deadLine = GetModeDateTime();
            string dateNow = FormatRows.GetNowDateTime();

            string fileName = "tasks";

            string[] titleRowArray = { "nameTask", "nameTask", "description", "deadLine" };
            string titleRow = FormatRows.FormatRow(titleRowArray);
            string[] rowArray = { nameTask, description, dateNow, deadLine };
            string row = FormatRows.FormatRow(rowArray);
            FileWriter file = new();
            string fullPath = file.TitleRowWriter(fileName, titleRow);
            file.WriteFile(fullPath, row, true);
        }
        public static void AddTaskAndPrint()
        {
            /*программа запрашивает у пользователя все необходимые ей данные
            и записывает их в файл tasks.csv с нужным форматированием 
            после чего выводит сообщение о добовлении данных дублируя их 
            пользователю для проверки*/
            string nameTask = InputString("Введите название задания: ");
            string description = InputString("Введите описание задания: ");
            System.Console.WriteLine("--- Ввод крайнего срока выполнения ---");
            string deadLine = GetModeDateTime();
            string dateNow = FormatRows.GetNowDateTime();

            string fileName = "tasks";

            string[] titleRowArray = { "nameTask", "nameTask", "description", "deadLine" };
            string titleRow = FormatRows.FormatRow(titleRowArray);
            string[] rowArray = { nameTask, description, dateNow, deadLine };
            string row = FormatRows.FormatRow(rowArray);
            try
            {
                FileWriter file = new();
                string fullPath = file.TitleRowWriter(fileName, titleRow);
                file.WriteFile(fullPath, row, true);

                System.Console.WriteLine("\nTask под названием {0} успешно занесен в файл", nameTask);
                System.Console.WriteLine("Описание: {0}", description);
                System.Console.WriteLine("Крайний срок выполнения {0}", deadLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникла ошибка при записи в файл\n", ex.Message);
            }

        }
        public static void AddConfUserData(string nameData)
        {
            if (nameData == "NULL")
            {
                nameData = InputString("Введите название для файла с данными: ");
            }

            FileWriter file = new();
            string fullPathConfig = file.CreatePath($"{nameData}_conf");
            string askFile = "y";
            string searchLine1 = "NULL";
            string searchLine2 = "NULL";
            if (File.Exists(fullPathConfig))
            {
                searchLine1 = file.GetLineFile(fullPathConfig, 0);
                searchLine2 = file.GetLineFile(fullPathConfig, 1);
                Console.WriteLine($"{searchLine1}\n{searchLine2}");
                askFile = InputString($"Вы точно уверены, что хотите перезаписать конфигурацию?(y/N): ");
            }
            if (askFile == "y")
            {
                string titleRow = "";
                string sepor = file.seporRows;
                do
                {
                    string intermediateResultString =
                        InputString("Введите название пункта титульного оформления файла: ");
                    if (intermediateResultString == "exit" &&
                    titleRow != "") break;
                    else if (intermediateResultString == "exit")
                        Console.WriteLine("В титульном оформлении должен быть хотя бы один пункт: ");
                    else if (titleRow != "")
                        titleRow = titleRow + sepor + intermediateResultString;
                    else titleRow = titleRow + intermediateResultString;
                }
                while (true);
                string[] titleRowArray = titleRow.Split(sepor);
                string dataTypeRow = "";
                foreach (string title in titleRowArray)
                {
                    Commands config = new();
                    string dataTypeString = config.InputDataType($"Введите тип данных для строки {title}: ");
                    if (dataTypeRow != "")
                    {
                        dataTypeRow = dataTypeRow + sepor + dataTypeString;
                    }
                    else dataTypeRow = dataTypeRow + dataTypeString;
                }
                Console.WriteLine($"{titleRow}\n{dataTypeRow}");
                fullPathConfig = file.TitleRowWriter($"{nameData}_conf", titleRow);
                string line1 = file.GetLineFile(fullPathConfig, 0);
                string line2 = file.GetLineFile(fullPathConfig, 1);
                string askTitle = "y";
                string askDataType = "y";
                if (line1 != titleRow && line1 != "NULL")
                {
                    askTitle = InputString($"Титульный лист отличается \nНыняшний: {titleRow}\nПрошлый: {line1}\nЗаменить?(y/N): ");
                }
                else if (line2 != dataTypeRow && line2 != "NULL")
                {
                    askDataType = InputString($"Конфигурация уже имеется\nНынешняя: {dataTypeRow}\nПрошлая: {line2}\nЗаменить?(y/N): ");
                }
                if (askTitle == "y" || askDataType == "y")
                {
                    file.WriteFile(fullPathConfig, titleRow, false);
                    file.WriteFile(fullPathConfig, dataTypeRow, true);
                }
            }
            else
            {
                System.Console.WriteLine("Будет использована конфигурация: ");
                System.Console.WriteLine($"{searchLine1}\n{searchLine2}");
            }
        }
        public static void AddUserData(string nameData)
        {
            FileWriter file = new();

            string fullPathConfig = file.CreatePath($"{nameData}_conf");
            if (File.Exists(fullPathConfig))
            {
                string titleRow = file.GetLineFile(fullPathConfig, 0);
                string[] titleRowArray = titleRow.Split(file.seporRows);
                string dataTypeRow = file.GetLineFile(fullPathConfig, 1);
                string[] dataTypeRowArray = dataTypeRow.Split(file.seporRows);
                string row = "";
                for (int i = 0; i < titleRowArray.Length; i++)
                {
                    string path = "NULL";
                    switch (dataTypeRowArray[i])
                    {
                        case Commands.StringChar:
                            path = InputString($"введите {titleRowArray[i]}: ");
                            break;
                        case Commands.IntegerChar:
                            path = InputString($"введите {titleRowArray[i]}: ");
                            break;
                        case Commands.DoubleChar:
                            path = InputString($"введите {titleRowArray[i]}: ");
                            break;
                        case Commands.DateChar:
                            Console.WriteLine($"---ввод {titleRowArray[i]}---");
                            path = GetModeDate();
                            break;
                        case Commands.TimeChar:
                            Console.WriteLine($"---ввод {titleRowArray[i]}---");
                            path = GetModeTime();
                            break;
                        case Commands.DateAndTime:
                            Console.WriteLine($"---ввод {titleRowArray[i]}---");
                            path = GetModeDateTime();
                            break;
                        case Commands.NowDateTime:
                            path = FormatRows.GetNowDateTime();
                            break;
                    }
                    if (row == "") row = row + path;
                    else row = row + file.seporRows + path;
                }
                string fullPath = file.TitleRowWriter(nameData, titleRow);
                string testTitleRow = file.GetLineFile(fullPath, 0);
                if (testTitleRow != titleRow)
                {
                    file.WriteFile(fullPath, titleRow, false);
                }
                file.WriteFile(fullPath, row, true);
            }
            else Console.WriteLine($"Сначала создайте конфигурацию или проверьте правильность написания названия => '{nameData}'");
        }
    }
    public class FormatRows
    {
        public static string FormatRow(string[] data)
        {
            /*Форматирует массив данных под будущию таблицу csv*/
            string text = "";
            foreach (string pathRow in data)
            {
                if (text == "") text = text + pathRow;
                else text = text + "|" + pathRow;
            }
            return text;
        }
        public static string GetNowDateTime()
        {
            /*возвращает сегодняшнюю дату и время в нужном формате*/
            DateTime nowDate = DateTime.Now;
            string date = nowDate.ToShortDateString();
            string time = nowDate.ToShortTimeString();
            string dateString = ($"{date} {time}");
            return dateString;
        }
    }
    public class Captions
    {
        public string TextCaptions = "";
        public void WriteCaption()
        {
            /*спрашивает и выводит текст субтитров созданный 
            методом CompText*/
            if (TextCaptions == "") CompText();
            Console.Write("Вывести титры?(y/N): ");
            string Char = Console.ReadLine() ?? "NULL";
            Char = Char.Trim();
            if (Char == "") Char = "NULL";
            Char = Char.ToLower();
            if (Char == "y") Console.WriteLine(TextCaptions);
        }
        void CompText()
        {
            /*Составляет текст для субтитров*/
            /*WHAT THE HAY IS THAT?! I think i actually like that;) - PoneMaurice */
            string[] Ed =
            {
            "README",
            "исходный код",
            "некоторые аспекты git"
            };
            string[] Misha =
            {
            "git",
            ".gitignore",
            "некоторый части исходного кода"
            };

            Dictionary<int, string> feces /*An appropriate name for our duo*/= new Dictionary<int, string>()
            {
                {0, "Шевченок Э."}, // Шевченок Э. на месте? - PoneMaurice
                {1, "Титов М."}
            };
            Dictionary<int, string[]> captions = new Dictionary<int, string[]>()
            {
                {0 , Ed},
                {1 , Misha}
            };
            string text = "За работу ответственны:\n";
            for (int i = 0; i < feces.Count; ++i)
            {
                text = text + $"{feces[i]} :";
                for (int j = 0; j < captions[i].Length; ++j)
                {
                    string[] caption = captions[i];
                    text = text + $" {caption[j]}";
                }
                text = text + "\n";
            }
            TextCaptions = text;
        }
    }
    public class FileWriter
    {
        public string seporRows = "|";
        public string CreatePath(string nameFile) // Function for creating file path - PoneMaurice
        {
            /*Создание актульного пути под каждый нужный файл находящийся в деректории с конфигами*/
            string dataPath = "/.config/RKIS-TodoList/"; // Расположение файла для UNIX и MacOSX
            string winDataPath = "\\RKIS-todoList\\"; // Расположение файла для Win32NT
            string fullPath;

            string? homePath = (Environment.OSVersion.Platform == PlatformID.Unix || // Если платформа UNIX или MacOSX, то homePath = $HOME
                   Environment.OSVersion.Platform == PlatformID.MacOSX)
                   ? Environment.GetEnvironmentVariable("HOME")
                   : Environment.ExpandEnvironmentVariables("%APPDATA%");   // Если платформа Win32NT, то homepath = \users\<username>\Documents 
            if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
                fullPath = Path.Join(homePath, dataPath); // Если платформа UNIX или MacOSX, то мы соединяем homePath и dataPath
            else
                fullPath = Path.Join(homePath, winDataPath); // Если платформа Win32NT, то мы соединяем homePath и winDataPath
            DirectoryInfo? directory = new DirectoryInfo(fullPath); // Инициализируем объект класса для создания директории
            if (!directory.Exists) Directory.CreateDirectory(fullPath); // Если директория не существует, то мы её создаём по пути fullPath
            fullPath = Path.Join(fullPath, $"{nameFile}.csv");
            return fullPath;
        }
        public string TitleRowWriter(string nameFile, string titleRow)
        {
            string fullPath = CreatePath(nameFile);
            if (!File.Exists(fullPath))
                using (var fs = new FileStream(fullPath, FileMode.CreateNew,
                FileAccess.Write, FileShare.Read))
                {
                        using (var sw = new StreamWriter(fs))
                        {
                            sw.WriteLine(titleRow);
                        }
                }
            return fullPath;
        }
        public void WriteFile(string fullPath, string dataFile, bool noRewrite)
        {
            /*Запись в конец файла строки*/
            try
            {
                if (noRewrite)
                {
                    using (StreamWriter sw = new StreamWriter(fullPath, true, Encoding.UTF8))
                    {
                        sw.WriteLine(dataFile);
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(fullPath, false, Encoding.UTF8))
                    {
                        sw.WriteLine(dataFile);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}\n");
            }
        }
        public string GetLineFile(string fullPath, string dataFile, int positionInRow)
        {
            /*Возвращает строку если ее элемент по заданой позиции 
            соответствует введеным нами данным*/
            try
            {
                using (StreamReader reader = new StreamReader(fullPath, Encoding.UTF8))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] pathLine = line.Split(seporRows);
                        if (pathLine.Length < positionInRow)
                        {
                            if (pathLine[positionInRow] == dataFile)
                                return pathLine[positionInRow];
                            else System.Console.WriteLine($"Строка '{line}'\nНе содержит {dataFile}.");
                        }
                        else
                        {
                            System.Console.WriteLine($"В файле нет столько позиций");
                            System.Console.WriteLine(line);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}\n");
            }
            return "NULL";
        }
        public string GetLineFile(string fullPath, int positionRow)
        {
            /*Возвращает строку если ее элемент по заданной позиции 
            соответствует введеным нами данным*/
            try
            {
                using (StreamReader reader = new StreamReader(fullPath, Encoding.UTF8))
                {
                    string? line;
                    int numLine = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (numLine == positionRow)
                        {
                            return line;
                        }
                        ++numLine;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}\n");
            }
            return "NULL";
        }
        public string GetLineFile(string fullPath, string dataFile)
        {
            /*перегрузка метода только без позиции*/
            try
            {
                using (StreamReader reader = new StreamReader(fullPath, Encoding.UTF8))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line == dataFile)
                        {
                            return line;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}\n");
            }
            return "NULL";
        }
    }
    public class Survey
    {
        public int counter = 0;
        public string newText = "";
        public string[] listComm = {
            "none",
            "add",
            "help",
            "print",
            "task",
            "clear",
            "search",
            "config",
            "exit"
        };
        public void GlobalCommamd()
        {
            if (SearchExtension(0, "exit")) Exit();
            else if (SearchExtension(0, "help")) Help();
            else if (SearchExtension(0, "add")) Add();
            else if (SearchExtension(0, "task")) Task();
            else if (SearchExtension(0, "print")) Print();
            else None();
        }
        public void Help()
        {
            string text = "help";
            Console.WriteLine(text);
        }
        public void Add()
        {
            string text = "add";
            if (SearchExtension(1, "help")) Console.WriteLine($"{text} help");
            else if (SearchExtension("task") && SearchExtension("print"))
                Commands.AddTaskAndPrint();
            else if (SearchExtension(1, "task")) Commands.AddTask();
            else if (SearchExtension(1, "config")) Commands.AddConfUserData(newText);
            else Commands.AddUserData(newText);
        }
        public void Task()
        {
            string text = "task";
            if (SearchExtension(1, "help")) Console.WriteLine($"{text} help");
            else if (SearchExtension(1, "clear")) Console.WriteLine($"{text} clear");
            else if (SearchExtension(1, "search")) Console.WriteLine($"{text} search");
            else if (SearchExtension(1, "print")) Console.WriteLine($"{text} print");
            else Console.WriteLine(text);
        }
        public void Print()
        {
            string text = "print";
            if (SearchExtension(1, "help")) Console.WriteLine($"{text} help");
            else Console.WriteLine(text);
        }
        public void Exit()
        {
            Environment.Exit(0);
        }
        public void None()
        {
            Console.WriteLine("none");
        }


        public Dictionary<string, bool> extensions = new Dictionary<string, bool> { };
        public void AddExtensions()
        {
            for (int i = 0; i < listComm.Length; ++i)
            {
                extensions.Add(listComm[i], false);
            }
        }
        public void ClearExtensions() {
            for (int i = 0; i < extensions.Count; ++i)
            {
                extensions[listComm[i]] = false;
            }
        }
        public Dictionary<string, int> extensionsNUM = new Dictionary<string, int> { };
        public void AddExtensionsNUM()
        {
            for (int i = 0; i < listComm.Length; ++i)
            {
                extensionsNUM.Add(listComm[i], 0);
            }
        }
        public void ClearExtensionsNUM() {
            for (int i = 0; i < extensionsNUM.Count; ++i)
            {
                extensionsNUM[listComm[i]] = 0;
            }
        }
        public bool SearchExtension(int position, string extension)
        {
            if (extensionsNUM[extension] == position &&
            extensions[extension] == true) return true;
            return false;
        }
        public bool SearchExtension(string extension)
        {
            if (extensions[extension] == true) return true;
            return false;
        }
        
        public void ProceStr(string text)
        {
            Console.Write(text);
            string ask = Console.ReadLine() ?? "NULL";
            ask = ask.Trim();
            if (ask == "") ask = "NULL";
            string[] partsText = ask.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            SearchCommand(partsText);
            newText = AssociationString(partsText);
            GlobalCommamd();
            Console.WriteLine(newText);
        }

        public string AssociationString(string[] sepText)
        {
            string text = "";
            bool noneText = true;
            for (int i = counter; i < sepText.Length; ++i)
            {
                if (noneText)
                {
                    text = text + sepText[i];
                    noneText = false;
                }
                else text = text + " " + sepText[i];
            }
            if (text == "") text = "NULL";
            return text;
        }

        void SearchCommand(string[] command)
        {
            AddExtensions();
            AddExtensionsNUM();
            int num = 0;

            for (int i = 0; i < command.Length; ++i)
            {
                int lus = 1;
                for (int j = 1; j < listComm.Length; ++j)
                {
                    if (command[i] == listComm[j] &&
                    extensions[listComm[j]] != true)
                    {
                        extensions[listComm[j]] = true;
                        extensionsNUM[listComm[j]] = i;
                        num++;
                        break;
                    }
                    else ++lus;
                }
                if (lus == listComm.Length)
                {
                    if (i == 0) extensions["none"] = true;
                    break;
                }
            }
            counter = num;
        }
    }
    public static class TaskExtensions
    {
        public static void Main()
        {
            int cycle = 0;
            do
            {
                if (cycle == 0)
                {
                    var cap = new Captions();
                    cap.WriteCaption();
                }
                var sur = new Survey();
                sur.ProceStr("-- ");
                ++cycle;
            }
            while (true);
        }
    }
}
