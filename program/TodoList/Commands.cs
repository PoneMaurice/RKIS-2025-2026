// This is the main file, it contains cruical components of the program - PoneMaurice
using System;
using System.Data;
using System.IO.Enumeration;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Task
{
    public class Commands
    {
        const string TaskName = "tasks";
        const string ProfileName = "Profile";
        public string nameTask { get { return TaskName; } }
        const string StringChar = "s";
        const string IntegerChar = "i";
        const string DoubleChar = "f";
        const string TimeChar = "t";
        const string DateChar = "d";
        const string DateAndTime = "dt";
        const string NowDateTime = "ndt";
        static string[] TaskTitle = { "nameTask", "description", "nowDateAndTime", "deadLine" };
        static string[] TaskTypeData = { StringChar, StringChar, NowDateTime, DateAndTime };
        static string[] ProfileTitle = {"name", "soreName", "DOB" , "nowDateAndTime"};
        static string[] ProfileDataType = { StringChar, StringChar, DateChar, NowDateTime };
        const string PrefConfigFile = "_conf";
        public string InputDataType(string text)
        {

            /*Выводит на экран текст и запрашивает у пользователя 
            ввести тип данных и вводит его в бесконечный цикл 
            вводимая пользователем строка проверяеться на наличие 
            такого типа и если он есть возвращает его сокращение*/

            string[,] DataTypePros =
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

                string input = InputString(text);
                int rows = DataTypePros.GetUpperBound(0) + 1;
                for (int i = 0; i < rows; i++)
                {
                    if (DataTypePros[i, 0] == input)
                    {
                        return DataTypePros[i, 1].ToString();
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
            StringBuilder input = new();
            input.Append((Console.ReadLine() ?? FileWriter.stringNull).Trim());
            if (input.ToString() == "") input.Append(FileWriter.stringNull);
            return input.ToString();
        }
        public static int InputIntegerWithMinMax(string text, int min, int max)
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
        public static int InputInteger(string text)
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
        private static string GetDateAndTime()
        {
            /*Запрашивает всю дату в двух вариантах опросом и 
            когда пользователя спрашивают по пунктам, 
            а так же если он не выберет какой-то из вариантов 
            ввода даты то программа автоматически введет "NULL"*/
            string dateAndTime = GetDate() + " " + GetTime();
            return dateAndTime;
        }
        private static string GetDate()
        {
            /*Запрашивает всю дату в двух вариантах опросом и 
            когда пользователя спрашивают по пунктам, 
            а так же если он не выберет какой-то из вариантов 
            ввода даты то программа автоматически введет "NULL"*/
            System.Console.WriteLine("---Ввод даты---");
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
                int year = InputInteger("Введите год: ");
                int month = InputIntegerWithMinMax("Введите месяц: ", 1, 12);
                int day = InputIntegerWithMinMax("Введите день: ", 1,
                    DateTime.DaysInMonth(year, month));
                DateOnly yearMonthDay = new(year, month, day);
                return yearMonthDay.ToShortDateString();
            }
            else Console.WriteLine("Вы не выбрали режим, все даты по default будут 'NULL'");
            return FileWriter.stringNull;
        }
        private static string GetTime()
        {
            /*Запрашивает всю дату в двух вариантах опросом и 
            когда пользователя спрашивают по пунктам, 
            а так же если он не выберет какой-то из вариантов 
            ввода даты то программа автоматически введет "NULL"*/
            System.Console.WriteLine("---Ввод времени---");
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
                int hour = InputIntegerWithMinMax("Введите час: ", 0, 23);
                int minute = InputIntegerWithMinMax("Введите минуты: ", 0, 59);
                TimeOnly hourAndMinute = new(hour, minute);
                return hourAndMinute.ToShortTimeString();
            }
            else
            {
                Console.WriteLine("Вы не выбрали режим, все даты по default будут 'NULL'");
            }
            return FileWriter.stringNull;
        }
        public static void AddTask()
        {
            /*программа запрашивает у пользователя все необходимые ей данные
            и записывает их в файл tasks.csv с нужным форматированием*/
            FileWriter.AddRowInFile(TaskName, TaskTitle, TaskTypeData);
        }
        public static void AddTaskAndPrint()
        {
            /*программа запрашивает у пользователя все необходимые ей данные
            и записывает их в файл tasks.csv с нужным форматированием 
            после чего выводит сообщение о добовлении данных дублируя их 
            пользователю для проверки*/
            FileWriter file = new(TaskName);
            FileWriter.AddRowInFile(TaskName, TaskTitle, TaskTypeData);
            try
            {
                string[] titleRowString = file.GetLineFilePositionRow(0).Split(FormatRows.seporRows);
                string[] rowString = file.GetLineFilePositionRow(file.GetLeghtFile()-1).Split(FormatRows.seporRows);
                for (int i = 0; i < titleRowString.Length; ++i)
                { Console.WriteLine($"{titleRowString[i]}: {rowString[i]}"); }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникла ошибка при записи в файл\n", ex.Message);
            }
        }
        public static void AddConfUserData(string fileName)
        {
            if (fileName == FileWriter.stringNull)
            {
                fileName = InputString("Введите название для файла с данными: ");
            }
            fileName = fileName + PrefConfigFile;
            FileWriter file = new(fileName);

            string fullPathConfig = file.CreatePath(fileName);
            string? askFile = null;
            string searchLine1 = FileWriter.stringNull;
            string searchLine2 = FileWriter.stringNull;
            if (File.Exists(fullPathConfig))
            {
                searchLine1 = file.GetLineFilePositionRow(0);
                searchLine2 = file.GetLineFilePositionRow(1);
                Console.WriteLine($"{searchLine1}\n{searchLine2}");
                askFile = InputString($"Вы точно уверены, что хотите перезаписать конфигурацию?(y/N): ");
            }
            if (askFile == FileWriter.Yes || askFile == null)
            {
                FormatRows titleRow = new(fileName, FormatRows.Type.title), dataTypeRow = new(fileName, FormatRows.Type.dataType);

                while (true)
                {
                    string intermediateResultString =
                        InputString("Введите название пункта титульного оформления файла: ");
                    if (intermediateResultString == "exit" &&
                    titleRow.GetLeghtRow() != 0) break;
                    else if (intermediateResultString == "exit")
                        Console.WriteLine("В титульном оформлении должен быть хотя бы один пункт: ");
                    else titleRow.AddInRow(intermediateResultString);
                }

                string[] titleRowArray = titleRow.Row.ToString().Split(FormatRows.seporRows);

                Commands config = new();
                foreach (string title in titleRowArray)
                {
                    if (title == FormatRows.titleFirstObject) continue;
                    else dataTypeRow.AddInRow(config.InputDataType($"Введите тип данных для строки {title}: "));
                }

                file.TitleRowWriter(titleRow.Row.ToString());
                Console.WriteLine($"{titleRow.Row}\n{dataTypeRow.Row}");

                string lastTitleRow = file.GetLineFilePositionRow(0);
                string lastDataTypeRow = file.GetLineFilePositionRow(1);
                string askTitle = FileWriter.Yes;
                string askDataType = FileWriter.Yes;
                if (lastTitleRow != titleRow.Row.ToString() && lastTitleRow != FileWriter.stringNull)
                    askTitle = InputString($"Титульный лист отличается \nНыняшний: {titleRow.Row}\nПрошлый: {lastTitleRow}\nЗаменить?(y/N): ");
                else if (lastDataTypeRow != dataTypeRow.Row.ToString() && lastDataTypeRow != FileWriter.stringNull)
                    askDataType = InputString($"Конфигурация уже имеется\nНынешняя: {dataTypeRow.Row}\nПрошлая: {lastDataTypeRow}\nЗаменить?(y/N): ");
                if (askTitle == FileWriter.Yes || askDataType == FileWriter.Yes)
                {
                    file.WriteFile(titleRow.Row.ToString(), false);
                    file.WriteFile(dataTypeRow.Row.ToString(), true);
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
            FileWriter fileConf = new(nameData + PrefConfigFile);
            FileWriter file = new(nameData);

            string fullPathConfig = fileConf.CreatePath(nameData + PrefConfigFile);
            if (File.Exists(fullPathConfig))
            {
                string titleRow = fileConf.GetLineFilePositionRow(0);
                string dataTypeRow = fileConf.GetLineFilePositionRow(1);
                string[] titleRowArray = titleRow.Split(FormatRows.seporRows);
                string[] dataTypeRowArray = dataTypeRow.Split(FormatRows.seporRows);

                string row = GetRowOnTitleAndConfig(titleRowArray, dataTypeRowArray, nameData);

                file.TitleRowWriter(titleRow);
                string testTitleRow = file.GetLineFilePositionRow(0);
                if (testTitleRow != titleRow)
                    file.WriteFile(titleRow, false);
                file.WriteFile(row, true);
            }
            else Console.WriteLine($"Сначала создайте конфигурацию или проверьте правильность написания названия => '{nameData}'");
        }
        public static string GetRowOnTitleAndConfig(string[] titleRowArray, string[] dataTypeRowArray, string nameData = TaskName)
        {
            FormatRows row = new(nameData);
            for (int i = 0; i < titleRowArray.Length; i++)
            {
                switch (dataTypeRowArray[i])
                {
                    case Commands.StringChar:
                        row.AddInRow(InputString($"введите {titleRowArray[i]}: "));
                        break;
                    case Commands.IntegerChar:
                        row.AddInRow(InputInteger($"введите {titleRowArray[i]}: ").ToString());
                        break;
                    case Commands.DoubleChar:
                        row.AddInRow(InputInteger($"введите {titleRowArray[i]}: ").ToString());
                        break;
                    case Commands.DateChar:
                        Console.WriteLine($"---ввод {titleRowArray[i]}---");
                        row.AddInRow(GetDate());
                        break;
                    case Commands.TimeChar:
                        Console.WriteLine($"---ввод {titleRowArray[i]}---");
                        row.AddInRow(GetTime());
                        break;
                    case Commands.DateAndTime:
                        Console.WriteLine($"---ввод {titleRowArray[i]}---");
                        row.AddInRow(GetDateAndTime());
                        break;
                    case Commands.NowDateTime:
                        row.AddInRow(FormatRows.GetNowDateTime());
                        break;
                }
            }
            return row.Row.ToString();
        }
        public static void ClearAllTasks()
        {
            if (InputString("Вы уверены что хотите очистить весь файл task? (y/N): ") == FileWriter.Yes)
            {
                string fileName = TaskName;

                FormatRows titleRow = new(fileName, FormatRows.Type.title);
                FileWriter file = new(fileName);
                string[] titleRowArray = TaskTitle;
                foreach (string pathTitleRow in titleRowArray)
                    titleRow.AddInRow(pathTitleRow);
                file.WriteFile(titleRow.Row.ToString(), false);
            }
            else System.Console.WriteLine("Будте внимателны");
        }
        public void ClearPartTask(string text) // недописан обязательно исправить
        {
            string fileName = TaskName;
            FileWriter file = new(fileName);

            string[] titleRowArray = file.GetLineFilePositionRow(0).Split(FormatRows.seporRows);
            Dictionary<int, bool> tableClear = new Dictionary<int, bool>();

            System.Console.WriteLine($"Выберете в каком столбце искать {text} (y/N): ");
            for (int i = 0; i < titleRowArray.Length; ++i)
            {
                if (InputString($"{titleRowArray[i]}: ") == FileWriter.Yes)
                    tableClear.Add(i, true);
                else tableClear.Add(i, false);
            }
        }
        public void SearchPartData(string fileName, string text = FileWriter.stringNull)
        {
            if (fileName == FileWriter.stringNull)
                fileName = InputString("Ведите название файла: ");
            if (text == FileWriter.stringNull)
                text = InputString("Поиск: ");

            FileWriter file = new(fileName);


            if (File.Exists(file.fullPath))
            {
                string[] titleRowArray = file.GetLineFilePositionRow(0).Split(FormatRows.seporRows);
                Dictionary<int, bool> tableClear = new Dictionary<int, bool>();


                System.Console.WriteLine($"Выберете в каком столбце искать {text} (y/N): ");
                for (int i = 0; i < titleRowArray.Length; ++i)
                {
                    if (InputString($"{titleRowArray[i]}: ") == FileWriter.Yes)
                        tableClear.Add(i, true);
                    else tableClear.Add(i, false);
                }
                foreach (int i in tableClear.Keys)
                {
                    if (tableClear[i])
                        Console.WriteLine(file.GetLineFileDataOnPositionInRow(text, i));
                }
            }
            else System.Console.WriteLine(fileName + ": такого файла не существует.");
        }
        public static void PrintData(string fileName)
        {
            if (fileName == FileWriter.stringNull)
                fileName = InputString("Ведите название файла: ");

            FileWriter file = new(fileName);

            try
            {
                using (StreamReader reader = new StreamReader(file.fullPath, Encoding.UTF8))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}\n");
            }
        }
        public static void AddProfile()
        {
            FileWriter.AddRowInFile(ProfileName, ProfileTitle, ProfileDataType);
        }
    }
}
