// ReSharper disable NonReadonlyMemberInGetHashCode
namespace ALDCodeCriticStuff.Solutions.ClockStuff;

public class Type1 : AnalogClock
{
    private PoweredState ASegment { get; set; }
    private PoweredState BSegment { get; set; }

    public Type1(byte hour, byte minute, byte second) : base(hour, minute, second)
    {
        this.ASegment = PoweredState.Off;
        this.BSegment = PoweredState.Off;
        this.ComputeSegments();
    }

    internal sealed override void ComputeSegments()
    {
        if (Hour is 9 || Minute is 45 || Second is 45)
        {
            ASegment = PoweredState.On;
        }
        if (Hour is 6 || Minute is 30 || Second is 30)
        {
            BSegment = PoweredState.On;
        }
    }
    
    public override int GetHashCode()
    {
        unchecked
        {
            var result = ASegment.GetHashCode();
            result = (result*397) ^ (BSegment.GetHashCode());
            return result;
        }
    }
}

