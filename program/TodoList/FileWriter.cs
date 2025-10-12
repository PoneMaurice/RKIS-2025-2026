// This file contains everything related to generating and reading paths, files - PoneMaurice
using System.Formats.Asn1;
using System.Text;
namespace Task
{
    public class OpenFile
    {
        public string fullPath;
        public string nameFile;
        public OpenFile(string fileName)
        {
            nameFile = fileName;
            fullPath = CreatePath(nameFile);
        }
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
        public static string GetPathToZhopa()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string[] huis = baseDirectory.Split("/");
            StringBuilder huiBolshoy = new();
            foreach (string indexHui in huis)
            {
                if (indexHui != "bin")
                {
                    huiBolshoy.Append(indexHui + "/");
                }
                else
                {
                    break;
                }
            }
            return huiBolshoy.ToString();
        }
        public static string StringFromFileInMainFolder(string fileName)
        {
            string huiBolshoy = OpenFile.GetPathToZhopa();
            string sex = Path.Join(huiBolshoy, fileName);
            return File.ReadAllText(sex);
        }
        public string TitleRowWriter(string titleRow) // Function for writing rows in tasks titles - PoneMaurice
        {
            /*Создаёт титульное оформление в файле 
            при условии что это новый файл*/
            string fullPath = CreatePath(nameFile);
            if (!File.Exists(fullPath))
                using (var fs = new FileStream(fullPath, FileMode.CreateNew,
                FileAccess.Write, FileShare.Read))
                {
                    using (var sw = new System.IO.StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.WriteLine(titleRow);
                    }
                }
            return fullPath;
        }
        public void WriteFile(string dataFile, bool noRewrite = true)
        {
            /*Запись строки в конец файла при условии что 
            аргумент "noRewrite" равен true, а иначе файл будет перезаписан*/
            try
            {

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fullPath, noRewrite, Encoding.UTF8))
                {
                    sw.WriteLine(dataFile);
                }

            }
            catch (Exception)
            {
                WriteToConsole.RainbowText("В мире произошло что то плохое", ConsoleColor.Red);
            }
        }
        public string GetLineFileDataOnPositionInRow(string dataFile, int positionInRow)
        {
            /*Возвращает строку если ее элемент по заданной позиции 
            соответствует введенным нами данным*/
            try
            {
                using (StreamReader reader = new StreamReader(fullPath, Encoding.UTF8))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] pathLine = line.Split(ConstProgram.SeparRows);
                        if (pathLine.Length > positionInRow)
                        {
                            if (pathLine[positionInRow] == dataFile)
                                return line;
                        }
                    }
                }
            }
            catch (Exception)
            {
                WriteToConsole.RainbowText("Разраб отдыхает, прошу понять", ConsoleColor.Red);
                WriteToConsole.RainbowText("^если что там ошибка чтения файла", ConsoleColor.Red);
            }
            return ConstProgram.StringNull;
        }
        public string GetLineFilePositionRow(int positionRow)
        {
            /*Возвращает строку если ее элемент по заданной позиции 
            соответствует введенным нами данным*/
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
            catch (Exception)
            {
                WriteToConsole.RainbowText("не найдено, что именно я тоже не знаю", ConsoleColor.Red);
            }
            return ConstProgram.StringNull;
        }
        public string GetLineFileData(string dataFile)
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
            return ConstProgram.StringNull;
        }
        public int GetLengthFile()
        {
            int numLine = 0;
            try
            {
                if (File.Exists(fullPath))
                {
                    using (StreamReader reader = new StreamReader(fullPath, Encoding.UTF8))
                    {
                        string? line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            ++numLine;
                        }
                    }
                }
                else return 1;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return numLine;
        }
        public static void AddRowInFile(string nameFile, string[] titleRowArray, string[] dataTypeRowArray)
        {
            try
            {
                OpenFile file = new(nameFile);
                FormatterRows titleRow = new(nameFile, FormatterRows.Type.title);
                string row = Commands.GetRowOnTitleAndConfig(titleRowArray, dataTypeRowArray, ConstProgram.TaskName);
                titleRow.AddInRow(titleRowArray);
                file.TitleRowWriter(titleRow.Row.ToString());
                file.WriteFile(row);
                WriteToConsole.RainbowText("Задание успешно записано", ConsoleColor.Green);
            }
            catch (Exception)
            {
                WriteToConsole.RainbowText("Произошла ошибка при записи файла", ConsoleColor.Red);
            }
        }
        public void RecordingData(string[] rows)
        {
            OpenFile file = new(nameFile);
            string titleRow = rows[0];
            file.WriteFile(titleRow, false);
            foreach (string row in rows[1..])
            {
                file.WriteFile(row);
            }
        }
        public void EditingRow(string requiredData, string modifiedData, int indexColumn, int numberOfIterations = 1) // не тестилась
        {
            string data = File.ReadAllText(fullPath);
            string[] rows = data.Split("\n");
            bool searchWasSuccessful = false;
            int counter = 0;
            if (rows.Length > indexColumn)
            {
                for (int i = 1; i < rows.Length; ++i)
                {
                    string[] partsText = rows[i].Split(ConstProgram.SeparRows);
                    if (partsText[indexColumn] == requiredData)
                    {
                        FormatterRows buildRow = new(nameFile);
                        partsText[indexColumn] = modifiedData;
                        buildRow.AddInRow(partsText);
                        rows[i] = buildRow.Row.ToString();
                        searchWasSuccessful = true;
                        ++counter;
                        if (counter >= numberOfIterations)
                        {
                            break;
                        }
                    }
                }
                if (searchWasSuccessful)
                {
                    RecordingData(rows);
                    if (counter == 1)
                    {
                        WriteToConsole.RainbowText($"Строка была успешно перезаписана", ConsoleColor.Green);
                    }
                    else
                    {
                        WriteToConsole.RainbowText($"Было перезаписано '{counter}' строк", ConsoleColor.Green);
                    }
                }
                else
                {
                    WriteToConsole.RainbowText($"В файле нет объекта соответствующего '{requiredData}'", ConsoleColor.Yellow);
                }
            }
            else
            {
                WriteToConsole.RainbowText($"Index слишком большой максимальное значение {rows.Count()}", ConsoleColor.Red);
            }
        }
        public void ClearRow(string requiredData, int indexColumn, int numberOfIterations = 1)
        {
            string data = File.ReadAllText(fullPath);
            List<string> rows = data.Split("\n").ToList();
            bool searchWasSuccessful = false;
            int counter = 0;
            if (rows.Count() > indexColumn)
            {
                for (int i = 1; i < rows.Count(); ++i)
                {
                    string[] partsText = rows[i].Split(ConstProgram.SeparRows);
                    if (partsText[indexColumn] == requiredData)
                    {
                        searchWasSuccessful = true;
                        ++counter;
                        rows.RemoveAt(i);
                        if (counter >= numberOfIterations)
                        {
                            break;
                        }
                    }
                }
                if (searchWasSuccessful)
                {
                    RecordingData(rows.ToArray());
                    if (counter == 1)
                    {
                        WriteToConsole.RainbowText($"Строка была успешно удалена", ConsoleColor.Green);
                    }
                    else
                    {
                        WriteToConsole.RainbowText($"Было удалено {counter} строк", ConsoleColor.Green);
                    }
                }
                else
                {
                    WriteToConsole.RainbowText($"В файле нет объекта соответствующего '{requiredData}'", ConsoleColor.Yellow);
                }
            }
            else
            {
                WriteToConsole.RainbowText($"Index слишком большой максимальное значение {rows.Count()}", ConsoleColor.Red);
            }
        }
    }
}