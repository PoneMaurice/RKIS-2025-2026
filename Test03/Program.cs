using System;
using System.Data;
using System.Formats.Asn1;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;
using Microsoft.VisualBasic;
using Task;

namespace Task
{
    public class Captions
    {
        public string TextCaptions = "";
        public void WriteCaption()
        {
            if (TextCaptions == "") CompText();
            Console.Write("Вывести титры?(y/N): ");
            string Char = Console.ReadLine() ?? "NULL";
            Char = Char.Trim();
            if (Char == "") Char = "NULL";
            Char = Char.ToLower();
            if (Char == "y") Console.WriteLine(TextCaptions);
        }
        void CompText()
        {
            string[] Ed =
            {
            "README",
            "исходный код",
            "некоторые аспекты git"
            };
            string[] Misha =
            {
            "git",
            ".gitignore",
            "некоторый части исходного кода"
            };

            Dictionary<int, string> fices = new Dictionary<int, string>()
            {
                {0, "Шевченок Э."},
                {1, "Титов М."}
            };
            Dictionary<int, string[]> captions = new Dictionary<int, string[]>()
            {
                {0 , Ed},
                {1 , Misha}
            };
            string text = "За работу отвецтвенны:\n";
            for (int i = 0; i < fices.Count; ++i)
            {
                text = text + $"{fices[i]} :";
                for (int j = 0; j < captions[i].Length; ++j)
                {
                    string[] caption = captions[i];
                    text = text + $" {caption[j]}";
                }
                text = text + "\n";
            }
            TextCaptions = text;
        }
    }
    public class Commands
    {
        
    }

    public class Survey
    {
        public int counter = 0;
        public string NewText = "";
        public string[] listcomm = {
            "none",
            "add",
            "help",
            "print",
            "task",
            "clear",
            "search",
            "exit"
        };
        public void GlobalCommamd()
        {
            if (SearchExtension(0, "none")) none();
            else if (SearchExtension(0, "help")) help();
            else if (SearchExtension(0, "add")) add();
            else if (SearchExtension(0, "task")) task();
            else none();
        }
        public void help()
        {
            string text = "help";
            Console.WriteLine(text);
        }
        public void add()
        {
            string text = "add";
            if (SearchExtension(1, "help")) Console.WriteLine($"{text} help");
            else if (SearchExtension("task") && SearchExtension("print"))
                Console.WriteLine($"{text} task and print");
            else if (SearchExtension(1, "task")) Console.WriteLine($"{text} task");
            else Console.WriteLine(text);
        }
        public void task()
        {
            string text = "task";
            if (SearchExtension(1, "help")) Console.WriteLine($"{text} help");
            else if (SearchExtension(1, "clear")) Console.WriteLine($"{text} clear");
            else if (SearchExtension(1, "search")) Console.WriteLine($"{text} search");
            else if (SearchExtension(1, "print")) Console.WriteLine($"{text} print");
            else Console.WriteLine(text);
        }
        public void none()
        {
            Console.WriteLine("none");
        }


        public Dictionary<string, bool> extensions = new Dictionary<string, bool> { };
        public void AddExtensions()
        {
            for (int i = 0; i < listcomm.Length; ++i)
            {
                extensions.Add(listcomm[i], false);
            }
        }
        public void ClearExtensions() {
            for (int i = 0; i < extensions.Count; ++i)
            {
                extensions[listcomm[i]] = false;
            }
        }
        public Dictionary<string, int> extensionsNUM = new Dictionary<string, int> { };
        public void AddExtensionsNUM()
        {
            for (int i = 0; i < listcomm.Length; ++i)
            {
                extensionsNUM.Add(listcomm[i], 0);
            }
        }
        public void ClearExtensionsNUM() {
            for (int i = 0; i < extensionsNUM.Count; ++i)
            {
                extensionsNUM[listcomm[i]] = 0;
            }
        }
        public bool SearchExtension(int position, string extension)
        {
            if (extensionsNUM[extension] == position &&
            extensions[extension] == true) return true;
            return false;
        }
        public bool SearchExtension(string extension)
        {
            if (extensions[extension] == true) return true;
            return false;
        }
        
        public void ProceStr(string text)
        {
            Console.Write(text);
            string ans = Console.ReadLine() ?? "NULL";
            ans = ans.Trim();
            if (ans == "") ans = "NULL";
            string[] PartsText = ans.Split(" ");
            SearchCommand(PartsText);
            NewText = AssociationString(PartsText);
            GlobalCommamd();
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
            if (text == "") text = "NULL";
            return text;
        }

        void SearchCommand(string[] command)
        {
            AddExtensions();
            AddExtensionsNUM();
            int num = 0;

            for (int i = 0; i < command.Length; ++i)
            {
                string testcomm = command[i].Trim();
                Console.WriteLine($"'{command[i]}' - '{testcomm}'");
                if (testcomm == "") continue;
                int lus = 1;
                for (int j = 1; j < listcomm.Length; ++j)
                {
                    if (testcomm == listcomm[j] &&
                    extensions[listcomm[j]] != true)
                    {
                        extensions[listcomm[j]] = true;
                        extensionsNUM[listcomm[j]] = i;
                        num++;
                        break;
                    }
                    else ++lus;
                }
                if (lus == listcomm.Length)
                {
                    if (i == 0) extensions["none"] = true;
                    break;
                }
            }
            counter = num;
        }
    }
    public static class TaskExtensions
    {
        public static void Main()
        {
            int cycle = 0;
            do
            {
                if (cycle == 0)
                {
                    var cap = new Captions();
                    cap.WriteCaption();
                }
                var STR = new Survey();
                STR.ProceStr("-- ");
                ++cycle;
            }
            while (true);
        }
    }
}
