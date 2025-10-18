// This file contains row and date formatting - PoneMaurice

using System.Collections;
using System.Data;
using System.Globalization;
using System.Text;

namespace Task
{
    public class FormatterRows
    {
        public StringBuilder Row = new();
        int Num;
        TypeEnum type;

        public enum TypeEnum
        {
            title,
            row,
            dataType,
            old
        }
        public string[] GetFirstObject()
        {
            string[] res = new string[0];
            switch (type)
            {
                case TypeEnum.row:
                    res = [Num.ToString(), ConstProgram.RowBoolDefault];
                    break;
                case TypeEnum.title:
                    res = [ConstProgram.TitleNumbingObject, ConstProgram.TitleBoolObject];
                    break;
                case TypeEnum.dataType:
                    res = [ConstProgram.DataTypeNumbingObject, ConstProgram.DataTypeBoolObject];
                    break;
                case TypeEnum.old:
                    break;
            }
            return res;
        }
        public FormatterRows(string nameFile, TypeEnum typeOut = TypeEnum.row)
        {
            OpenFile file = new(nameFile);
            Num = file.GetLengthFile();
            type = typeOut;
            Row.Append(string.Join("|", GetFirstObject()));
        }

        public void AddInRow(string pathRow)
        {
            /*Форматирует массив данных под будущую таблицу csv*/
            if (Row.ToString().Length == 0) Row.Append(pathRow);
            else Row.Append(ConstProgram.SeparRows + pathRow);
        }
        public void AddInRow(string[] row)
        {
            AddInRow(string.Join(ConstProgram.SeparRows, row));
        }
        public int GetLengthRow()
        {
            if (Row.Length != 0)
                return Row.ToString().Split(ConstProgram.SeparRows).Count();
            return 0;
        }
    }
    public static class ConstProgram
    {
        public const string SeparRows = "|";
        public const string TitleNumbingObject = "numbering";
        public const string DataTypeNumbingObject = "counter";
        public const string TitleBoolObject = "Bool";
        public const string DataTypeBoolObject = "bool";
        public const string RowBoolDefault = "False";
        public const string PrefConfigFile = "_conf";
        public const string PrefTemporaryFile = "_temp";
        public const string PrefIndex = "_index";
        public readonly static string[] StringArrayNull = new string[0];
        public const string TaskName = "Tasks";
        public const string ProfileName = "Profiles";
        public static readonly string[] TaskTitle = { "nameTask", "description", "nowDateAndTime", "deadLine" };
        public static readonly string[] TaskTypeData = { "s", "s", "ndt", "dt" };
        public static readonly string[] ProfileTitle = { "name", "DOB", "nowDateAndTime" };
        public static readonly string[] ProfileDataType = { "s", "d", "ndt" };
        public static readonly string[] AdminProfile = { "guest", "None", Input.NowDateTime() };
        public const string LogName = "log";
        public static readonly string[] LogTitle = { "ActiveProfile", "DateAndTime", "Command", "Options", "TextCommand" };
        public static readonly string[] LogDataType = { "s", "ndt", "s", "s", "s" };
    }
}