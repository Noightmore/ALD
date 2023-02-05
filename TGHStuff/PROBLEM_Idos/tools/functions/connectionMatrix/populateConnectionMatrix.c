#include "../../headers/connectionMatrix/populateConnectionMatrix.h"

int populateConnectionMatrix(Node*** matrix, const IdosData* idosData)
{
    char buffer[BUFFER_SIZE];

    unsigned long start;
    unsigned long end;
    unsigned long start_time;
    unsigned long time_it_takes;

    for(int i = 0; i < *idosData->connectionCount; i++)
    {

        fgets(buffer, BUFFER_SIZE, stdin);
        sscanf(buffer, "%lu %lu %lu %lu", &start, &end, &start_time, &time_it_takes); // NOLINT(cert-err34-c)
        printf("start_time: %lu, end: %lu, start_time: %lu, time_it_takes: %lu\n",
               start, end, start_time, time_it_takes);

        int result = addConnection(&matrix[start][end], &start_time, &time_it_takes);
        printf("result: %d \n", result);

    }

    return 0;
}
