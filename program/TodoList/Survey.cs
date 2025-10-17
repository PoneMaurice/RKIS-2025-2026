//This file contains every command and option for program and their logic - PoneMaurice 
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Nodes;

namespace Task
{
    public class Survey
    {
        public SearchCommandOnJson? commandLineGlobal;
        
        public void GlobalCommand(string Text)
        {
            string ask = Input.String(Text);
            SearchCommandOnJson commandLine = new(ask.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            commandLineGlobal = commandLine;
            
            switch (commandLine.commandOut)
            {
                case "add":
                    if (commandLine.optionsOut.Length > 0)
                    {
                        if (commandLine.SearchOption("help"))
                        {
                            Commands.AddHelp();
                        }
                        else if (commandLine.SearchOption("task"))
                        {
                            Commands.AddTask();
                        }
                        else if (commandLine.SearchOption("task", "print"))
                        {
                            Commands.AddTaskAndPrint();
                        }
                        else if (commandLine.SearchOption("config"))
                        {
                            Commands.AddConfUserData(commandLine.nextTextOut);
                        }
                        else if (commandLine.SearchOption("profile"))
                        {
                            Commands.AddProfile();
                        }
                        else { Commands.AddUserData(commandLine.nextTextOut); }
                    }
                    else {Commands.AddUserData(commandLine.nextTextOut);}
                    break;

                case "profile":
                    if (commandLine.optionsOut.Length > 0)
                    {
                        if (commandLine.SearchOption("help"))
                        {
                            Commands.ProfileHelp();
                        }
                        else if (commandLine.SearchOption("add"))
                        {
                            Commands.AddProfile();
                        }
                    }
                    else {WriteToConsole.RainbowText("Эта функция ещё в разработке", ConsoleColor.Magenta);} ///////////////////////////////////////////////////////////
                    break;

                case "print":
                    if (commandLine.optionsOut.Length > 0)
                    {
                        if (commandLine.SearchOption("help"))
                        {
                            Commands.PrintHelp();
                        }
                        else if (commandLine.SearchOption("task"))
                        {
                            Commands.PrintData(ConstProgram.TaskName);
                        }
                        else if (commandLine.SearchOption("config"))
                        {
                            Commands.PrintData(commandLine.nextTextOut + ConstProgram.PrefConfigFile);
                        }
                        else if (commandLine.SearchOption("profile"))
                        {
                            Commands.PrintData(ConstProgram.ProfileName);
                        }
                        else if (commandLine.SearchOption("captions")){
                            Commands.WriteCaption();
                        } 
                        else { Commands.PrintData(commandLine.nextTextOut); }
                    }
                    else { Commands.PrintData(commandLine.nextTextOut); }
                    break;

                case "search":
                    if (commandLine.optionsOut.Length > 0)
                    {
                        if (commandLine.SearchOption("help"))
                        {
                            Commands.SearchHelp();
                        }
                        else if (commandLine.SearchOption("task"))
                        {
                            Commands.SearchPartData(ConstProgram.TaskName, commandLine.nextTextOut);
                        }
                        else if (commandLine.SearchOption("profile"))
                        {
                            Commands.SearchPartData(ConstProgram.ProfileName, commandLine.nextTextOut);
                        }
                        else if (commandLine.SearchOption("numbering"))
                        {
                            WriteToConsole.RainbowText("Эта функция ещё в разработке", ConsoleColor.Magenta); ///////////////////////////////////////////////////////////
                        }
                        else { Commands.SearchPartData(commandLine.nextTextOut); }
                    }
                    else {Commands.SearchPartData(commandLine.nextTextOut);}
                    break;

                case "clear":
                    if (commandLine.optionsOut.Length > 0)
                    {
                        if (commandLine.SearchOption("help"))
                        {
                            WriteToConsole.RainbowText("Эта функция ещё в разработке", ConsoleColor.Magenta); ///////////////////////////////////////////////////////////
                        }
                        else if (commandLine.SearchOption("task"))
                        {
                            Commands.ClearRow(ConstProgram.TaskName, commandLine.nextTextOut);
                        }
                        else if (commandLine.SearchOption("task", "all"))
                        {
                            Commands.ClearAllFile(ConstProgram.TaskName);
                        }
                        else if (commandLine.SearchOption("profile"))
                        {
                            Commands.ClearRow(ConstProgram.ProfileName, commandLine.nextTextOut);
                        }
                        else if (commandLine.SearchOption("profile", "all"))
                        {
                            Commands.ClearAllFile(ConstProgram.ProfileName);
                        }
                        else if (commandLine.SearchOption("console"))
                        {
                            WriteToConsole.RainbowText("CCCCCCClear", ConsoleColor.Magenta);
                            Console.Clear();
                        }
                        else if (commandLine.SearchOption("all"))
                        {
                            Commands.ClearAllFile(commandLine.nextTextOut);
                        }
                        else { Commands.ClearRow(commandLine.nextTextOut);}
                    }
                    else { Commands.ClearRow(commandLine.nextTextOut); }
                    break;

                case "edit":
                    if (commandLine.optionsOut.Length > 0)
                    {
                        if (commandLine.SearchOption("help"))
                        {
                            WriteToConsole.RainbowText("Эта функция ещё в разработке", ConsoleColor.Magenta); ///////////////////////////////////////////////////////////
                        }
                        else if (commandLine.SearchOption("task"))
                        {
                            Commands.EditRow(ConstProgram.TaskName);
                        }
                        else if (commandLine.SearchOption("task", "index"))
                        {
                            Commands.FixingIndexing(ConstProgram.TaskName);
                        }
                        else if (commandLine.SearchOption("index"))
                        {
                            Commands.FixingIndexing(commandLine.nextTextOut);
                        }
                        else { Commands.EditRow(commandLine.nextTextOut); }
                    }
                    else { Commands.EditRow(commandLine.nextTextOut); }
                    break;
                
                case "help":
                    Commands.Help();
                    break;

                case "exit":
                    Environment.Exit(0);
                    break;
            }
            
        }
    }
}