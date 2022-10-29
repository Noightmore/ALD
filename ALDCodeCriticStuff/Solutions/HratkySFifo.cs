using System.Globalization;
using ALDCodeCriticStuff.Solutions.LIFOFIFO;

namespace ALDCodeCriticStuff.Solutions;

public class HratkySFifo
{
    public static void Evaluate()
    {
        var myFifo = new Fifo<string[]>();
        var output = "";
        
        while(true)
        {
            var line = Console.ReadLine()?.Split(" ");
            
            if (line is null) break;
            if (line.FirstOrDefault() is "") break;
            
            myFifo.Enqueue(line);
        }

        while(true)
        {
            var line = myFifo.Dequeue();
            if (line is null) break;

            output = 
                line.Aggregate(output, (current, word) => 
                    current + (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word.ToLower()) + " "));
            output += Environment.NewLine;
            
        }
        Console.Write(output);
    }
}