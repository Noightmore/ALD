// ReSharper disable InconsistentNaming
namespace ALDCodeCriticStuff.Solutions.GPSStuff;

public class Vertex
{
    public int Id { get; set; } // id of the city
    
    public Vertex? PreviousVertex { get; set; }
    public ulong DistanceToPreviousVertex { get; set; }
    
    
    public Vertex(int id, Vertex? previousVertex, ulong distanceToPreviousVertex)
    {
        Id = id;
        PreviousVertex = previousVertex;
        DistanceToPreviousVertex = distanceToPreviousVertex;
    }
    
    
}