using System.Collections.ObjectModel;

namespace ALDCodeCriticStuff.Solutions.GPSStuff;

public class DijkstrasSolver
{
    public Vertex CurrentVertex { get; set; }
    public int EndCityIndex { get; set; }
    public bool[] IsVisited { get; set; }
    
    public ReadOnlyCollection<ReadOnlyCollection<ulong>> Distances { get; }

    private DijkstrasSolver(Vertex startVertex, int endCityIndex, ReadOnlyCollection<ReadOnlyCollection<ulong>> distances)
    {
        this.CurrentVertex = startVertex; 
        this.EndCityIndex = endCityIndex;
        this.IsVisited = new bool[CityData.Cities.Count];
        this.Distances = distances;
    }
    
    public static DijkstrasSolver Create(byte[] data)
    {
        var startingVertex = new Vertex(data[0], null, 0);
        var endCityIndex = data[1];
        var distanceTable = data[2] == 0 ? CityData.Distances : CityData.TimeDistances;
        
        return new DijkstrasSolver(startingVertex, endCityIndex, distanceTable);
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