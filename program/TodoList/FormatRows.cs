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
        public string GetFirstObject()
        {
            switch (type)
            {
                case TypeEnum.row:
                    return Num.ToString() + ConstProgram.SeparRows + ConstProgram.RowBoolDefault + ConstProgram.SeparRows;
                case TypeEnum.title:
                    return ConstProgram.TitleNumbingObject + ConstProgram.SeparRows + ConstProgram.TitleBoolObject + ConstProgram.SeparRows;
                case TypeEnum.dataType:
                    return ConstProgram.DataTypeNumbingObject + ConstProgram.SeparRows + ConstProgram.DataTypeBoolObject + ConstProgram.SeparRows;
                case TypeEnum.old:
                    return "";
            }
            return Num.ToString();
        }
        public FormatterRows(string nameFile, TypeEnum typeOut = TypeEnum.row)
        {
            OpenFile file = new(nameFile);
            Num = file.GetLengthFile();
            type = typeOut;
        }

        public void AddInRow(string pathRow)
        {
            /*Форматирует массив данных под будущую таблицу csv*/
            if (Row.ToString().Length == 0) Row.Append(GetFirstObject() + pathRow);
            else Row.Append(ConstProgram.SeparRows + pathRow);
        }
        public void AddInRow(string[] row)
        {
            foreach (string path in row)
            {
                AddInRow(path);
            }
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
        public readonly static string[] StringArrayNull = new string[0];
        public const string TaskName = "Tasks";
        public const string ProfileName = "Profiles";
        public static readonly string[] TaskTitle = { "nameTask", "description", "nowDateAndTime", "deadLine" };
        public static readonly string[] TaskTypeData = { "s", "s", "ndt", "dt" };
        public static readonly string[] ProfileTitle = { "name", "DOB", "nowDateAndTime" };
        public static readonly string[] ProfileDataType = { "s", "d", "ndt" };
        public static readonly string[] AdminProfile = { "guest", "None", Input.NowDateTime()};
    }
}