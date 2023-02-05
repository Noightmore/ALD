#include <stdio.h>
#include <unistd.h>
#include "../data/IdosData.h"
#include "../data/Macros.h"

IdosData* loadInitialData()
{
    char buffer[BUFFER_SIZE];
    IdosData* initialData = sbrk(sizeof(IdosData));
    initialData->stationCount = sbrk(sizeof(unsigned long));
    initialData->connectionCount = sbrk(sizeof(unsigned long));

    // load 1st line of input
    fgets(buffer, BUFFER_SIZE, stdin);
    sscanf(buffer, "%lu %lu", initialData->connectionCount, initialData->stationCount); // NOLINT(cert-err34-c)

    // caller takes the ownership of the memory
    return initialData;
}