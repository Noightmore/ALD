// ReSharper disable NonReadonlyMemberInGetHashCode
namespace ALDCodeCriticStuff.Solutions.ClockStuff;

public class Type2 : AnalogClock
{
    private PoweredState CSegment { get; set; }
    private PoweredState DSegment { get; set; }

    public Type2(byte hour, byte minute, byte second) : base(hour, minute, second)
    {
        this.ComputeSegments();
    }
    
    protected internal sealed override void ComputeSegments()
    {
        if (Hour is 24 or 12 or 0 || Minute is 0 or 60 || Second is 0 or 60)
        {
            CSegment = PoweredState.On;
        }
        else
        {
            this.CSegment = PoweredState.Off;
        }
        if (Hour is 9 or 21|| Minute is 45 || Second is 45)
        {
            DSegment = PoweredState.On;
        }
        else
        {
            this.DSegment = PoweredState.Off;
        }
    }

    protected override byte[] GetSegmentRawData()
    {
        return ObjectToByteArray(CSegment)
            .Concat(ObjectToByteArray(DSegment)).ToArray();
    }
}