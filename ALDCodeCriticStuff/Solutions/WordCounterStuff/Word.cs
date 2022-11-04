namespace ALDCodeCriticStuff.Solutions.WordCounterStuff;

public class Word
{
    public string Value { get; }
    public uint Count { get; private set; }
    
    public Word(string value)
    {
        Value = value;
        Count = 1;
    }
    
    public void Increment()
    {
        Count++;
    }
    
    
}