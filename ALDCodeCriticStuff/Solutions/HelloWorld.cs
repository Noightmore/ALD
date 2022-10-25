namespace ALDCodeCriticStuff.Solutions;

public static class HelloWorld
{
    public static void Eval()
    {
        var count = int.Parse(Console.ReadLine() ?? string.Empty);
        for (var i = 0; i < count; i++)
        {
            Console.WriteLine("Hello world");
        }
    }
}