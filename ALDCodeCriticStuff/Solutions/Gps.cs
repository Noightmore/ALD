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
}
