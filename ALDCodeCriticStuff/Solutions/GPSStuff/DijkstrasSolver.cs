using System.Collections.ObjectModel;

namespace ALDCodeCriticStuff.Solutions.GPSStuff;

public class DijkstrasSolver
{
    private DijkstrasSolver(
        int startCityIndex,
        int endCityIndex,
        ReadOnlyCollection<ReadOnlyCollection<ulong>> distances,
        ReadOnlyCollection<ReadOnlyCollection<ulong>> secondaryDistances, bool isPrimaryDistanceTime)
    {
        StartCityIndex = startCityIndex;
        EndCityIndex = endCityIndex;
        IsVisited = new bool[CityData.Cities.Count];

        // fill the array with false
        for (var i = 0; i < IsVisited.Length; i++) IsVisited[i] = false;

        Distances = distances;
        SecondaryDistances = secondaryDistances;
        IsPrimaryDistanceTime = isPrimaryDistanceTime;

        Vertices = new List<Vertex>();
    }

    private int StartCityIndex { get; }
    private int EndCityIndex { get; }
    private bool[] IsVisited { get; }
    private List<Vertex> Vertices { get; }
    private bool IsPrimaryDistanceTime { get; }
    private ReadOnlyCollection<ReadOnlyCollection<ulong>> Distances { get; }
    private ReadOnlyCollection<ReadOnlyCollection<ulong>> SecondaryDistances { get; }

    public static DijkstrasSolver Create(ref IReadOnlyList<int> data)
    {
        // vars for better code readability
        var startingCityIndex = data[0];
        var endCityIndex = data[1];
        var distanceTable =
            data[2] is 0 ? ref CityData.Distances : ref CityData.TimeDistances;
        var secondaryDistanceTable =
            data[2] is 0 ? ref CityData.TimeDistances : ref CityData.Distances;
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
        while (tempVertex.PreviousVertex != null)
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
        while (tempVertex.PreviousVertex != null)
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
        while (tempVertex.PreviousVertex != null)
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
        while (IsVisited.Any(x => x == false))
        {
            // mark the current vertex as visited
            IsVisited[currentVertex.Id] = true;

            // record all vertices that are not yet visited and have a distance to the current vertex
            for (var i = 0; i < Distances[currentVertex.Id].Count; i++)
            {
                if (Distances[currentVertex.Id][i] is CityData.Infinity) continue;
                
                Vertices.Add(
                    new Vertex(
                        i,
                        currentVertex,
                        Distances[currentVertex.Id][i],
                        SecondaryDistances[currentVertex.Id][i]
                    ));
                // debug:
                // Console.WriteLine(
                //     $"Added backwards path from {CityData.Cities[currentVertex.Id]} to {CityData.Cities[i]} of length {this.Distances[currentVertex.Id][i]}");
            }

            // if the end city is already visited, we can stop
            if (IsVisited[EndCityIndex]) break;

            // get the next vertex to visit
            currentVertex = Vertices
                .Where(x => IsVisited[x.Id] is false).MinBy(x => x.DistanceToPreviousVertex)!;
            // more debug logging:
            //Console.WriteLine($"Next vertex to visit is {CityData.Cities[currentVertex.Id]}");
        }
    }


    public string Solve()
    {
        var startingVertex =
            new Vertex(StartCityIndex, null, 0, 0);
        MapAllVertices(startingVertex);

        // co kdyz existuji 2 nejkratsi cesty do cile? 
        // kod realne uklada vsechny existujici cesty do cile, ale vypise jen tu prvni na kterou narazi
        //Console.WriteLine(string.Join(", ", Vertices.Where(v => v.Id == EndCityIndex).Select(v => $"{CityData.Cities[v.Id]}: {GetTotalDistanceToStartFromGivenVertex(v)}")));

        var endVertex = Vertices.Where(v => v.Id == EndCityIndex).MinBy(GetTotalDistanceToStartFromGivenVertex)!;
        var totalDistance = GetTotalDistanceToStartFromGivenVertex(endVertex);
        var totalSecondaryDistance = GetTheTotalSecondaryDistanceToStartFromGivenVertex(endVertex);
        var path = GetAllCitiesOnThePathToStartFromGivenVertex(endVertex);

        var first = IsPrimaryDistanceTime ? totalDistance : totalSecondaryDistance;
        var second = !IsPrimaryDistanceTime ? totalDistance : totalSecondaryDistance;

        return $"({first} min, {second} km) {path}";
    }
}