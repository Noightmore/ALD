#include "../include/loadInitialData.h"

idosData* loadInitialData() {
        char buffer[BUFFER_SIZE];

        idosData* initialData = malloc(sizeof(idosData));
        initialData->stationCount = malloc(sizeof(unsigned long));
        initialData->connectionCount = malloc(sizeof(unsigned long));

        // loads 1st line of input
        if(fgets(buffer, BUFFER_SIZE, stdin) == NULL) {
                return NULL;
        }
        sscanf(buffer, "%lu %lu", initialData->stationCount, initialData->connectionCount); // NOLINT(cert-err34-c)

        // caller takes the ownership of the memory
        return initialData;
}
