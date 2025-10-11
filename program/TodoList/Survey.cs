//This file contains every command and option for program and their logic - PoneMaurice 
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Nodes;

namespace Task
{
    public class Survey
    {
        public void GlobalCommand(string Text)
        {
            string ask = Commands.InputString(Text);
            SearchCommandOnJson commandLine = new(ask.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            switch (commandLine.commandOut)
            {
                case "add":
                    if (commandLine.optionsOut.Length > 0)
                    {
                        if (commandLine.SearchOption(["help"]))
                        {
                            AddHelp();
                        }
                        else if (commandLine.SearchOption(["task"]))
                        {
                            Commands.AddTask();
                        }
                        else if (commandLine.SearchOption(["config"]))
                        {
                            Commands.AddConfUserData(commandLine.nextTextOut);
                        }
                        else if (commandLine.SearchOption(["profile"]))
                        {
                            Commands.AddProfile();
                        }
                        else {Commands.AddUserData(commandLine.nextTextOut);}
                    }
                    else {Commands.AddUserData(commandLine.nextTextOut);}
                    break;

                case "profile":
                    if (commandLine.optionsOut.Length > 0)
                    {
                        if (commandLine.SearchOption(["help"]))
                        {
                            ProfileHelp();
                        }
                        else if (commandLine.SearchOption(["add"]))
                        {
                            Commands.AddProfile();
                        }
                    }
                    else {} ///////////////////////////////////////////////////////////
                    break;

                case "print":
                    if (commandLine.optionsOut.Length > 0)
                    {
                        if (commandLine.SearchOption(["help"]))
                        {
                            PrintHelp();
                        }
                        else if (commandLine.SearchOption(["task"]))
                        {
                            Commands.PrintData(Commands.TaskName);
                        }
                        else if (commandLine.SearchOption(["config"]))
                        {
                            Commands.PrintData(commandLine.nextTextOut + ConstProgram.PrefConfigFile);
                        }
                        else if (commandLine.SearchOption(["profile"]))
                        {
                            Commands.PrintData(Commands.ProfileName);
                        }
                        else if (commandLine.SearchOption(["captions"])){
                            Commands.WriteCaption();
                        } 
                        else { Commands.PrintData(commandLine.nextTextOut); }
                    }
                    else { Commands.PrintData(commandLine.nextTextOut); }
                    break;

                case "search":
                    if (commandLine.optionsOut.Length > 0)
                    {
                        if (commandLine.SearchOption(["help"]))
                        {
                            SearchHelp();
                        }
                        else if (commandLine.SearchOption(["task"]))
                        {

                        }
                        else if (commandLine.SearchOption(["config"]))
                        {

                        }
                        else if (commandLine.SearchOption(["profile"]))
                        {

                        }
                        else if (commandLine.SearchOption(["numbering"]))
                        {

                        }
                        else { }
                    }
                    else {Commands.SearchPartData();}
                    break;

                case "clear":
                    if (commandLine.optionsOut.Length > 0)
                    {
                        if (commandLine.SearchOption(["help"]))
                        {

                        }
                        else if (commandLine.SearchOption(["task"]))
                        {

                        }
                        else if (commandLine.SearchOption(["config"]))
                        {

                        }
                        else if (commandLine.SearchOption(["profile"]))
                        {

                        }
                        else if (commandLine.SearchOption(["console"]))
                        {
                            System.Console.WriteLine("CCCCCCClear");
                            Console.Clear();
                        }
                        else { }
                    }
                    else { }
                    break;

                case "help":
                    Help();
                    break;

                case "exit":
                    Environment.Exit(0);
                    break;
            }
            
        }
        public void ProfileHelp()
        {
            Console.WriteLine("Команда для работы с профилями;");
            Console.WriteLine("При простом вызове, выводится первый добавленный пользователь: profile;");
            Console.WriteLine("При использовании как аргумент с командой add - добавляется новый пользователь: add profile;");
        }
        public void Help()
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
        public void AddHelp()
        {
            Console.WriteLine("add - Добавляет записи(задания);");
            Console.WriteLine("Добавляет запись по базовой конфигурации: add task;");
            Console.WriteLine("Добавляет файл конфигурации: add config <File>;");
            Console.WriteLine("Добавляет запись по заранее созданной конфигурации: add <File>;");
            Console.WriteLine("Создаёт новый профиль: add profile;");
            Console.WriteLine("При добавлении print в конце команды, выводится добавленный текст");
        }
        public void PrintHelp()
        {
            Console.WriteLine("print - Команда позволяющая получить содержимое файла;");
            Console.WriteLine("Примеры: print task; print <File>;");
            Console.WriteLine("Также может использоваться как аргумент в командах add task print/add <File> print,");
            Console.WriteLine("после создания записи её содержимое будет выведено в консоль;");
        }
        public void SearchHelp()
        {
            Console.WriteLine("search - Ищет все идентичные строчки в файле;");
        }
    }
}