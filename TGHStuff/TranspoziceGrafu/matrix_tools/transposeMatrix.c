#include <unistd.h>

char** transposeMatrix(char ***matrix, const long *rowSize, const long *colSize)
{
    char **transposedMatrix = sbrk(*colSize * sizeof(char*)); // NOLINT(cppcoreguidelines-narrowing-conversions)

    for (int i = 0; i < *colSize; i++)
    {
        *(transposedMatrix + i) = sbrk(*rowSize * sizeof(char)); // NOLINT(cppcoreguidelines-narrowing-conversions)
        for (int j = 0; j < *rowSize; j++)
        {
            *(*(transposedMatrix + i) + j) = (*matrix)[j][i]; // NOLINT(cppcoreguidelines-narrowing-conversions)
        }
    }
    return transposedMatrix;
}
