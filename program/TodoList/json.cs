using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
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
        static string huiBolshoy = FileWriter.GetPathToZhopa();
        static string sex = Path.Join(huiBolshoy, nameFileJson);
        static string jsonString = File.ReadAllText(sex);
        static AllCommands? openJsonFile = JsonSerializer.Deserialize<AllCommands?>(jsonString);
        public string? commandOut = null;
        public string[]? optionsOut = null;
        public string? nextTextOut = null;
        public SearchCommandOnJson(string[] text)
        {
            StringBuilder optionsLine = new();
            StringBuilder textLine = new();
            if (openJsonFile != null &&
            openJsonFile.Commands != null)
            {
                foreach (var command in openJsonFile.Commands)
                {
                    if (command.Name == text[0])
                    {
                        commandOut = command.Name;
                        if (command.Options != null)
                        {
                            Range startTextToEnd = 1..text.Length;
                            bool optionInText = true;
                            foreach (var pathText in text[startTextToEnd])
                            {
                                bool notOption = true;
                                if (optionInText)
                                {
                                    foreach (var option in command.Options)
                                    {
                                        if (pathText == option.Short || pathText == option.Long)
                                        {
                                            if (!optionsLine.ToString().Contains(option.Name))
                                            {
                                                if (optionsLine.ToString() == "")
                                                {
                                                    optionsLine.Append(option.Name);
                                                }
                                                else
                                                {
                                                    optionsLine.Append("|" + option.Name);
                                                }
                                            }
                                            notOption = false;
                                        }
                                    }
                                }
                                if (notOption)
                                {
                                    if (textLine.ToString() == "")
                                    {
                                        optionInText = false;
                                        textLine.Append(pathText);
                                    }
                                    else {textLine.Append(" "+pathText);}
                                    
                                }
                            }
                        }
                    }
                }
                optionsOut = optionsLine.ToString().Split("|");
                nextTextOut = textLine.ToString();
                // System.Console.WriteLine("com: " + commandOut);
                // System.Console.WriteLine("opt:");
                // foreach (var option in optionsOut)
                // {
                //     System.Console.WriteLine("\t" + option);
                // }
                // System.Console.WriteLine("text: "+ nextTextOut);
            }
        }
    }
}
