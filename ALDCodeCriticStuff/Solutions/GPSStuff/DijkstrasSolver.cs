using System.Collections.ObjectModel;

namespace ALDCodeCriticStuff.Solutions.GPSStuff;

public class DijkstrasSolver
{
    private int StartCityIndex { get; set; }
    private int EndCityIndex { get; set; }
    private bool[] IsVisited { get; set; }
    private List<Vertex> Vertices { get; set; }
    private ReadOnlyCollection<ReadOnlyCollection<ulong>> Distances { get; }

    private DijkstrasSolver(
        int startCityIndex,
        int endCityIndex,
        ReadOnlyCollection<ReadOnlyCollection<ulong>> distances)
    {
        this.StartCityIndex = startCityIndex; 
        this.EndCityIndex = endCityIndex;
        this.IsVisited = new bool[CityData.Cities.Count];
        
        // fill the array with false
        for (var i = 0; i < this.IsVisited.Length; i++) this.IsVisited[i] = false;

        this.Distances = distances;
        
        this.Vertices = new List<Vertex>();
    }
    
    public static DijkstrasSolver Create(IReadOnlyList<int> data)
    {
        // vars for better code readability
        var startingCityIndex = data[0];
        var endCityIndex = data[1];
        var distanceTable = 
            data[2] is 0 ? CityData.Distances : CityData.TimeDistances;
        
        return new DijkstrasSolver(startingCityIndex, endCityIndex, distanceTable);
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

    // records each vertex that is not yet visited and has a distance to the current vertex
    private void MapAllVertices(Vertex currentVertex)
    {
        // until all vertices are visited
        while (this.IsVisited.Any(x => x == false))
        {
            // mark the current vertex as visited
            this.IsVisited[currentVertex!.Id] = true;
            
            // record all vertices that are not yet visited and have a distance to the current vertex
            for (var i = 0; i < this.Distances[currentVertex.Id].Count; i++)
            {
                if (!this.IsVisited[i] && this.Distances[currentVertex.Id][i] is not CityData.Infinity and not 0)
                {
                    this.Vertices.Add(new Vertex(i, currentVertex, this.Distances[currentVertex.Id][i]));
                }
            }
            
            // if the end city is already visited, we can stop
            if (this.IsVisited[this.EndCityIndex]) break;
            
            // get the next vertex to visit
            currentVertex = this.Vertices
                .Where(x => this.IsVisited[x.Id] is false).MinBy(x => x.DistanceToPreviousVertex)!;
            
        }
        
        // if (this.IsVisited[currentVertex.Id]) return;
        //
        // var distances = this.Distances[currentVertex.Id];
        // foreach (var path in distances)
        // {
        //     if(path is CityData.Infinity) continue;
        //         
        //     var idOfNextCity = distances.IndexOf(path);
        //     var vertex = new Vertex(idOfNextCity, currentVertex, path);
        //     Vertices.Add(vertex);
        //     this.IsVisited[currentVertex.Id] = true;
        //     this.MapAllVertices(vertex); // oof
        // }
    }
    

    public void Solve()
    {
        var startingVertex = new Vertex(this.StartCityIndex, null, 0);
        this.Vertices.Add(startingVertex);
        this.MapAllVertices(startingVertex);
        
        // get vertex with index of end city
        var endVertex = this.Vertices.Find(v => v.Id == this.EndCityIndex);
        var totalDistance = GetTotalDistanceToStartFromGivenVertex(endVertex!);
        var path = GetAllCitiesOnThePathToStartFromGivenVertex(endVertex!);
        Console.WriteLine(path);
        Console.WriteLine(totalDistance);
        
    }
}