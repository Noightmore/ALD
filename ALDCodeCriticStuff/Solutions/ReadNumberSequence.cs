namespace ALDCodeCriticStuff.Solutions;

public class ReadNumberSequence
{
    public static void Evaluate()
    {
        while (true)
        {
            var num = int.Parse(Console.ReadLine() ?? string.Empty);
            if (num == 42) break;
            Console.WriteLine(num);
        }
    }
}