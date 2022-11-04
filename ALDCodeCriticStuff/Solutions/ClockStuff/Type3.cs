// ReSharper disable NonReadonlyMemberInGetHashCode

namespace ALDCodeCriticStuff.Solutions.ClockStuff;

public class Type3 : AnalogClock
{
    private PoweredState FSegment { get; set; }
    private PoweredState GSegment { get; set; }
    private PoweredState ESegment { get; set; }

    public Type3(byte hour, byte minute, byte second) : base(hour, minute, second)
    {
        this.ComputeSegments();
    }
    
    protected internal sealed override void ComputeSegments()
    {
        if (Hour is 24 or 12 or 0 || Minute is 0 or 60 || Second is 0 or 60)
        {
            FSegment = PoweredState.On;
        }
        else
        {
            this.FSegment = PoweredState.Off;
        }
        if (Hour is 3 or 15 || Minute is 15 || Second is 15)
        {
            GSegment = PoweredState.On;
        }
        else
        {
            this.GSegment = PoweredState.Off;
        }
        if (Hour is 6 or 18 || Minute is 30 || Second is 30)
        {
            ESegment = PoweredState.On;
        }
        else
        {
            this.ESegment = PoweredState.Off;
        }
    }

    protected override byte[] GetSegmentRawData()
    {
        return ObjectToByteArray(ESegment)
            .Concat(ObjectToByteArray(GSegment)).ToArray()
            .Concat(ObjectToByteArray(FSegment)).ToArray();
    }
}