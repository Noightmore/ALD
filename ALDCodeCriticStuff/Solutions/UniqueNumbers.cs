namespace ALDCodeCriticStuff.Solutions;

public static class UniqueNumbers
{
    public static void Evaluate()
    {
        var uniqueValues = new Dictionary<int, Occurence>();
        while (true)
        {
            var line = Console.ReadLine()?.Split(",");
            if (line is null) break;
            
            if (string.IsNullOrEmpty(line.FirstOrDefault())) break;

            foreach (var number in line.Select(int.Parse).ToArray())
                if (uniqueValues.ContainsKey(number))
                    uniqueValues[number] = Occurence.MoreThanOnce;
                else
                    uniqueValues.Add(number, Occurence.Once);
        }

        PrintOutput(uniqueValues);
    }

    private static void PrintOutput<T>(Dictionary<T, Occurence> uniqueParamsOccurenceContainer) where T : notnull
    {
        const string all = "All: ";
        const string moreThanOnce = ">1x: ";
        const string exactlyOnce = "=1x: ";

        var allLine = all;
        var moreThanOnceLine = moreThanOnce;
        var exactlyOnceLine = exactlyOnce;


        foreach (var param in uniqueParamsOccurenceContainer)
        {
            var item = param.Key + ",";

            allLine += item;
            switch (param.Value)
            {
                case Occurence.MoreThanOnce:
                    moreThanOnceLine += item;
                    break;
                case Occurence.Once:
                    exactlyOnceLine += item;
                    break;
                case Occurence.NotAtAll:
                default:
                    Console.WriteLine("FATAL INTERNAL ERROR");
                    return;
            }
        }

        Console.WriteLine(allLine.Remove(allLine.Length - 1, 1));
        Console.WriteLine(moreThanOnceLine.Remove(moreThanOnceLine.Length - 1, 1));
        Console.WriteLine(exactlyOnceLine.Remove(exactlyOnceLine.Length - 1, 1));
    }
}

internal enum Occurence
{
    NotAtAll,
    Once,
    MoreThanOnce
}