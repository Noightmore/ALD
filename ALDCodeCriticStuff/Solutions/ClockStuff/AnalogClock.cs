namespace ALDCodeCriticStuff.Solutions.ClockStuff;

public class AnalogClock
{
    // unsigned byte 0-255
    protected byte Hour { get; set; }
    protected byte Minute { get; set; }
    protected byte Second { get; set; }

    protected AnalogClock(byte hour, byte minute, byte second)
    {
        // Hour = hour;
        // Minute = minute;
        // Second = second;
    }

    public static AnalogClock? Generate(byte hour, byte minute, byte second, byte clockType)
    {
        return clockType switch
        {
            1 => new Type1(hour, minute, second),
            2 => new Type2(hour, minute, second),
            3 => new Type3(hour, minute, second),
            _ => null
        };
    }
    
    // method that computes segments based on the time
    internal virtual void ComputeSegments()
    {
        return;
    }
}