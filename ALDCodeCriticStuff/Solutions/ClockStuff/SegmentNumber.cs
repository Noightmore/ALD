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
        
        #region One Segment
        {
            HashSegments(
                new Type1(6, 0, 30),
                new Type2(12, 0,0),
                null
                ),
            
            (byte) 1
        },
        #endregion
        
         #region Two Segment
         {
             HashSegments(
                 new Type1(6, 45, 30),
                 new Type2(9, 45, 45),
                 new Type3(3, 30, 15)
                 ),
             
             (byte) 2
         },
        #endregion
        
         #region Three Segment
         {
             HashSegments(
                 new Type1(9, 30, 30),
                 new Type2(9, 45,0),
                 new Type3(3, 15,15)
                 ),
             
             (byte) 3
         },
         #endregion
        
        #region Four Segment
        {
            HashSegments(
                new Type1(6, 30, 30),
                new Type2(0, 0,0),
                new Type3(0, 15,15)
                ), 
            
            (byte) 4
        },
        #endregion
        
        #region Five Segment
        {
            HashSegments(
                new Type1(9, 45, 30),
                new Type2(0, 0,45),
                new Type3(3, 15,0)
                ),
            
            (byte) 5
        },
        #endregion
    };

    public static byte GetSegmentNumber(AnalogClock? t1, AnalogClock? t2, AnalogClock? t3)
    {
        var hash = HashSegments(t1, t2, t3);
        
        // for debug
        foreach(DictionaryEntry ele1 in Segments)
        {
            Console.WriteLine("{0} and {1} ", ele1.Key, ele1.Value);
        }

        var segment = GetSegmentValueAndCheckValidity(hash);
        Console.WriteLine(hash);
        if (segment is not 255) return segment;
        Console.WriteLine("Invalid segment");
        return 255;

    }
    
    private static string HashSegments(AnalogClock? t1, AnalogClock? t2, AnalogClock? t3)
    {
        var hash = new System.Security.Cryptography.SHA256CryptoServiceProvider().ComputeHash(AnalogClock.GetSegmentRawData(t1, t2, t3));
        return AnalogClock.ByteArrayToString(hash);
    }

    private static byte GetSegmentValueAndCheckValidity(string hash)
    {
        byte seg;
        try
        {
            seg = (byte) Segments[hash]!;
        }
        catch (NullReferenceException ex)
        {
            return 255; // a return code for invalid segment
        }
        
        return seg;
    }
}