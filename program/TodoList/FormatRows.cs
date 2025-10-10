// This file contains row and date formatting - PoneMaurice

using System.Collections;
using System.Data;
using System.Globalization;
using System.Text;

namespace Task
{
    public class FormatRows
    {
        public const string SeparRows = "|";
        public StringBuilder Row = new();
        int Num;
        Type type;
        public static string titleFirstObject = "numbering";
        public static string dataTypeFirstObject = "counter";

        public enum Type : int
        {
            title = 1,
            row = 2,
            dataType = 3
        }
        public FormatRows(string nameFile, Type typeOut = Type.row)
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
                    return titleFirstObject;
                case Type.dataType:
                    return dataTypeFirstObject;
            }
            return Num.ToString();
        }
        public void AddInRow(string pathRow)
        {
            /*Форматирует массив данных под будущию таблицу csv*/
            if (Row.ToString() == "") Row.Append(GetFirstObject() + SeparRows + pathRow);
            else Row.Append(SeparRows + pathRow);
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
                return Row.ToString().Split(SeparRows).Count();
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
}