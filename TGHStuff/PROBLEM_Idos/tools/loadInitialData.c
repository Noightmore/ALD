#include <stdio.h>
#include <unistd.h>
#include "../data/Node.h"

Node* loadInitialData()
{
    char buffer[256];
    Node* initialData = sbrk(sizeof(Node));

    // load 1st line of input
    fgets(buffer, 256, stdin);
    sscanf(buffer, "%lu %lu", initialData->start, initialData->time_it_takes); // NOLINT(cert-err34-c)

    // caller takes the ownership of the memory
    return initialData;
}