#pragma clang diagnostic push
#pragma ide diagnostic ignored "cppcoreguidelines-narrowing-conversions"
#include <unistd.h>
#include "../data/Node.h"

#define NULLPTR (Node*) 0x00;

Node*** initMatrix(const unsigned long* size)
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

    return data;
}
#pragma clang diagnostic pop