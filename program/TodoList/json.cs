using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;

namespace Task
{
    class AllCommands
    {
        public Command[]? Commands { get; set; }
    }
    class Command
    {
        public string? Name { get; set; }
        public Option[]? Options { get; set; }
    }
    class Option
    {
        public string? Name { get; set; } = null;
        public string? Long { get; set; } = null;
        public string? Short { get; set; } = null;
    }

    class SearchCommandOnJson
    {
        static string nameFileJson = "commands.json";
        static string jsonString = File.ReadAllText(nameFileJson);
        static AllCommands? openJsonFile = JsonSerializer.Deserialize<AllCommands?>(jsonString);
        public static void hyi()
        {
            foreach (var command in openJsonFile.Commands)
            {
                System.Console.WriteLine(command.Name);
                if (command.Options != null)
                {
                    foreach (var option in command.Options)
                    {
                        if (option.Name == null) System.Console.WriteLine("Name == null");
                        else System.Console.WriteLine("\t" + option.Name);
                        if (option.Long == null) System.Console.WriteLine("\tLong == null");
                        else System.Console.WriteLine("\t" + option.Long);
                        if (option.Short == null) System.Console.WriteLine("\tShort == null");
                        else System.Console.WriteLine("\t" + option.Short);
                        System.Console.WriteLine();
                    }
                }
            }
        }
        string[] pathsText;
        public SearchCommandOnJson(string[] text)
        {
            pathsText = text;
        }
        public bool SearchOptionInArgs(string nameCommand, string[] args) {
            if (args.Length > 0 && openJsonFile != null &&
            openJsonFile.Commands != null)
            {
                int count = args.Length;
                for (int i = 0; i < args.Length; ++i)
                {
                    for (int j = 0; j < openJsonFile.Commands.Length; j++)
                    {
                        if (openJsonFile.Commands[j].Options != null && 
                        openJsonFile.Commands[j].Name == nameCommand)
                        {
                            for (int k = 0; k < openJsonFile.Commands[j].Options.Length; ++k)
                            {
                                if (args[i] == openJsonFile.Commands[j].Options[k].Name)
                                {
                                    --count;
                                }
                            }
                        }
                    }
                }
                if (count == 0) return true;
            }
            return false;
        }

        public bool SearchCommands(string nameCommand)
        {
            string? NameCommand = null;
            if (openJsonFile != null && openJsonFile.Commands != null)
            {
                foreach (var commands in openJsonFile.Commands)
                {
                    if (nameCommand == commands.Name)
                    {
                        NameCommand = commands.Name;
                    }
                }
                try
                {
                    if (NameCommand == null)
                    {
                        throw new ArgumentException("Было указано не актуальное имя для переменной!");
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                }
                if (pathsText[0] == FileWriter.stringNull)
                {
                    return false;
                }
                else if (pathsText[0] == NameCommand)
                {
                    return true;
                }

                return false;
            }
            return false;
        }
        public bool SearchCommands(string nameCommand, string[] args)
        {
            if (SearchCommands(nameCommand) && SearchOptionInArgs(nameCommand, args) &&
            openJsonFile != null && openJsonFile.Commands != null && pathsText.Length > 1)
            {
                int count = args.Length;
                for (int i = 1; i < pathsText.Length; ++i)
                {
                    Range twoTir = 0..2;
                    if (pathsText[i][twoTir] == "--")
                    {
                        for (int j = 0; j < args.Length; ++j)
                        {
                            for (int k = 0; k < openJsonFile.Commands.Length; ++k)
                            {
                                if (openJsonFile.Commands[k].Options != null && 
                                openJsonFile.Commands[k].Name == nameCommand)
                                {
                                    for (int l = 0; l < openJsonFile.Commands[k].Options.Length; ++l)
                                    {
                                        if (args[j] == openJsonFile.Commands[k].Options[l].Name)
                                        {
                                            if (pathsText[i] == openJsonFile.Commands[k].Options[l].Long)
                                            {
                                                --count;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (pathsText[i][0] == '-')
                    {
                        for (int j = 0; j < args.Length; ++j)
                        {
                            for (int k = 0; k < openJsonFile.Commands.Length; ++k)
                            {
                                if (openJsonFile.Commands[k].Options != null && 
                                openJsonFile.Commands[k].Name == nameCommand)
                                {
                                    for (int l = 0; l < openJsonFile.Commands[k].Options.Length; ++l)
                                    {
                                        if (args[j] == openJsonFile.Commands[k].Options[l].Name)
                                        {
                                            if (pathsText[i] == openJsonFile.Commands[k].Options[l].Short)
                                            {
                                                --count;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else return false;
                }
                if (count == 0) return true;
                else return false;
            }
            return false;
        }
    }
}
