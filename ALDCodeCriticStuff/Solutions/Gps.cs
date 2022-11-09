using ALDCodeCriticStuff.Solutions.GPSStuff;

namespace ALDCodeCriticStuff.Solutions;

public static class Gps
{
    public static void Evaluate()
    {
        IReadOnlyList<int>? input;
        do
        {
            input = GetUserInput();
            if (input == null) continue;
            
            // var solution = ComputeShortestPath(input);
            // Console.WriteLine(solution);
            var solver = DijkstrasSolver.Create(input);
            solver.Solve();
            
        } while (input is not null);
    }

    private static IReadOnlyList<int>? GetUserInput()
    {
        // 0th index = start city index
        // 1st index = end city index
        // 2nd index = 0 goes for distance, 1 goes for time
        var info = new int[3];

        var line = Console.ReadLine();
        if(string.IsNullOrEmpty(line)) return null;
        
        var data = line.Split(' ');
        if(data.Length != 3) return null; // invalid input case
        
        // parse the start city index   
        if(CityData.Cities.Contains(data[0]))
        {
            info[0] = CityData.Cities.IndexOf(data[0]);
        }
        else
        {
            return null; // invalid input case
        }
        
        // parse the end city index
        if(CityData.Cities.Contains(data[1]))
        {
            info[1] = CityData.Cities.IndexOf(data[1]);
        }
        else
        {
            return null; // invalid input case
        }
        
        // parse the distance or time
        switch (data[2])
        {
            case "nejkratsi":
                info[2] = 0;
                break;
            case "nejlepsi":
                info[2] = 1;
                break;
            default:
                return null; // invalid input case
        }
        
        return info;
    }

    // does not work
    private static string ComputeShortestPath(IReadOnlyList<int> input)
    {
        #region Initialisation
        
        var startCityIndex = input[0];
        var currentCityIndex = startCityIndex;
        var endCityIndex = input[1];
        
        var currentDistanceTable = 
            input[2] == 0 ? CityData.Distances : CityData.TimeDistances;
        
        var visitedCities = new Dictionary<int, bool>();
        for (var i = 0; i < CityData.Cities.Count; i++)
        {
            visitedCities.Add(i, false);
        }
        
        visitedCities[startCityIndex] = true;

        var progressTracker = new List<ulong[]>();
        var currentState = new ulong[CityData.Cities.Count];
        for (var i = 0; i < CityData.Cities.Count; i++)
        {
            if (i == startCityIndex)
            {
                currentState[i] = 0;
                continue;
            }
            currentState[i] = CityData.Infinity;
        }
        progressTracker.Add(currentState);
        
        var smallestDistanceIndex = currentCityIndex;
        var solvingCycleIndex = progressTracker.Count;
        #endregion
        
        #region Dijkstra solver
        
        var totalDistance = (ulong) 0;
        
        while(currentCityIndex != endCityIndex && solvingCycleIndex < CityData.Cities.Count)
        {
            var smallestDistance = (ulong) CityData.Infinity;
            
            progressTracker.Add(new ulong[CityData.Cities.Count]);
            solvingCycleIndex = progressTracker.Count-1;
            
            for (var i = 0; i < CityData.Cities.Count; i++)
            {
                // if we are at the current city the solver is at or have visited, skip
                if (visitedCities[i]) continue;
                
                // a distance to the current city being pointed
                // from the perspective of the current city the solver is at
                var distanceToPointedCity = currentDistanceTable[currentCityIndex][i];
                
                // if connection doesn't exist, skip
                if (distanceToPointedCity is CityData.Infinity) continue;

                // if the distance to the current city being pointed is shorter than the previous shortest distance
                // to the current city being pointed, update the shortest distance
                if (progressTracker[solvingCycleIndex - 1][i] > distanceToPointedCity + totalDistance)
                {
                    progressTracker[solvingCycleIndex][i] = distanceToPointedCity + totalDistance;
                }
                else
                {
                    progressTracker[solvingCycleIndex - 1][i] = progressTracker[solvingCycleIndex][i] + totalDistance;
                }

                if (smallestDistance <= distanceToPointedCity) continue;
                smallestDistance = distanceToPointedCity;
                smallestDistanceIndex = i;
                
            }
            totalDistance += smallestDistance;
            currentCityIndex = smallestDistanceIndex;
            // resolve updating of current city index and keeping record of the smallest distance index
        }
        
        #endregion
        
        
        // return the result  
        return $"{CityData.Cities[startCityIndex]} -> {CityData.Cities[endCityIndex]}: {totalDistance}";
        
    }
}
