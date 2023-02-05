#include "tools/headers/loadInitialData.h"
#include "tools/headers/initConnectionMatrix.h"
#include "data/LinkedList.h"
#include "tools/headers/populateConnectionMatrix.h"

int main()
{
    IdosData* initialData = loadInitialData();
    Node*** matrix = initConnectionMatrix(initialData->stationCount);
    populateConnectionMatrix(matrix, initialData);
    //printf("start_time: %lu, time_it_takes: %lu\n", initialData->start_time, initialData->time_it_takes);
    return 0;
}
