#include <stdio.h>

void printMatrix_InPlainFormat(char ***matrix, long rowSize, long colSize)
{
    for (int i = 0; i < rowSize; i++)
    {
        for (int j = 0; j < colSize; j++)
        {
            printf("%c", (*matrix)[i][j]+48);
            printf(" ");
        }
        printf("\n");
    }
}

