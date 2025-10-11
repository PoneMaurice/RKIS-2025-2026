// This is the main file, it contains cruical components of the program - PoneMaurice
using System.Text;

namespace Task
{
    public class Commands
    {
        public static void AddTask()
        {
            /*программа запрашивает у пользователя все необходимые ей данные
            и записывает их в файл tasks.csv с нужным форматированием*/
            OpenFile.AddRowInFile(ConstProgram.TaskName, ConstProgram.TaskTitle, ConstProgram.TaskTypeData);
        }
        public static void AddTaskAndPrint()
        {
            /*программа запрашивает у пользователя все необходимые ей данные
            и записывает их в файл tasks.csv с нужным форматированием 
            после чего выводит сообщение о добавлении данных дублируя их 
            пользователю для проверки*/
            OpenFile file = new(ConstProgram.TaskName);
            OpenFile.AddRowInFile(ConstProgram.TaskName, ConstProgram.TaskTitle, ConstProgram.TaskTypeData);
            try
            {
                string[] titleRowString = file.GetLineFilePositionRow(0).Split(ConstProgram.SeparRows);
                string[] rowString = file.GetLineFilePositionRow(file.GetLengthFile() - 1).Split(ConstProgram.SeparRows);
                for (int i = 0; i < titleRowString.Length; ++i)
                { Console.WriteLine($"{titleRowString[i]}: {rowString[i]}"); }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникла ошибка при записи в файл\n", ex.Message);
            }
        }
        public static void AddConfUserData(string fileName = "")
        {
            if (fileName == "")
            {
                fileName = Input.String("Введите название для файла с данными: ");
            }
            fileName = fileName + ConstProgram.PrefConfigFile;
            OpenFile file = new(fileName);

            string fullPathConfig = file.CreatePath(fileName);
            string? askFile = null;
            string searchLine1 = ConstProgram.StringNull;
            string searchLine2 = ConstProgram.StringNull;
            if (File.Exists(fullPathConfig))
            {
                searchLine1 = file.GetLineFilePositionRow(0);
                searchLine2 = file.GetLineFilePositionRow(1);
                Console.WriteLine($"{searchLine1}\n{searchLine2}");
                askFile = Input.String($"Вы точно уверены, что хотите перезаписать конфигурацию?(y/N): ");
            }
            if (askFile == ConstProgram.Yes || askFile == null)
            {
                FormatterRows titleRow = new(fileName, FormatterRows.Type.title), dataTypeRow = new(fileName, FormatterRows.Type.dataType);

                while (true)
                {
                    string intermediateResultString =
                        Input.String("Введите название пункта титульного оформления файла: ");
                    if (intermediateResultString == "exit" &&
                    titleRow.GetLengthRow() != 0) break;
                    else if (intermediateResultString == "exit")
                        Console.WriteLine("В титульном оформлении должен быть хотя бы один пункт: ");
                    else titleRow.AddInRow(intermediateResultString);
                }

                string[] titleRowArray = titleRow.Row.ToString().Split(ConstProgram.SeparRows);

                foreach (string title in titleRowArray)
                {
                    if (title == ConstProgram.TitleFirstObject) continue;
                    else dataTypeRow.AddInRow(Input.DataType($"Введите тип данных для строки {title}: "));
                }

                file.TitleRowWriter(titleRow.Row.ToString());
                Console.WriteLine($"{titleRow.Row}\n{dataTypeRow.Row}");

                string lastTitleRow = file.GetLineFilePositionRow(0);
                string lastDataTypeRow = file.GetLineFilePositionRow(1);
                string askTitle = ConstProgram.Yes;
                string askDataType = ConstProgram.Yes;
                if (lastTitleRow != titleRow.Row.ToString() && lastTitleRow != ConstProgram.StringNull)
                    askTitle = Input.String($"Титульный лист отличается \nНыняшний: {titleRow.Row}\nПрошлый: {lastTitleRow}\nЗаменить?(y/N): ");
                else if (lastDataTypeRow != dataTypeRow.Row.ToString() && lastDataTypeRow != ConstProgram.StringNull)
                    askDataType = Input.String($"Конфигурация уже имеется\nНынешняя: {dataTypeRow.Row}\nПрошлая: {lastDataTypeRow}\nЗаменить?(y/N): ");
                if (askTitle == ConstProgram.Yes || askDataType == ConstProgram.Yes)
                {
                    file.WriteFile(titleRow.Row.ToString(), false);
                    file.WriteFile(dataTypeRow.Row.ToString());
                }
            }
            else
            {
                System.Console.WriteLine("Будет использована конфигурация: ");
                System.Console.WriteLine($"{searchLine1}\n{searchLine2}");
            }
        }
        public static void AddUserData(string nameData = "")
        {
            if (nameData == "")
            {
                nameData = Input.String("Введите название для файла с данными: ");
            }
            OpenFile fileConf = new(nameData + ConstProgram.PrefConfigFile);
            OpenFile file = new(nameData);

            string fullPathConfig = fileConf.CreatePath(nameData + ConstProgram.PrefConfigFile);
            if (File.Exists(fullPathConfig))
            {
                string titleRow = fileConf.GetLineFilePositionRow(0);
                string dataTypeRow = fileConf.GetLineFilePositionRow(1);
                string[] titleRowArray = titleRow.Split(ConstProgram.SeparRows);
                string[] dataTypeRowArray = dataTypeRow.Split(ConstProgram.SeparRows);

                string row = GetRowOnTitleAndConfig(titleRowArray, dataTypeRowArray, nameData);

                file.TitleRowWriter(titleRow);
                string testTitleRow = file.GetLineFilePositionRow(0);
                if (testTitleRow != titleRow)
                    file.WriteFile(titleRow, false);
                file.WriteFile(row);
            }
            else Console.WriteLine($"Сначала создайте конфигурацию или проверьте правильность написания названия => '{nameData}'");
        }
        public static string GetRowOnTitleAndConfig(string[] titleRowArray, string[] dataTypeRowArray, string nameData = ConstProgram.TaskName)
        {
            FormatterRows row = new(nameData);
            for (int i = 0; i < titleRowArray.Length; i++)
            {
                switch (dataTypeRowArray[i])
                {
                    case "s":
                        row.AddInRow(Input.String($"введите {titleRowArray[i]}: "));
                        break;
                    case "i":
                        row.AddInRow(Input.Integer($"введите {titleRowArray[i]}: ").ToString());
                        break;
                    case "pos_i":
                        row.AddInRow(Input.PositiveInteger($"введите {titleRowArray[i]}: ").ToString());
                        break;
                    case "f":
                        row.AddInRow(Input.Float($"введите {titleRowArray[i]}: ").ToString());
                        break;
                    case "pos_f":
                        row.AddInRow(Input.PositiveFloat($"введите {titleRowArray[i]}: ").ToString());
                        break;
                    case "d":
                        Console.WriteLine($"---ввод {titleRowArray[i]}---");
                        row.AddInRow(Input.Date());
                        break;
                    case "t":
                        Console.WriteLine($"---ввод {titleRowArray[i]}---");
                        row.AddInRow(Input.Time());
                        break;
                    case "dt":
                        Console.WriteLine($"---ввод {titleRowArray[i]}---");
                        row.AddInRow(Input.DateAndTime());
                        break;
                    case "ndt":
                        row.AddInRow(Input.NowDateTime());
                        break;
                    
                    
                }
            }
            return row.Row.ToString();
        }
        public static void ClearAllFile(string fileName = ConstProgram.StringNull)
        {
            if (Input.String("Вы уверены что хотите очистить весь файл task? (y/N): ") == ConstProgram.Yes)
            {
                if (fileName == ConstProgram.StringNull)
                {
                    fileName = Input.String("Введите название файла: ");
                }
                OpenFile file = new(fileName);
                if (File.Exists(file.fullPath))
                {
                    file.WriteFile(file.GetLineFilePositionRow(0), false);
                }
                else WriteToConsole.RainbowText(fileName + ": такого файла не существует.", ConsoleColor.Red);
            }
            else System.Console.WriteLine("Буде внимательны");
        }
        public void ClearPartTask(string text) // недописанн обязательно исправить
        {
            string fileName = ConstProgram.TaskName;
            OpenFile file = new(fileName);

            string[] titleRowArray = file.GetLineFilePositionRow(0).Split(ConstProgram.SeparRows);
            Dictionary<int, bool> tableClear = new Dictionary<int, bool>();

            System.Console.WriteLine($"Выберете в каком столбце искать {text} (y/N): ");
            for (int i = 0; i < titleRowArray.Length; ++i)
            {
                if (Input.String($"{titleRowArray[i]}: ") == ConstProgram.Yes)
                    tableClear.Add(i, true);
                else tableClear.Add(i, false);
            }
        }
        public static void SearchPartData(string fileName = ConstProgram.StringNull, string text = ConstProgram.StringNull)
        {
            if (fileName == ConstProgram.StringNull)
                {
                fileName = Input.String("Ведите название файла: ");
                }
            if (text == ConstProgram.StringNull)
                {
                text = Input.String("Поиск: ");
                }

            OpenFile file = new(fileName);
            
            if (File.Exists(file.fullPath))
            {
                string[] titleRowArray = file.GetLineFilePositionRow(0).Split(ConstProgram.SeparRows);
                int[] tableClear = new int[titleRowArray.Length];
                Array.Fill(tableClear, -1);
                System.Console.WriteLine($"Выберете в каком столбце искать {text} (y/N): ");
                for (int i = 0; i < titleRowArray.Length; ++i)
                {
                    if (Input.String($"{titleRowArray[i]}: ") == ConstProgram.Yes)
                        tableClear[i] = i;
                }
                foreach (int i in tableClear)
                {
                    if (i != -1)
                        Console.WriteLine(file.GetLineFileDataOnPositionInRow(text, i));
                }
            }
            else WriteToConsole.RainbowText(fileName + ": такого файла не существует.", ConsoleColor.Red);
        }
        public static void PrintData(string fileName = ConstProgram.StringNull)
        {
            if (fileName == ConstProgram.StringNull)
            {
                fileName = Input.String("Ведите название файла: ");
            }

            OpenFile file = new(fileName);

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
            catch (Exception)
            {
                WriteToConsole.RainbowText("Произошла ошибка при чтении файла", ConsoleColor.Red);
            }
        }
        public static void AddProfile()
        {
            OpenFile.AddRowInFile(ConstProgram.ProfileName, ConstProgram.ProfileTitle, ConstProgram.ProfileDataType);
        }
        public static void WriteCaption()
        {
            /*спрашивает и выводит текст субтитров созданный 
            методом CompText*/
            System.Console.WriteLine("За работу ответственны:");
            System.Console.WriteLine("\tШевченко Э. - README, исходный код, некоторые аспекты git;");
            System.Console.WriteLine("\tТитов М. - github, .gitignore, некоторый части исходного кода;");
        }
        public static void ProfileHelp()
        {
            Console.WriteLine("Команда для работы с профилями;");
            Console.WriteLine("При простом вызове, выводится первый добавленный пользователь: profile;");
            Console.WriteLine("При использовании как аргумент с командой add - добавляется новый пользователь: add profile;");
        }
        public static void Help()
        {
            Console.WriteLine("Данная программа позволяет пользователю создавать свой список заданий и контролировать их выполнение");
            Console.WriteLine("help - Выводит помощь по программе и её командам например: add help");
            Console.WriteLine("profile - Команда для работы с профилями");
            Console.WriteLine("add - Добавляет запись по базовой конфигурации: add task;");
            Console.WriteLine("Добавляет файл конфигурации: add config <File>;");
            Console.WriteLine("Добавляет запись по заранее созданной конфигурации: add <File>;");
            Console.WriteLine("clear - очищает выбранный файл");
            Console.WriteLine("search - Ищет все идентичные строчки в файле");
            Console.WriteLine("exit - Выход из программы либо из текущего действия");
            Console.WriteLine("print - Выводит всё содержимое файла");
        }
        public static void AddHelp()
        {
            Console.WriteLine("add - Добавляет записи(задания);");
            Console.WriteLine("Добавляет запись по базовой конфигурации: add task;");
            Console.WriteLine("Добавляет файл конфигурации: add config <File>;");
            Console.WriteLine("Добавляет запись по заранее созданной конфигурации: add <File>;");
            Console.WriteLine("Создаёт новый профиль: add profile;");
            Console.WriteLine("При добавлении print в конце команды, выводится добавленный текст");
        }
        public static void PrintHelp()
        {
            Console.WriteLine("print - Команда позволяющая получить содержимое файла;");
            Console.WriteLine("Примеры: print task; print <File>;");
            Console.WriteLine("Также может использоваться как аргумент в командах add task print/add <File> print,");
            Console.WriteLine("после создания записи её содержимое будет выведено в консоль;");
        }
        public static void SearchHelp()
        {
            Console.WriteLine("search - Ищет все идентичные строчки в файле;");
        }
    }
}
