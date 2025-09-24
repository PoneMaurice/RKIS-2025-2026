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
            Console.WriteLine("help");
        }
        public static void add(Dictionary<string, bool> extensions)
        {
            if (extensions["help"]) Console.WriteLine("add help");
            else if (extensions["task"]) Console.WriteLine("add task");
            else Console.WriteLine("add");
        }
        public static void print(Dictionary<string, bool> extensions)
        {
            if (extensions["help"]) Console.WriteLine("print help");
            else if (extensions["task"]) Console.WriteLine("print task");
            else Console.WriteLine("print");
        }
        public static void none()
        {
            Console.WriteLine("none");
        }
    }
    public class Survey
    {
        Dictionary<string, bool> extensions = new Dictionary<string, bool>
        {
            {"help", false},
            {"task", false},
        };
        enum Command
        {
            add,
            help,
            print,
            task,
            none
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
        Command [] SearchCommand(string [] command)
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
                return instructions;
            }
            else if (command[0] == "add")
            {
                instructions[0] = Command.add;
                if (CommLight >= 2 && command[1] == "help") instructions[1] = Command.help;
                else if (CommLight >= 2 && command[1] == "task") instructions[1] = Command.task;
                return instructions;
            }
            else if (command[0] == "print")
            {
                instructions[0] = Command.print;
                if (CommLight >= 2 && command[1] == "help") instructions[1] = Command.help;
                else if (CommLight >= 2 && command[1] == "task") instructions[1] = Command.task;
                return instructions;
            }
            return [Command.none];
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