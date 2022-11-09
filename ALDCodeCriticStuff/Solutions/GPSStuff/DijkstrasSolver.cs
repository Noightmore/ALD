using System.Collections.ObjectModel;

namespace ALDCodeCriticStuff.Solutions.GPSStuff;

public class DijkstrasSolver
{
    private int StartCityIndex { get; set; }
    private int EndCityIndex { get; set; }
    private bool[] IsVisited { get; set; }
    private List<Vertex> Vertices { get; set; }
    private bool IsPrimaryDistanceTime { get; set; }
    private ReadOnlyCollection<ReadOnlyCollection<ulong>> Distances { get; }
    private ReadOnlyCollection<ReadOnlyCollection<ulong>> SecondaryDistances { get; }

    private DijkstrasSolver(
        int startCityIndex,
        int endCityIndex,
        ReadOnlyCollection<ReadOnlyCollection<ulong>> distances,
        ReadOnlyCollection<ReadOnlyCollection<ulong>> secondaryDistances, bool isPrimaryDistanceTime)
    {
        this.StartCityIndex = startCityIndex; 
        this.EndCityIndex = endCityIndex;
        this.IsVisited = new bool[CityData.Cities.Count];
        
        // fill the array with false
        for (var i = 0; i < this.IsVisited.Length; i++) this.IsVisited[i] = false;

        this.Distances = distances;
        SecondaryDistances = secondaryDistances;
        this.IsPrimaryDistanceTime = isPrimaryDistanceTime;

        this.Vertices = new List<Vertex>();
    }
    
    public static DijkstrasSolver Create(IReadOnlyList<int> data)
    {
        // vars for better code readability
        var startingCityIndex = data[0];
        var endCityIndex = data[1];
        var distanceTable = 
            data[2] is 0 ? CityData.Distances : CityData.TimeDistances;
        var secondaryDistanceTable = 
            data[2] is 0 ? CityData.TimeDistances : CityData.Distances;
        var primaryDistanceType = data[2] is not 0;
        return new DijkstrasSolver(
            startingCityIndex,
            endCityIndex, 
            distanceTable,
            secondaryDistanceTable,
            primaryDistanceType);
    }


    // thankfully this way of getting the total distance is O(n)
    private static ulong GetTotalDistanceToStartFromGivenVertex(Vertex vertex)
    {
        var tempVertex = vertex;
        ulong totalDistance = 0;
        while(tempVertex.PreviousVertex != null)
        {
            totalDistance += tempVertex.DistanceToPreviousVertex;
            tempVertex = tempVertex.PreviousVertex;
        }
        return totalDistance;
    }
    
    private static string GetAllCitiesOnThePathToStartFromGivenVertex(Vertex vertex)
    {
        var tempVertex = vertex;
        var cities = new List<string>();
        while(tempVertex.PreviousVertex != null)
        {
            cities.Add(CityData.Cities[tempVertex.Id]);
            tempVertex = tempVertex.PreviousVertex;
        }
        cities.Add(CityData.Cities[tempVertex.Id]);
        cities.Reverse();
        return string.Join(" -> ", cities);
    }

    private static ulong GetTheTotalSecondaryDistanceToStartFromGivenVertex(Vertex vertex)
    {
        var tempVertex = vertex;
        ulong totalDistance = 0;
        while(tempVertex.PreviousVertex != null)
        {
            totalDistance += tempVertex.SecondaryDistanceValue;
            tempVertex = tempVertex.PreviousVertex;
        }
        return totalDistance;
    }

    // records each vertex that is not yet visited and has a distance to the current vertex
    private void MapAllVertices(Vertex currentVertex)
    {
        // until all vertices are visited
        while (this.IsVisited.Any(x => x == false))
        {
            // mark the current vertex as visited
            this.IsVisited[currentVertex.Id] = true;
            
            // record all vertices that are not yet visited and have a distance to the current vertex
            for (var i = 0; i < this.Distances[currentVertex.Id].Count; i++)
            {
                if (!this.IsVisited[i] && this.Distances[currentVertex.Id][i] is not CityData.Infinity)
                {
                    this.Vertices.Add(
                        new Vertex(
                            i, 
                            currentVertex,
                            this.Distances[currentVertex.Id][i],
                            this.SecondaryDistances[currentVertex.Id][i]
                            ));
                    
                    // debug:
                    //Console.WriteLine($"Added path {CityData.Cities[currentVertex.Id]}  to {CityData.Cities[i]} of length {this.Distances[currentVertex.Id][i]}");
                }
            }
            
            // if the end city is already visited, we can stop
            if (this.IsVisited[this.EndCityIndex]) break;
            
            // get the next vertex to visit
            currentVertex = this.Vertices
                .Where(x => this.IsVisited[x.Id] is false).MinBy(x => x.DistanceToPreviousVertex)!;
            // more debug logging:
            //Console.WriteLine($"Next vertex to visit is {CityData.Cities[currentVertex.Id]}");
            
        }
    }
    

    public void Solve()
    {
        var startingVertex = 
            new Vertex(this.StartCityIndex, null, 0, 0);
        this.MapAllVertices(startingVertex);
        
        // co kdyz existuji 2 nejkratsi cesty do cile? 
        // napr: liberec -> chrastava -> new-york -> ceska-lipa
        // asi vrati ten prvni co najde, jako je to jedno realne
        
        var endVertex = this.Vertices.Where(v => v.Id == this.EndCityIndex).MinBy(GetTotalDistanceToStartFromGivenVertex)!;
        var totalDistance = GetTotalDistanceToStartFromGivenVertex(endVertex);
        var totalSecondaryDistance = GetTheTotalSecondaryDistanceToStartFromGivenVertex(endVertex);
        var path = GetAllCitiesOnThePathToStartFromGivenVertex(endVertex);

        var first = IsPrimaryDistanceTime ? totalDistance : totalSecondaryDistance;
        var second = !IsPrimaryDistanceTime ? totalDistance : totalSecondaryDistance;
        Console.WriteLine($"({first} min, {second} km) {path}");
        
    }
}