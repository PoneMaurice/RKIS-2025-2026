// This file contains row and date formatting - PoneMaurice
namespace Task
{
    public class FormatRows
    {
        public static string FormatRow(string[] data)
        {
            /*Форматирует массив данных под будущию таблицу csv*/
            string text = "";
            FileWriter file = new();
            foreach (string pathRow in data)
            {
                if (text == "") text = text + pathRow;
                else text = text + file.seporRows + pathRow;
            }
            return text;
        }
        public static string GetNowDateTime()
        {
            /*возвращает сегодняшнюю дату и время в нужном формате*/
            DateTime nowDate = DateTime.Now;
            string date = nowDate.ToShortDateString();
            string time = nowDate.ToShortTimeString();
            string dateString = ($"{date} {time}");
            return dateString;
        }
    }
}