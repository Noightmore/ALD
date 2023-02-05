#pragma clang diagnostic push
#pragma ide diagnostic ignored "cppcoreguidelines-narrowing-conversions"

#include "../../headers/connectionMatrix/initConnectionMatrix.h"

Node*** initConnectionMatrix(const unsigned long* size)
{
    Node*** data = malloc(sizeof(Node**) * *size);
    for (int i=0; i < *size; i++)
    {
        data[i] = malloc(sizeof(Node*) * *size);
        for (int j=0; j < *size; j++)
        {
            data[i][j] = NULLPTR;
        }
    }

    // caller takes ownership of the memory
    return data;
}
#pragma clang diagnostic pop