//This file contains every command and option for program and their logic - PoneMaurice 
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Nodes;

namespace Task
{
    public class Survey
    {
        public static int counter = 0;
        public string nowText = "";


        public void GlobalCommand(string[] splitText)
        {
            SearchCommandOnJson commandLine = new(splitText);
            switch (commandLine.commandOut)
            {
                case "add":
                    System.Console.WriteLine("add");
                    break;
                case "profile":
                    break;
                case "print":
                    break;
                case "search":
                    break;
                case "clear":
                    break;
                case "help":
                    break;
                case "exit":
                    break;
            }
            
        }
        public void ProfileHelp()
        {
            StringBuilder text = new();
            text.Append("Команда для работы с профилями;\n");
            text.Append("При простом вызове, выводится первый добавленный пользователь: profile;\n");
            text.Append("При использовании как аргумент с командой add - добавляется новый пользователь: add profile;\n");
            Console.WriteLine(text.ToString());
            // else Commands.PrintProfile();
        }
        public void Help()
        {
            StringBuilder text = new();
            text.Append("Данная программа позволяет пользователю создавать свой список заданий и контролировать их выполнение\n");
            text.Append("help - Выводит помощь по программе и её командам например: add help\n");
            text.Append("profile - Команда для работы с профилями\n");
            text.Append("add - Добавляет запись по базовой конфигурации: add task;\nДобавляет файл конфигурации: add config <File>;\nДобавляет запись по заранее созданной конфигурации: add <File>;\n");
            text.Append("clear - очищает выбранный файл\n");
            text.Append("search - Ищет все идентичные строчки в файле\n");
            text.Append("exit - Выход из программы либо из текущего действия\n");
            text.Append("print - Выводит всё содержимое файла\n");
            Console.WriteLine(text.ToString());
        }
        public void AddHelp()
        {
            StringBuilder text = new();
            text.Append("add - Добавляет записи(задания);\n");
            text.Append("Добавляет запись по базовой конфигурации: add task;\n");
            text.Append("Добавляет файл конфигурации: add config <File>;\n");
            text.Append("Добавляет запись по заранее созданной конфигурации: add <File>;\n");
            text.Append("Создаёт новый профиль: add profile;\n");
            text.Append("При добавлении print в конце команды, выводится добавленный текст\n");
            Console.WriteLine(text.ToString());
            // else if (SearchExtension("task") && SearchExtension("print"))
            //     global::Task.Commands.AddTaskAndPrint();
            // else if (SearchExtension(1, "task")) global::Task.Commands.AddTask();
            // else if (SearchExtension(1, "config")) global::Task.Commands.AddConfUserData(nowText);
            // else if (SearchExtension(1, "profile")) global::Task.Commands.AddProfile();
            // else global::Task.Commands.AddUserData(nowText);
        }
        public void Task()
        {
            Commands command = new();
            StringBuilder text = new();
            text.Append("task - Служебная команда для работы со стандартным конфигурационным файлом;\n");
            text.Append("task - Используется как аргумент для таких команд как: add, clear, search, print;\n");
            text.Append("add - Добавляет запись по базовой конфигурации: add task;\n");
            text.Append("clear - Удаляет все записи из файла tasks: clear task;\n");
            text.Append("search - Ищет все идентичные строчки в файле: search task;\n");
            text.Append("print - Выводит всё содержимое файла: print task;\n");
            // if (SearchExtension(1, "help")) Console.WriteLine(text.ToString());
            // else if (SearchExtension(1, "clear") && nowText == FileWriter.stringNull)
            //     global::Task.Commands.ClearAllTasks();
            // else if (SearchExtension(1, "search")) command.SearchPartData(nowText, command.nameTask);
            // else global::Task.Commands.PrintData(command.nameTask);
        }
        public void PrintHelp()
        {
            StringBuilder text = new();
            text.Append("print - Команда позволяющая получить содержимое файла;\n");
            text.Append("Примеры: print task; print <File>;\n");
            text.Append("Также может использоваться как аргумент в командах add task print/add <File> print,\nпосле создания записи её содержимое будет выведено в консоль;\n");
            Console.WriteLine(text.ToString());
            // else global::Task.Commands.PrintData(nowText);
        }
        public void SearchHelp()
        {
            Commands command = new();
            StringBuilder text = new();
            text.Append("search - Ищет все идентичные строчки в файле;\n");
            Console.WriteLine(text.ToString());
            // else command.SearchPartData(FileWriter.stringNull, nowText);
        }
        public void ProceStr(string text)
        {
            string ask = global::Task.Commands.InputString(text);
            string[] partsText = ask.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            GlobalCommand(partsText);
        }
        public static string AssociationString(string[] sepText)
        {
            StringBuilder text = new();
            bool noneText = true;
            for (int i = counter; i < sepText.Length; ++i)
            {
                if (noneText)
                {
                    text.Append(sepText[i]);
                    noneText = false;
                }
                else text.Append($" {sepText[i]}");
            }
            if (text.ToString() == "") text.Append(FileWriter.stringNull);
            return text.ToString();
        }
    }
}