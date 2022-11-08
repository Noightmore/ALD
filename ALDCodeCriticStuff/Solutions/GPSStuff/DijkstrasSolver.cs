namespace ALDCodeCriticStuff.Solutions.GPSStuff;

public class DijkstrasSolver
{
    public Vertex CurrentVertex { get; set; }
    public int EndCityIndex { get; set; }
    public bool[] IsVisited { get; set; }
    
    //TODO: store reference to CityData.Distances;
    
    private DijkstrasSolver(Vertex startVertex, int endCityIndex)
    {
        this.CurrentVertex = startVertex;
        this.EndCityIndex = endCityIndex;
        this.IsVisited = new bool[CityData.Cities.Count];
    }
    
    public static DijkstrasSolver Create(int startingCityIndex, int endCityIndex)
    {
        var startingVertex = new Vertex(startingCityIndex, null, 0);
        return new DijkstrasSolver(startingVertex, endCityIndex);
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