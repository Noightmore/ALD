#include <unistd.h>

#include "matrix_tools/headerFiles/loadMatrix_FromInput.h"
#include "matrix_tools/headerFiles/printMatrix_InPlainFormat.h"
#include "matrix_tools/headerFiles/transposeMatrix.h"

int main()
{
    long *dimensions = sbrk(2 * sizeof(long)); // NOLINT(cppcoreguidelines-narrowing-conversions)
    char** matrix = loadMatrix_FromInput(dimensions, (dimensions + 1));
    printMatrix_InPlainFormat(&matrix, *dimensions, *(dimensions + 1));
    char** transposedMatrix = transposeMatrix(&matrix, dimensions, (dimensions + 1));
    printMatrix_InPlainFormat(&transposedMatrix, *dimensions, *(dimensions + 1));
    return 0;
}
