#include <stdio.h>

void printMatrix_VertexConnections(char*** matrix, const long* rowSize, const long* colSize)
{
    for (long i = 0; i < *rowSize; ++i)
    {
        for (long j = 0; j < *colSize; ++j)
        {
            if (*(*(*matrix + i) + j))
            {
                printf("%c -> %c ", (char) i+65, (char) j+65);
            }
        }
    }
    printf("\n");
}