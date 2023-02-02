using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ALDCodeCriticStuff.Solutions.ClockStuff;

public abstract class AnalogClock
{
    // perhaps add a way to directly set segments
    protected AnalogClock(byte hour, byte minute, byte second)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
    }

    // unsigned byte 0-255
    protected byte Hour { get; }
    protected byte Minute { get; }
    protected byte Second { get; }

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
    protected internal abstract void ComputeSegments();

    protected abstract byte[] GetSegmentRawData();

    public static byte[] GetSegmentRawData(AnalogClock? t1, AnalogClock? t2, AnalogClock? t3)
    {
        var defaultValue = new byte[] {0};

        return (t1?.GetSegmentRawData() ?? defaultValue)
            .Concat(t2?.GetSegmentRawData() ?? defaultValue).ToArray()
            .Concat(t3?.GetSegmentRawData() ?? defaultValue).ToArray();
    }

    // Binary formatter should not be used in production
    // https://learn.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide
    // Making it internal use only could make it more secure

    // the 2 methods below were taken from:
    // https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/compute-hash-values

    // ReSharper disable once ReturnTypeCanBeEnumerable.Global
    internal static byte[] ObjectToByteArray(object obj)
    {
        var bf = new BinaryFormatter();
        var ms = new MemoryStream();
#pragma warning disable SYSLIB0011
        bf.Serialize(ms, obj);
#pragma warning restore SYSLIB0011
        return ms.ToArray();
    }

    public static string ByteArrayToString(byte[] arrInput)
    {
        int i;
        var sOutput = new StringBuilder(arrInput.Length);
        for (i = 0; i < arrInput.Length; i++) sOutput.Append(arrInput[i].ToString("X2"));
        return sOutput.ToString();
    }
}