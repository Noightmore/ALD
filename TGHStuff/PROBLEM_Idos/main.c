#include "tools/headers/loadInitialData.h"
#include "tools/headers/connectionMatrix/initConnectionMatrix.h"
#include "data/LinkedList.h"
#include "tools/headers/connectionMatrix/populateConnectionMatrix.h"
#include "tools/headers/connectionMatrix/matrixVisualizer.h"
#include "tools/headers/loadAndProcess_Requests.h"

int main()
{
    IdosData* initialData =
            loadInitialData();
    Node*** matrix =
            initConnectionMatrix(initialData->stationCount);

    int result = populateConnectionMatrix(matrix, initialData);
    //printf("start_time: %lu, time_it_takes: %lu\n", initialData->start_time, initialData->time_it_takes);

    if (result != 0)
    {
        return result;
    }

    // visualization for debugging only
    matrixVisualizer(matrix, initialData);

    result =
            loadAndProcess_Requests((const Node ***) matrix, initialData);

    return result;
}
