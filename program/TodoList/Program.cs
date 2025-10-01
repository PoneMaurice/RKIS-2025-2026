// This is the main file, it contains cruical components of the program - PoneMaurice
using System;
using System.Data;
namespace Task
{
    public class Commands
    {
        const string StringChar = "s";
        const string IntegerChar = "i";
        const string DoubleChar = "f";
        const string TimeChar = "t";
        const string DateChar = "d";
        const string DateAndTime = "dt";
        const string NowDateTime = "ndt";
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
