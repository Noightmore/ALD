#include <stdio.h>
#include <unistd.h>
#include <alloca.h>
#include <string.h>
#include <stdlib.h>

#include "headerFiles/loadMatrix_FromInput.h"

char **loadMatrix_FromInput(long *rowSize, long *colSize)
{

    char buffer[256];

    fgets(buffer, 256, stdin);

    char *token = strtok(buffer, " ");
    *rowSize = atoi(token);
    token = strtok(NULL, " ");
    *colSize = atoi(token);

    char **matrix = sbrk(*rowSize * sizeof(char*)); // NOLINT(cppcoreguidelines-narrowing-conversions)

    for (int i = 0; i < *rowSize; i++)
    {
        *(matrix + i) = sbrk(*colSize * sizeof(char)); // NOLINT(cppcoreguidelines-narrowing-conversions)
        for (int j = 0; j < *colSize; j++)
        {
            fgets(buffer, 256, stdin);
            *(*(matrix + i) + j) = buffer[0]-48; // NOLINT(cppcoreguidelines-narrowing-conversions)
        }
    }

    // caller takes the ownership of this allocated memory
    return matrix;
}
