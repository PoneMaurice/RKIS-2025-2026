using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Task
{
    public class Survey
    {
        public string str(string text)
        {
            Console.Write(text);
            string? ans = Console.ReadLine();
            if (ans is null)
            {
                return "NULL";
            }
            ans = ans.Trim();
            if (ans == "")
            {
                return "NONE";
            }
            return ans;
        }
    }
    public static class TaskExtensions
    {
        public static void Main()
        {
            Survey survey = new Survey();
            string NameTask = survey.str("Введите название задания: ");
            string DescTask = survey.str($"Введите описание {NameTask}: ");
            Console.WriteLine(NameTask);
            Console.WriteLine(DescTask);
        }
    }
}