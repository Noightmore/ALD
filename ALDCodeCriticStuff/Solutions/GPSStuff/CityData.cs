using System.Collections.ObjectModel;

namespace ALDCodeCriticStuff.Solutions.GPSStuff;

public readonly struct CityData
{
    public static readonly ReadOnlyCollection<string> Cities = new(new[]
    {
        "liberec",
        "ceska-lipa",
        "chrastava",
        "new-york",
        "turnov",
        "jablonec-nad-nisou"
    });
    
    // maximum unsigned 32-bit integer value
    public const ulong Infinity = ulong.MaxValue;
    
    // max distance between two cities means that there is no connection between them
    public static readonly ReadOnlyCollection<ReadOnlyCollection<ulong>> TimeDistances = new(new[]
    {
        new ReadOnlyCollection<ulong>(new ulong[] { Infinity, Infinity, 12, 24, 22, 20 }),
        new ReadOnlyCollection<ulong>(new ulong[] { Infinity, Infinity, 40, 10, 52, Infinity }),
        new ReadOnlyCollection<ulong>(new ulong[] { 12, 40, Infinity, 20, Infinity, Infinity }),
        new ReadOnlyCollection<ulong>(new ulong[] { 24, 10, 20, Infinity, 15, 30 }),
        new ReadOnlyCollection<ulong>(new ulong[] { 22, 52, Infinity, 15, Infinity, 22 }),
        new ReadOnlyCollection<ulong>(new ulong[] { 20, Infinity, Infinity, 30, 22, Infinity })
    }); 
    
    public static readonly ReadOnlyCollection<ReadOnlyCollection<ulong>> Distances = new(new[]
    {
        new ReadOnlyCollection<ulong>(new ulong[] { Infinity, Infinity, 10, 35, 26, 20 }),
        new ReadOnlyCollection<ulong>(new ulong[] { Infinity, Infinity, 47, 30, 67, Infinity }),
        new ReadOnlyCollection<ulong>(new ulong[] { 10, 47, Infinity, 14, Infinity, Infinity }),
        new ReadOnlyCollection<ulong>(new ulong[] { 35, 30, 14, Infinity, 40, 30 }),
        new ReadOnlyCollection<ulong>(new ulong[] { 26, 67, Infinity, 40, Infinity, 24 }),
        new ReadOnlyCollection<ulong>(new ulong[] { 20, Infinity, Infinity, 30, 24, Infinity })
    });
}