#include <stdio.h>
#include "data/Node.h"
#include "tools/headers/loadInitialData.h"

int main()
{
    Node* initialData = loadInitialData();


    //printf("start: %lu, time_it_takes: %lu\n", initialData->start, initialData->time_it_takes);
    return 0;
}
