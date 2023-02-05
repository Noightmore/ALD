#pragma clang diagnostic push
#pragma ide diagnostic ignored "cppcoreguidelines-narrowing-conversions"
#include <unistd.h>
#include "../data/LinkedList.h"
#include "../data/Macros.h"

Node*** initConnectionMatrix(const unsigned long* size)
{
    Node*** data = sbrk(sizeof(Node**) * *size);
    for (int i=0; i < *size; i++)
    {
        data[i] = sbrk(sizeof(Node*) * *size);
        for (int j=0; j < *size; j++)
        {
            data[i][j] = NULLPTR;
        }
    }

    // caller takes ownership of the memory
    return data;
}
#pragma clang diagnostic pop