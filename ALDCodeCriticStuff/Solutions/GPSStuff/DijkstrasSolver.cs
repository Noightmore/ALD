namespace ALDCodeCriticStuff.Solutions.GPSStuff;

public class DijkstrasSolver
{
    public Vertex CurrentVertex { get; set; }
    
    private DijkstrasSolver(Vertex startVertex)
    {
        this.CurrentVertex = startVertex;
    }
    
    public static DijkstrasSolver Create(int startingCityIndex)
    {
        return new DijkstrasSolver(new Vertex(startingCityIndex, null, 0));
    }

    public int AddNextVertex(int idOfNextCity, ulong distance)
    {
        try
        {
            var vertex = new Vertex(idOfNextCity, this.CurrentVertex, distance);
            this.CurrentVertex = vertex;
        }
        catch (Exception)
        {
            return 2;
        }
        return 0;
    }
    
    
    // thankfully this way of getting the total distance is O(n)
    public ulong GetTotalDistance()
    {
        var tempVertex = this.CurrentVertex;
        ulong totalDistance = 0;
        while(tempVertex.PreviousVertex != null)
        {
            totalDistance += tempVertex.DistanceToPreviousVertex;
            tempVertex = tempVertex.PreviousVertex;
        }
        return totalDistance;
    }
}