using ALDCodeCriticStuff.Solutions.ClockStuff;

namespace ALDCodeCriticStuff.Solutions;

public static class ClockTo7SegmentNumber
{
    public static void Evaluate()
    {
        var number = "";
        
        while (true)
        {
            AnalogClock? t1 = null, t2 = null, t3 = null;

            for (byte i = 1; i < 4; i++)
            {
                var line = Console.ReadLine()?.Split(" ");
                if (line is null) break;
                
                
                // TODO: refactor this
                switch (i)
                {
                    case 1:
                    {
                        if (line.Last().Equals("broken"))
                        {
                            t1 = null;
                            break;
                        }

                        var data = line.Last().Split(":");
                        t1 = AnalogClock.Generate(
                            byte.Parse(data[0]),
                            byte.Parse(data[1]), 
                            byte.Parse(data[2]), 
                            i
                            );
                        break;
                    }
                    case 2:
                    {
                        if (line.Last().Equals("broken"))
                        {
                            t1 = null;
                            break;
                        }

                        var data = line.Last().Split(":");
                        t2 = AnalogClock.Generate(
                            byte.Parse(data[0]),
                            byte.Parse(data[1]),
                            byte.Parse(data[2]),
                            i
                        );
                        break;
                    }
                    case 3:
                    {
                        if (line.Last().Equals("broken"))
                        {
                            t3 = null;
                            break;
                        }

                        var data = line.Last().Split(":");
                        t3 = AnalogClock.Generate(
                            byte.Parse(data[0]),
                            byte.Parse(data[1]),
                            byte.Parse(data[2]),
                            i
                        );
                        break;
                    }
                }
            }


            number += SegmentNumber.GetSegmentNumber(t1, t2, t3);
            var endOrNot = Console.ReadLine();
            
            if (endOrNot is null) break;
            if (!endOrNot.Equals("-")) break;
        }

        Console.WriteLine(number);
    }
}