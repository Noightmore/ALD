#include "data/Node.h"
#include "tools/headers/loadInitialData.h"
#include "tools/headers/initMatrix.h"

int main()
{
    IdosData* initialData = loadInitialData();
    Node*** matrix = initMatrix(initialData->);

    //printf("start: %lu, time_it_takes: %lu\n", initialData->start, initialData->time_it_takes);
    return 0;
}
