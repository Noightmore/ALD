namespace TGHStuff.Solutions;

public static class Transpose
{
    public static void Evaluate()
    {
        var dims = Console.ReadLine() ?? string.Empty;
        var colCount = int.Parse(dims.Split(' ')[0]);
        var rowCount = int.Parse(dims.Split(' ')[1]); 
        
        var matrix = new int[colCount, rowCount] ;
        for (var i = 0; i < colCount; i++)
        {
            var row = Console.ReadLine() ?? string.Empty;
            var items = row.Split(" ");
            for (var j = 0; j < items.Length; j++)
            {
                matrix[j, i] = int.Parse(items[j]);
            }
        }

        Console.WriteLine("");
        PrintMatrix(matrix);

    }

    private static void PrintMatrix<T>(T[,] matrix)
    {
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            var output = "";
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                output += matrix[i, j];
                output += " ";
            }
            Console.WriteLine(output);
        }
    }

    private static void PrintFormattedMatrix<T>(T[,] matrix)
    {
        
    }
}