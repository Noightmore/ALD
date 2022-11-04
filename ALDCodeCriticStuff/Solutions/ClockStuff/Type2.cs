// ReSharper disable NonReadonlyMemberInGetHashCode

namespace ALDCodeCriticStuff.Solutions.ClockStuff;

public class Type2 : AnalogClock
{
    public Type2(byte hour, byte minute, byte second) : base(hour, minute, second)
    {
        ComputeSegments();
    }

    private PoweredState CSegment { get; set; }
    private PoweredState DSegment { get; set; }

    protected internal sealed override void ComputeSegments()
    {
        if (Hour is 24 or 12 or 0 || Minute is 0 or 60 || Second is 0 or 60)
            CSegment = PoweredState.On;
        else
            CSegment = PoweredState.Off;
        if (Hour is 9 or 21 || Minute is 45 || Second is 45)
            DSegment = PoweredState.On;
        else
            DSegment = PoweredState.Off;
    }

    protected override byte[] GetSegmentRawData()
    {
        return ObjectToByteArray(CSegment)
            .Concat(ObjectToByteArray(DSegment)).ToArray();
    }
}