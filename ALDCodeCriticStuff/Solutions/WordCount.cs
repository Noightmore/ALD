namespace ALDCodeCriticStuff.Solutions;

public static class WordCount
{
    public static void Evaluate()
    {
        var words = new Dictionary<string, ulong>();
        var phrases = new Dictionary<string, ulong>();

        var line = Console.ReadLine();
        while (line is not null)
        {
            var wordsInLine = line.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in wordsInLine)
            {
                if (words.ContainsKey(word))
                {
                    words[word]++;
                }
                else
                {
                    words.Add(word, 1);
                }
            }

            for (var i = 0; i < wordsInLine.Length - 1; i++)
            {
                var phrase = wordsInLine[i] + " " + wordsInLine[i + 1];
                if (phrases.ContainsKey(phrase))
                {
                    phrases[phrase]++;
                }
                else
                {
                    phrases.Add(phrase, 1);
                }
            }

            line = Console.ReadLine();
        }

        var sortedWords = words.OrderByDescending(x => x.Value);
        
        foreach (var word in sortedWords)
        {
            Console.WriteLine($"{word.Key} - {word.Value}");
        }
    }
}