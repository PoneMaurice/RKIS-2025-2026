// This file contains row and date formatting - PoneMaurice

using System.Collections;
using System.Data;
using System.Globalization;
using System.Text;

namespace Task
{
    public class FormaterRows
    {
        public StringBuilder Row = new();
        int Num;
        Type type;
        
        public enum Type : int
        {
            title = 1,
            row = 2,
            dataType = 3
        }
        public FormaterRows(string nameFile, Type typeOut = Type.row)
        {
            FileWriter file = new(nameFile);
            Num = file.GetLeghtFile();
            type = typeOut;
        }
        public string GetFirstObject()
        {
            switch (type)
            {
                case Type.row:
                    return Num.ToString();
                case Type.title:
                    return ConstProgram.TitleFirstObject;
                case Type.dataType:
                    return ConstProgram.DataTypeFirstObject;
            }
            return Num.ToString();
        }
        public void AddInRow(string pathRow)
        {
            /*Форматирует массив данных под будущию таблицу csv*/
            if (Row.ToString() == "") Row.Append(GetFirstObject() + ConstProgram.SeparRows + pathRow);
            else Row.Append(ConstProgram.SeparRows + pathRow);
        }
        public void AddArrayInRow(string[] row)
        {
            foreach (string path in row)
            {
                AddInRow(path);
            }
        }
        public int GetLeghtRow()
        {
            if (Row.Length != 0)
                return Row.ToString().Split(ConstProgram.SeparRows).Count();
            return 0;
        }
        public static string GetNowDateTime()
        {
            /*возвращает сегодняшнюю дату и время в нужном формате*/
            DateTime nowDate = DateTime.Now;
            return nowDate.ToShortDateString() +
                " " + nowDate.ToShortTimeString();
        }
    }
    public static class ConstProgram
    {
        public const string SeparRows = "|";
        public const string TitleFirstObject = "numbering";
        public const string DataTypeFirstObject = "counter";
        public const string PrefConfigFile = "_conf";
        public const string StringNull = "Null";
        public readonly static string[] StringArrayNull = [StringNull];
        public const string Yes = "y";
    }
}