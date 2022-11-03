// ReSharper disable NonReadonlyMemberInGetHashCode
namespace ALDCodeCriticStuff.Solutions.ClockStuff;

public class Type3 : AnalogClock
{
    private PoweredState FSegment { get; set; }
    private PoweredState GSegment { get; set; }
    private PoweredState ESegment { get; set; }

    public Type3(byte hour, byte minute, byte second) : base(hour, minute, second)
    {
        this.FSegment = PoweredState.Off;
        this.GSegment = PoweredState.Off;
        this.ESegment = PoweredState.Off;
        this.ComputeSegments();
    }
    
    internal sealed override void ComputeSegments()
    {
        if (Hour is 24 or 12 or 0 || Minute is 0 or 60 || Second is 0 or 60)
        {
            FSegment = PoweredState.On;
        }
        if (Hour is 3 || Minute is 15 || Second is 15)
        {
            GSegment = PoweredState.On;
        }
        if (Hour is 6 || Minute is 30 || Second is 30)
        {
            ESegment = PoweredState.On;
        }
    }
    
    // TODO: implement better hashing method
    public override int GetHashCode()
    {
        unchecked
        {
            var result = FSegment.GetHashCode();
            result = (result*397) ^ GSegment.GetHashCode();
            result = (result*397) ^ ESegment.GetHashCode();
            return result;
        }
    }
}