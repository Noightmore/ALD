// ReSharper disable InconsistentNaming

namespace ALDCodeCriticStuff.Solutions.GPSStuff;

public class Vertex
{
    public Vertex(int id, Vertex? previousVertex, ulong distanceToPreviousVertex, ulong secondaryDistanceValue)
    {
        Id = id;
        PreviousVertex = previousVertex;
        DistanceToPreviousVertex = distanceToPreviousVertex;
        SecondaryDistanceValue = secondaryDistanceValue;
    }

    public int Id { get; set; } // id of the city

    public Vertex? PreviousVertex { get; set; }
    public ulong DistanceToPreviousVertex { get; set; }

    public ulong SecondaryDistanceValue { get; set; }
}