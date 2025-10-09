using System.Reflection;
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
        // string[] pathsText;
        // public SearchCommandOnJson(string[] text)
        // {
        //     pathsText = text;
        // }

        // public bool SearchCommands(string nameCommand)
        // {
        //     string? NameCommand = null;
        //     foreach (string commands in openJsonFile.Commands)
        //     {
        //         if (nameCommand == commands)
        //         {
        //             NameCommand = commands;
        //         }
        //     }
        //     try
        //     {
        //         if (NameCommand == null)
        //         {
        //             throw new ArgumentException("Было указано не актуальное имя для переменной!");
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         System.Console.WriteLine(ex.ToString());
        //     }
        //     if (pathsText[0] == FileWriter.stringNull)
        //     {
        //         return false;
        //     }
        //     else if (pathsText[0] == NameCommand)
        //     {
        //         return true;
        //     }

        //     return false;
        // }
        // public bool SearchCommands(string nameCommand, string[] args)
        // {
        //     if (SearchCommands(nameCommand))
        //     {
        //         int count = args.Length;
        //         for (int i = 1; i < pathsText.Length; ++i)
        //         {
        //             Range twoTir = 0..2;
        //             if (pathsText[i][twoTir] == "--")
        //                 for (int j = 0; j < args.Length; ++j)
        //                 {
        //                     for (int k = 0; k < openJsonFile.Options.Length; ++k)
        //                     {
        //                         if (args[j] == openJsonFile.Options[k].Name)
        //                         {
        //                             if (pathsText[i] == openJsonFile.Options[k].Long)
        //                             {
        //                                 --count;
        //                             }
        //                         }
        //                     }
        //                 }
        //             else if (pathsText[i][0] == '-')
        //             {
        //                 for (int j = 0; j < args.Length; ++j)
        //                 {
        //                     for (int k = 0; k < openJsonFile.Options.Length; ++k)
        //                     {
        //                         if (args[j] == openJsonFile.Options[k].Name)
        //                         {
        //                             if (pathsText[i] == openJsonFile.Options[k].Short)
        //                             {
        //                                 --count;
        //                             }
        //                         }
        //                     }
        //                 }
        //             }
        //             else return false;
        //         }
        //         if (count == 0) return true;
        //         else return false;
        //     }
        //     return false;
        // }
    }
}
