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

namespace Task
{
    public static class Commands
    {
        public static string InputDataType(string text)
        {
            /*Выводит на экран текст и запрашивает у пользователя 
            ввести тип данных и вводит его в бесконечный цикл 
            вводимая пользователем строка проверяеться на наличие 
            такого типа и если он есть возвращает его сокращение*/
            while (true)
            {
                Console.Write(text);
                string input = Console.ReadLine() ?? "NULL";
                input = input.Trim();
                string inputLow = input.ToLower();
                if (input == "" || input == "NULL")
                {
                    Console.WriteLine("Требуеться ввести тип данных");
                    continue;
                }
                else if (inputLow == "integer" || inputLow == "int" || inputLow == "i") return "i";
                else if (inputLow == "double" || inputLow == "float" || inputLow == "f") return "f";
                else if (inputLow == "string" || inputLow == "str" || inputLow == "s") return "s";
                else if (inputLow == "date" || inputLow == "d") return "d";
                else if (inputLow == "time" || inputLow == "t") return "t";
                else if (inputLow == "date and time" || inputLow == "dt") return "dt";
                else Console.WriteLine("Вы ввели неподдерживваемый тип данных");
            }
        }
        public static string InputString(string text)
        {
            /*выводит текст пользователю и запрашивает 
            ввести строковые данные, они проверяютяся на 
            наличие и если строка пуста то возвращаеться 
            "NULL" если нет то возвращается обработаная 
            версия строки*/
            Console.Write(text);
            string input = Console.ReadLine() ?? "NULL";
            input = input.Trim();
            if (input == "") input = "NULL";
            return input;
        }
        public static string InputDate(string text, int min, int max)
        {
            /*Запрашивает у пользователя дату проверяется
            на миниммальное и максимальное допустимое значение
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
            string resultString = result.ToString().PadLeft(2, '0');
            return resultString;
        }
        public static string InputDate(string text)
        {
            /*Перегрузка метода InputDate только без лимитов*/
            int result = -1; // сродникоду об ошибке
            do
            {
                string input = InputString(text);
                int.TryParse(input, out result);
            }
            while (result <= 0);
            string resultString = string.Format("{0:d2}", result);
            return resultString;
        }
        private static string GetModeDate()
        {
            /*Запрашивает всю дату в двух вариантах простом и 
            когда пользователя спрашивают по пунктам 
            а так же если он не выберет какойто из вариантов 
            ввода даты то программа автоматически введет "NULL"*/
            string modeDate = InputString($"Выберете метод ввода даты (Стандартный('S'), Попунктный('P')): ");
            modeDate = modeDate.ToLower();
            if (modeDate == "s")
            {
                string exampleDate = FormatRows.GetNowDate();
                string dateString = InputString($"Введите дату (Пример {exampleDate}): ");
                return dateString;
            }
            else if (modeDate == "p")
            {
                string year = InputDate("Введите год: ");
                string month = InputDate("Введите месяц: ", 1, 12);
                string day = InputDate("Введите день: ",
                1, DateTime.DaysInMonth(int.Parse(year), int.Parse(month)));
                string hour = InputDate("Введите час: ", 0, 23);
                string minute = InputDate("Введите минуты: ", 0, 59);
                string dateString = $"{day}.{month}.{year} {hour}:{minute}";
                return dateString;
            }
            else
            {
                Console.WriteLine("Вы не выбрали режим все даты по default будут 'NULL'");
            }
            return "NULL";
        }
        public static void AddTask()
        {
            /*программа запрашивает у пользователя все необходимые ей данные
            и записует их в файл tasks.csv с нужным форматированием*/
            string nameTask = InputString("Введите название задания: ");
            string description = InputString("Введите описание задания: ");
            System.Console.WriteLine("--- Ввод крайнего срока выполнения ---");
            string deadLine = GetModeDate();
            string dateNow = FormatRows.GetNowDate();

            string fileName = "tasks";

            string[] titleRowArray = { "nameTask", "nameTask", "description", "deadLine" };
            string titleRow = FormatRows.FormatRow(titleRowArray);
            string[] rowArray = { nameTask, description, dateNow, deadLine };
            string row = FormatRows.FormatRow(rowArray);
            FileWriter file = new();
            string fullPath = file.CreatePath(fileName, titleRow);
            file.WriteFile(fullPath, row, true);
        }
        public static void AddTaskAndPrint()
        {
            /*программа запрашивает у пользователя все необходимые ей данные
            и записует их в файл tasks.csv с нужным форматированием 
            после чего выводит сообщение о добовление данных дублируя их 
            пользователю для проверки*/
            string nameTask = InputString("Введите название задания: ");
            string description = InputString("Введите описание задания: ");
            System.Console.WriteLine("--- Ввод крайнего срока выполнения ---");
            string deadLine = GetModeDate();
            string dateNow = FormatRows.GetNowDate();

            string fileName = "tasks";

            string[] titleRowArray = { "nameTask", "nameTask", "description", "deadLine" };
            string titleRow = FormatRows.FormatRow(titleRowArray);
            string[] rowArray = { nameTask, description, dateNow, deadLine };
            string row = FormatRows.FormatRow(rowArray);
            try
            {
                FileWriter file = new();
                string fullPath = file.CreatePath(fileName, titleRow);
                file.WriteFile(fullPath, row, true);

                System.Console.WriteLine("\nTask подназванием {0} успешно занесен в файл", nameTask);
                System.Console.WriteLine("Описание: {0}", description);
                System.Console.WriteLine("Крайний срок выполнения {0}", deadLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникла ошибка при записи в файл\n", ex.Message);
            }

        }
        public static void AddConfUserData(string nameDate)
        {

            if (nameDate == "NULL")
            {
                nameDate = InputString("Введие название для файла с данными: ");
            }

            FileWriter search = new();
            string searchPath = search.CreatePath(nameDate);
            string askFile = "";
            string searchLine1 = "NULL";
            string searchLine2 = "NULL";
            if (File.Exists(searchPath))
            {
                searchLine1 = search.GetLineFile(searchPath, 0);
                searchLine2 = search.GetLineFile(searchPath, 1);
                Console.WriteLine($"{searchLine1}\n{searchLine2}");
                askFile = InputString($"Вы точно уверены что хотите перерзаписать конфигкрацию?(y/N): ");
            }
            if (askFile == "y")
            {
                string intermediateResults = "";
                string sepor = "|||";
                do
                {
                    string intermediateResultString =
                        InputString("Введите название пункта титульного оформления файла: ");
                    if (intermediateResultString == "exit" &&
                    intermediateResults != "") break;
                    else if (intermediateResultString == "exit")
                        Console.WriteLine("В титульном оформлении должен быть хотябы один пункт: ");
                    else if (intermediateResults != "")
                        intermediateResults = intermediateResults + sepor + intermediateResultString;
                    else intermediateResults = intermediateResults + intermediateResultString;
                }
                while (true);
                string[] titleRowArray = intermediateResults.Split(sepor);
                string dataTypesRowString = "";
                string titleRow = FormatRows.FormatRow(titleRowArray);
                foreach (string title in titleRowArray)
                {
                    string dataTypeString = InputDataType($"Введите тип данных для строки {title}: ");
                    if (dataTypesRowString != "")
                    {
                        dataTypesRowString = dataTypesRowString + sepor + dataTypeString;
                    }
                    else dataTypesRowString = dataTypesRowString + dataTypeString;
                }
                string[] dataTypeRowAttay = dataTypesRowString.Split(sepor);
                string dataTypeRow = FormatRows.FormatRow(dataTypeRowAttay);
                Console.WriteLine($"{titleRow}\n{dataTypeRow}");
                FileWriter file = new FileWriter();
                string fullPathConfig = file.CreatePath($"{nameDate}_conf", titleRow);
                string line1 = file.GetLineFile(fullPathConfig, 0);
                string line2 = file.GetLineFile(fullPathConfig, 1);
                string askTitle = "";
                string askDataType = "";
                if (line1 != titleRow && line1 != "NULL")
                {
                    askTitle = InputString($"Титульный лист отличается \nНыняшний: {titleRow}\nПрошлый: {line1}\nЗаменить?(y/N): ");
                }
                else if (line2 != dataTypeRow && line2 != "NULL")
                {
                    askDataType = InputString($"Конфигурация уже имееться\nНынешняя: {dataTypeRow}\nПрошлая: {line2}\nЗаменить?(y/N): ");
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
    }
    public class FormatRows
    {
        public string endRows = "\n";
        public static string FormatRow(string[] data)
        {
            /*Форматирует масив данных под будущию таблицу csv*/
            string text = "";
            foreach (string pathRow in data)
            {
                text = text + pathRow + "|";
            }
            return text;
        }
        public static string GetNowDate()
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
            /*спрашивает и выводит текст субтитров созданый 
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
            /*Составляет текст для судтитров*/
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

            Dictionary<int, string> fices = new Dictionary<int, string>()
            {
                {0, "Шевченок Э."},
                {1, "Титов М."}
            };
            Dictionary<int, string[]> captions = new Dictionary<int, string[]>()
            {
                {0 , Ed},
                {1 , Misha}
            };
            string text = "За работу отвецтвенны:\n";
            for (int i = 0; i < fices.Count; ++i)
            {
                text = text + $"{fices[i]} :";
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
        public string endRows = "\n";
        public string seporRows = "|";
        public string CreatePath(string nameFile, string titleRow)
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
        public string CreatePath(string nameFile)
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
            if (!File.Exists(fullPath))
                using (var fs = new FileStream(fullPath, FileMode.CreateNew)){}
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
            /*Возвращвает строку если ее элемент по заданой позиции 
            соотвецтввует введеным нами данным*/
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
            /*Возвращвает строку если ее элемент по заданой позиции 
            соотвецтввует введеным нами данным*/
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
            /*перрегрузка метода только без позиции*/
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
            "exit"
        };
        public void GlobalCommamd()
        {
            if (SearchExtension(0, "exit")) Exit();
            else if (SearchExtension(0, "help")) Help();
            else if (SearchExtension(0, "add")) Add();
            else if (SearchExtension(0, "task")) Task();
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
            else Commands.AddConfUserData(newText);
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
