using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;
using Microsoft.VisualBasic;

namespace Task
{
    public static class Commands
    {
        public static void help(Dictionary<string, bool> extensions)
        {
            StartCommand(extensions, "help");
        }
        public static void add(Dictionary<string, bool> extensions)
        {
            StartCommand(extensions, "add");
        }
        public static void print(Dictionary<string, bool> extensions)
        {
            StartCommand(extensions, "print");
        }
        public static void none()
        {
            Console.WriteLine("none");
        }
        static void StartCommand(Dictionary<string, bool> extensions, string text)
        {
            if (extensions["help"]) Console.WriteLine($"{text} help");
            else if (extensions["task"]) Console.WriteLine($"{text} task");
            else if (extensions["clear"]) Console.WriteLine($"{text} clear");
            else if (extensions["search"]) Console.WriteLine($"{text} search");
            else Console.WriteLine(text);
        }

    }

    public class Survey
    {
        Dictionary<string, bool> extensions = new Dictionary<string, bool>
        {
            {"add", false},
            {"help", false},
            {"print", false},
            {"task", false},
            {"clear", false},
            {"search", false}
        };

        enum Command
        {
            add,
            help,
            print,
            task,
            none,
            clear,
            search
        }

        void SubObjCommand(Command[] command)
        {
            switch (command[1])
            {
                case Command.help:
                    extensions["help"] = true;
                    break;
                case Command.task:
                    extensions["task"] = true;
                    break;
                case Command.search:
                    extensions["search"] = true;
                    break;
                case Command.clear:
                    extensions["clear"] = true;
                    break;
            }
        }

        void ObjCommand(Command[] command)
        {
            switch (command[0])
            {
                case Command.add:
                    SubObjCommand(command);
                    Commands.add(extensions);
                    break;
                case Command.help:
                    SubObjCommand(command);
                    Commands.help(extensions);
                    break;
                case Command.print:
                    SubObjCommand(command);
                    Commands.print(extensions);
                    break;
                case Command.none:
                    Commands.none();
                    break;
            }
        }
        public void str(string text)
        {
            Console.Write(text);
            string ans = Console.ReadLine() ?? "NULL";
            string[] PartsText = ProceStr(ans);
            ObjCommand(SearchCommand(PartsText));
        }
        public string[] ProceStr(string text)
        {
            if (text == null) return ["NULL"];
            text = text.Trim();
            if (text == "") return ["NULL"];
            string[] PartsText = text.Split(" ");
            return PartsText;
        }

        Command SubCommand(string text, int light)
        {
            if (light >= 2 && text == "help") return Command.help;
            else if (light >= 2 && text == "task") return Command.task;
            else if (light >= 2 && text == "clear") return Command.clear;
            else if (light >= 2 && text == "search") return Command.search;
            else return Command.none;
        }
        Command[] SearchCommand(string[] command)
        {
            Command[] instructions = new Command[3]
            {
                Command.none,
                Command.none,
                Command.none
            };

            int CommLight = command.Length;

            if (command[0] == "help")
            {
                instructions[0] = Command.help;
                if (CommLight >= 2) instructions[1] = SubCommand(command[1], CommLight);
                return instructions;
            }
            else if (command[0] == "add")
            {
                instructions[0] = Command.add;
                if (CommLight >= 2) instructions[1] = SubCommand(command[1], CommLight);
                return instructions;
            }
            else if (command[0] == "print")
            {
                instructions[0] = Command.print;
                if (CommLight >= 2) instructions[1] = SubCommand(command[1], CommLight);
                return instructions;
            }
            else if (command[0] == "exit") Environment.Exit(0);
            return instructions;
        }
    }
    public static class TaskExtensions
    {
        public static void Main()
        {
            do
            {
                var STR = new Survey();
                STR.str("-- ");
            }
            while (true);
        }
    }
}