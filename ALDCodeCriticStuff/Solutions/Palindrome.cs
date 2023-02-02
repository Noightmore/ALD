namespace ALDCodeCriticStuff.Solutions;

public static class Palindrom
{
    public static void Evaluate()
    {
        while (true)
        {
            var slovo = Console.ReadLine()?.ToLower();
            if (slovo is null or "") break;

            Console.WriteLine(CheckSymmetric(slovo) ? "ano" : "ne");
        }
    }

    private static bool CheckSymmetric(string word)
    {
        for (var letterIndex = 0; letterIndex < word.Length; letterIndex++)
        {
            if (word[letterIndex] == word[word.Length - 1 - letterIndex]) continue;
            return false;
        }

        return true;
    }
}