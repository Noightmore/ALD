using System.Globalization;
using ALDCodeCriticStuff.Solutions.LIFOFIFO;

namespace ALDCodeCriticStuff.Solutions;

public static class HratkySLifo
{
    public static void Evaluate()
    {
        var myLifo = new Lifo<string[]>();
        var output = "";

        while (true)
        {
            var line = Console.ReadLine()?.Split(" ");

            if (line is null) break;
            if (line.FirstOrDefault() is "" or null) break;

            myLifo.Push(line);
        }

        while (true)
        {
            var line = myLifo.Pop();
            if (line is null) break;

            output =
                line.Aggregate(output, (current, word) =>
                    current + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word.ToLower()) + " ");
            output += Environment.NewLine;
        }

        Console.Write(output);
    }
}