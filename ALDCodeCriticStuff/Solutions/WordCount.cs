using System.Text;

namespace ALDCodeCriticStuff.Solutions;

public static class WordCount
{
    public static void Evaluate()
    {
        var words = new Dictionary<string, ulong>();
        var phrases = new Dictionary<string, ulong>();

        var text = new StringBuilder();

        var line = Console.ReadLine();
        while (line is not "---")
        {
            if (line is null) break;

            var wordsInLine =
                line.Split(new[] {' ', '\t', '\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in wordsInLine)
            {
                var loweredWord = word.ToLower();
                if (words.ContainsKey(loweredWord))
                    words[loweredWord]++;
                else
                    words.Add(loweredWord, 1);
            }

            text.Append(line.TrimEnd().TrimStart());
            if (line is not "") text.Append(' ');

            line = Console.ReadLine();
        }

        var textArray = text.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        for (var i = 0; i < textArray.Length - 1; i++)
        {
            var phrase = textArray[i] + " " + textArray[i + 1];
            var loweredPhrase = phrase.ToLower();
            if (phrases.ContainsKey(loweredPhrase))
                phrases[loweredPhrase]++;
            else
                phrases.Add(loweredPhrase, 1);
        }

        var sortedWords =
            words.OrderByDescending(x => x.Value).Take(15);
        var sortedPhrases =
            phrases.OrderByDescending(x => x.Value).Take(15);

        Console.WriteLine("Word Frequency:");
        foreach (var word in sortedWords)
        {
            var percentage = Math.Round(word.Value / (double) words.Count * 100, 2);
            Console.WriteLine($" - {word.Key,-12} {percentage}% ({word.Value})");
        }

        Console.WriteLine("Phrase Frequency:");
        foreach (var phrase in sortedPhrases)
        {
            var percentage = Math.Round(phrase.Value / (double) phrases.Count * 100, 2);
            Console.WriteLine($" - {phrase.Key,-20} {percentage}% ({phrase.Value})");
        }
    }
}