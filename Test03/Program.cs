using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;
using Microsoft.VisualBasic;
using Task;

namespace Task
{
    public class Commands
    {
        public Survey c = new Survey();
        public void help(Dictionary<string, bool> extensions)
        {
            string text = "help";
            if (extensions[c.Help]) Console.WriteLine($"{text} {c.Help}");
            else if (extensions[c.Task]) Console.WriteLine($"{text} {c.Task}");
            else if (extensions[c.Clear]) Console.WriteLine($"{text} {c.Clear}");
            else if (extensions[c.Search]) Console.WriteLine($"{text} {c.Search}");
            else Console.WriteLine(text);
        }
        public void add(Dictionary<string, bool> extensions)
        {
            string text = "add";
            if (extensions[c.Help]) Console.WriteLine($"{text} {c.Help}");
            else if (extensions[c.Task]) Console.WriteLine($"{text} {c.Task}");
            else if (extensions[c.Clear]) Console.WriteLine($"{text} {c.Clear}");
            else if (extensions[c.Search]) Console.WriteLine($"{text} {c.Search}");
            else Console.WriteLine(text);
        }
        public void print(Dictionary<string, bool> extensions)
        {
            string text = "print";
            if (extensions[c.Help]) Console.WriteLine($"{text} {c.Help}");
            else if (extensions[c.Task]) Console.WriteLine($"{text} {c.Task}");
            else if (extensions[c.Clear]) Console.WriteLine($"{text} {c.Clear}");
            else if (extensions[c.Search]) Console.WriteLine($"{text} {c.Search}");
            else Console.WriteLine(text);
        }
        public void none()
        {
            Console.WriteLine("none");
        }
    }

    public class Survey
    {
        public int counter = 0;
        public string NewText = "";
        public string Add = "add";
        public string Help = "help";
        public string Print = "print";
        public string Task = "task";
        public string Clear = "clear";
        public string Search = "search";
        public string Exit = "exit";

        Dictionary<string, bool> extensions = new Dictionary<string, bool> { };
        

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

        void SubObjCommand(Dictionary<int, Command> command)
        {
            extensions.Add(Add, false);
            extensions.Add(Help, false);
            extensions.Add(Clear, false);
            extensions.Add(Task, false);
            extensions.Add(Print, false);
            extensions.Add(Search, false);
            if (command.Count >= 2)
            {
                switch (command[1])
                {
                    case Command.help:
                        extensions[Help] = true;
                        break;
                    case Command.task:
                        extensions[Task] = true;
                        break;
                    case Command.search:
                        extensions[Search] = true;
                        break;
                    case Command.clear:
                        extensions[Clear] = true;
                        break;
                }
            }
        }

        void ObjCommand(Dictionary<int, Command> command)
        {
        Commands c = new Commands();
            switch (command[0])
        {
            case Command.add:
                SubObjCommand(command);
                c.add(extensions);
                break;
            case Command.help:
                SubObjCommand(command);
                c.help(extensions);
                break;
            case Command.print:
                SubObjCommand(command);
                c.print(extensions);
                break;
            case Command.none:
                c.none();
                break;
        }
        }
        public void ProceStr(string text)
        {
            Console.Write(text);
            string ans = Console.ReadLine() ?? "NULL";
            ans = ans.Trim();
            if (ans == "") ans = "NULL";
            string[] PartsText = ans.Split(" ");
            ObjCommand(SearchCommand(PartsText));
            NewText = AssociationString(PartsText);
            Console.WriteLine(NewText);
        }

        public string AssociationString(string[] SepText)
        {
            string text = "";
            bool nonetext = true;
            for (int i = counter; i < SepText.Length; ++i)
            {
                if (nonetext)
                {
                    text = text + SepText[i];
                    nonetext = false;
                }
                else text = text + " " + SepText[i];
            }
            return text;
        }

        Command SubCommand(string text, int light)
        {
            if (light >= 2 && text == Help) return Command.help;
            else if (light >= 2 && text == Task) return Command.task;
            else if (light >= 2 && text == Clear) return Command.clear;
            else if (light >= 2 && text == Search) return Command.search;
            else return Command.none;
        }
        Dictionary<int, Command> SearchCommand(string[] command)
        {
            var instructions = new Dictionary<int, Command> {};

            int CommLight = command.Length;

            if (command[0] == Help)
            {
                instructions[0] = Command.help;
                if (CommLight >= 2) instructions[1] = SubCommand(command[1], CommLight);
            }
            else if (command[0] == Add)
            {
                instructions[0] = Command.add;
                if (CommLight >= 2) instructions[1] = SubCommand(command[1], CommLight);
            }
            else if (command[0] == Print)
            {
                instructions[0] = Command.print;
                if (CommLight >= 2) instructions[1] = SubCommand(command[1], CommLight);
            }
            else if (command[0] == Exit) Environment.Exit(0);

            int num = 0;
            for (int i = 0; i < instructions.Count; i++)
            {
                if (instructions[i] != Command.none) ++num;
            }
            counter = num;
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
                STR.ProceStr("-- ");
            }
            while (true);
        }
    }
}