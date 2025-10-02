// This file contains row and date formatting - PoneMaurice

using System.Data;
using System.Globalization;
using System.Text;

namespace Task
{
    public class FormatRows(string nameFile, bool TitleRow = false, bool dataTypeRow = false)
    {
        public StringBuilder Row = new();
        public string? PathToFile;
        public int Num;
        public void AddInRow(string pathRow)
        {
            /*Форматирует массив данных под будущию таблицу csv*/
            FileWriter file = new();
            if (PathToFile == null)
            {
                PathToFile = file.CreatePath(nameFile);
            }
            if (Row.ToString() == "" && dataTypeRow)
            {
                Num = file.GetLeghtFile(PathToFile);
                Row.Append("counter" + file.seporRows + pathRow);
            }
            else if (Row.ToString() == "" && !TitleRow)
            {
                Num = file.GetLeghtFile(PathToFile);
                Row.Append(Num + file.seporRows + pathRow);
            }
            else if (Row.ToString() == "" && TitleRow)
            {
                Num = 0;
                Row.Append("numbering" + file.seporRows + pathRow);
            }
            else Row.Append(file.seporRows + pathRow);
        }
        public int GetLeghtRow()
        {
            if (Row.Length != 0)
            {
                FileWriter file = new();
                return Row.ToString().Split(file.seporRows).Count();
            }
            else return 0;
        }
        public static string GetNowDateTime()
        {
            /*возвращает сегодняшнюю дату и время в нужном формате*/
            DateTime nowDate = DateTime.Now;
            StringBuilder dateTime = new();
            dateTime.Append(nowDate.ToShortDateString() +
                " " + nowDate.ToShortTimeString());
            return dateTime.ToString();
        }
    }
}