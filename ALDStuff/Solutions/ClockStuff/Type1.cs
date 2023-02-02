// ReSharper disable NonReadonlyMemberInGetHashCode

namespace ALDCodeCriticStuff.Solutions.ClockStuff;

public class Type1 : AnalogClock
{
    public Type1(byte hour, byte minute, byte second) : base(hour, minute, second)
    {
        ComputeSegments();
    }

    private PoweredState ASegment { get; set; }
    private PoweredState BSegment { get; set; }

    protected internal sealed override void ComputeSegments()
    {
        if (Hour is 9 or 21 || Minute is 45 || Second is 45)
            ASegment = PoweredState.On;
        else
            ASegment = PoweredState.Off;
        if (Hour is 6 or 18 || Minute is 30 || Second is 30)
            BSegment = PoweredState.On;
        else
            BSegment = PoweredState.Off;
    }

    protected override byte[] GetSegmentRawData()
    {
        return ObjectToByteArray(ASegment)
            .Concat(ObjectToByteArray(BSegment)).ToArray();
    }
}