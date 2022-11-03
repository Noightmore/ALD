using System.Collections;

namespace ALDCodeCriticStuff.Solutions.ClockStuff;

public static class SegmentNumber
{
    private static readonly Hashtable Segments = new()
    {
        #region Zero Segment
        {
            HashSegments(
                new Type1(9, 30, 30),
                new Type2(12, 45 , 0),
                new Type3(12, 0, 30)
                ),
            
            (byte) 0
        },
        #endregion
        
        {
            new Type1(6, 0, 30).GetHashCode()^
            new Type2(12, 0,0).GetHashCode()^
            1,
            
            (byte) 1
        },
        {
            new Type1(9, 30, 30).GetHashCode()^
            new Type2(9, 45,45).GetHashCode()^
            new Type3(3, 30,15).GetHashCode(),
            
            2
        },
        // {
        //     new Type1(9, 30, 30).GetHashCode()^
        //     new Type2(9, 45,0).GetHashCode()^
        //     new Type3(3, 15,15).GetHashCode(),
        //     
        //     3
        // },
        // {
        //     new Type1(6, 30, 30).GetHashCode()^ 
        //     new Type2(0, 0,0).GetHashCode()^
        //     new Type3(0, 15,15).GetHashCode(),
        //     
        //     4
        // },
        // {
        //     new Type1(9, 45, 30).GetHashCode()^
        //     new Type2(0, 0,45).GetHashCode()^
        //     new Type3(3, 15,0).GetHashCode(),
        //     
        //     5
        // }
        //
    };

    public static byte GetSegmentNumber(AnalogClock? t1, AnalogClock? t2, AnalogClock? t3)
    {
        var hash = t1?.GetHashCode() ?? 1 ^ t2?.GetHashCode() ?? 1 ^ t3?.GetHashCode() ?? 1;
        foreach(DictionaryEntry ele1 in Segments)
        {
            Console.WriteLine("{0} and {1} ", ele1.Key, ele1.Value);
        }

        Console.WriteLine(hash);
        return (byte) Segments[hash]!;
        //return 0;
    }

    public static int HashSegments(AnalogClock? t1, AnalogClock? t2, AnalogClock? t3)
    {
        var result = t1?.GetHashCode() ?? 0;
        result = (result*397) ^ t2?.GetHashCode() ?? 0;
        result = (result * 397) ^ t3?.GetHashCode() ?? 0;
        return result;
    }
}