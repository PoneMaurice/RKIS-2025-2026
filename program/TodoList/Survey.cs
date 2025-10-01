//This file contains every command and option for program and their logic - PoneMaurice 
namespace Task
{
    public class Survey
    {
        public int counter = 0;
        public string newText = "";
        public string[] listComm = {
            "none",
            "add",
            "help",
            "print",
            "task",
            "clear",
            "search",
            "config",
            "exit"
        };
        public void GlobalCommamd()
        {
            if (SearchExtension(0, "exit")) Exit();
            else if (SearchExtension(0, "help")) Help();
            else if (SearchExtension(0, "add")) Add();
            else if (SearchExtension(0, "task")) Task();
            else if (SearchExtension(0, "print")) Print();
            else None();
        }
        public void Help()
        {
            string text = "help";
            Console.WriteLine(text);
        }
        public void Add()
        {
            string text = "add";
            if (SearchExtension(1, "help")) Console.WriteLine($"{text} help");
            else if (SearchExtension("task") && SearchExtension("print"))
                Commands.AddTaskAndPrint();
            else if (SearchExtension(1, "task")) Commands.AddTask();
            else if (SearchExtension(1, "config")) Commands.AddConfUserData(newText);
            else Commands.AddUserData(newText);
        }
        public void Task()
        {
            string text = "task";
            if (SearchExtension(1, "help")) Console.WriteLine($"{text} help");
            else if (SearchExtension(1, "clear")) Console.WriteLine($"{text} clear");
            else if (SearchExtension(1, "search")) Console.WriteLine($"{text} search");
            else if (SearchExtension(1, "print")) Console.WriteLine($"{text} print");
            else Console.WriteLine(text);
        }
        public void Print()
        {
            string text = "print";
            if (SearchExtension(1, "help")) Console.WriteLine($"{text} help");
            else Console.WriteLine(text);
        }
        public void Exit()
        {
            Environment.Exit(0);
        }
        public void None()
        {
            Console.WriteLine("none");
        }


        public Dictionary<string, bool> extensions = new Dictionary<string, bool> { };
        public void AddExtensions()
        {
            for (int i = 0; i < listComm.Length; ++i)
            {
                extensions.Add(listComm[i], false);
            }
        }
        public void ClearExtensions() {
            for (int i = 0; i < extensions.Count; ++i)
            {
                extensions[listComm[i]] = false;
            }
        }
        public Dictionary<string, int> extensionsNUM = new Dictionary<string, int> { };
        public void AddExtensionsNUM()
        {
            for (int i = 0; i < listComm.Length; ++i)
            {
                extensionsNUM.Add(listComm[i], 0);
            }
        }
        public void ClearExtensionsNUM() {
            for (int i = 0; i < extensionsNUM.Count; ++i)
            {
                extensionsNUM[listComm[i]] = 0;
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
            string ask = Console.ReadLine() ?? "NULL";
            ask = ask.Trim();
            if (ask == "") ask = "NULL";
            string[] partsText = ask.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            SearchCommand(partsText);
            newText = AssociationString(partsText);
            GlobalCommamd();
            Console.WriteLine(newText);
        }

        public string AssociationString(string[] sepText)
        {
            string text = "";
            bool noneText = true;
            for (int i = counter; i < sepText.Length; ++i)
            {
                if (noneText)
                {
                    text = text + sepText[i];
                    noneText = false;
                }
                else text = text + " " + sepText[i];
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
                int lus = 1;
                for (int j = 1; j < listComm.Length; ++j)
                {
                    if (command[i] == listComm[j] &&
                    extensions[listComm[j]] != true)
                    {
                        extensions[listComm[j]] = true;
                        extensionsNUM[listComm[j]] = i;
                        num++;
                        break;
                    }
                    else ++lus;
                }
                if (lus == listComm.Length)
                {
                    if (i == 0) extensions["none"] = true;
                    break;
                }
            }
            counter = num;
        }
    }

}