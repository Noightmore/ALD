namespace ALDCodeCriticStuff.Solutions.UniqueNumbersTools;

public class Value
{
    public int Id { get; private set; }
    
    public Occurence Occurence { get; private set; }

    public Value(int id)
    {
        Id = id;
        Occurence = Occurence.Once;
    }
}