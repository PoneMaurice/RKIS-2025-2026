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

namespace Task
{
    public static class Commands{
        public static string InputString(string text)
        {
            Console.Write(text);
            string input = Console.ReadLine() ?? "NULL";
            input = input.Trim();
            if (input == "") input = "NULL";
            return input;
        }
        public static string InputDate(string text, int min, int max)
        {
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
            int result = -1; // сродникоду об ошибке
            do
            {
                string input = InputString(text);
                int.TryParse(input, out result);
            }
            while (result <= 0);
            string resultString = result.ToString().PadLeft(2, '0');
            return resultString;
        }
        private static string GetModeDate()
        {
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
            string nameTask = InputString("Введите название задания: ");
            string description = InputString("Введите описание задания: ");
            string deadLine = GetModeDate();
            string dateNow = FormatRows.GetNowDate();

            string fileName = "tasks";

            string[] titleRowArray = { "nameTask", "nameTask", "description", "deadLine" };
            string titleRow = FormatRows.FormatRow(titleRowArray);
            string[] rowArray = { nameTask, description, dateNow, deadLine };
            string row = FormatRows.FormatRow(rowArray);
            FileWriter file = new();
            file.CreatePath(fileName, titleRow);
            file.WriteFile(row);
        }
    }
    public class FormatRows
    {
        public string endRows = "\n";
        public static string FormatRow(string[] data)
        {
            string text = "";
            foreach (string pathRow in data)
            {
                text = text + pathRow + "|";
            }
            return text;
        }
        public static string GetNowDate()
        {
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
        public string fullPath = "string";
        public void CreatePath(string nameFile, string titleRow)
        {
            string dataPath = "/.config/RKIS-TodoList/"; // Расположение файла для UNIX и MacOSX
            string winDataPath = "\\RKIS-todoList\\"; // Расположение файла для Win32NT

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
                using (var fs = new FileStream(fullPath, FileMode.CreateNew,
                FileAccess.Write, FileShare.Read))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(titleRow);
                    }
                }
        }
        public void WriteFile(string dataFile)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fullPath, true, Encoding.UTF8))
                {
                    sw.WriteLine(dataFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}\n");
            }
        }
        public bool GetBoolReadPathLineFile(string dataFile)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fullPath, Encoding.UTF8))
                {
                    foreach (string line in sr.ReadLine().Split(endRows))
                    {
                        foreach (string pathLine in line.Split(seporRows))
                        {
                            if (pathLine == dataFile) return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}\n");
            }
            return false;
        }
        public bool GetBoolReadLineFile(string dataFile)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fullPath, Encoding.UTF8))
                {
                    foreach (string line in sr.ReadLine().Split(endRows))
                    {
                        if (line == dataFile) return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}\n");
            }
            return false;
        }
        public bool GetBoolReadLineFile(string dataFile, int position)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fullPath, Encoding.UTF8))
                {
                    FileInfo fi = new FileInfo(fullPath);
                    if (fi.Length > 0)
                    {
                        string[] lines = sr.ReadLine().Split(endRows);
                        if (lines[position] == dataFile) return true;
                        else return false;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!!GetBoolReadLineFile!!!\n{ex}\n");
            }
            return false;
        }
        public string GetLineFile(string dataFile, int position)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fullPath, Encoding.UTF8))
                {
                    foreach (string line in sr.ReadLine().Split(endRows))
                    {
                        string[] pathText = line.Split(seporRows);
                        if (pathText[position] == dataFile) return line;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}\n");
            }
            return "NULL";
        }
        public string GetLineFile(string dataFile)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fullPath, Encoding.UTF8))
                {
                    foreach (string line in sr.ReadLine().Split(endRows))
                    {
                        foreach (string pathLine in line.Split(seporRows))
                        {
                            if (pathLine == dataFile) return line;
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
                Console.WriteLine($"{text} task and print");
            else if (SearchExtension(1, "task")) Commands.AddTask();
            else Console.WriteLine(text);
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
