//This file contains overly engineered logic for the welcome message - PoneMaurice
namespace Task
{
    public class Captions
    {
        public string TextCaptions = "";
        public void WriteCaption()
        {
            /*спрашивает и выводит текст субтитров созданный 
            методом CompText*/
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
            /*Составляет текст для субтитров*/
            /*WHAT THE HAY IS THAT?! I think i actually like that;) - PoneMaurice */
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

            Dictionary<int, string> feces = new Dictionary<int, string>()
            {
                {0, "Шевченок Э."}, // Шевченок Э. на месте? - PoneMaurice
                {1, "Титов М."}
            };
            Dictionary<int, string[]> captions = new Dictionary<int, string[]>()
            {
                {0 , Ed},
                {1 , Misha}
            };
            string text = "За работу ответственны:\n";
            for (int i = 0; i < feces.Count; ++i)
            {
                text = text + $"{feces[i]} :";
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
}