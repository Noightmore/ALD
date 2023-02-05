#include <stdio.h>
#include <unistd.h>
#include "../data/LinkedList.h"
#include "../data/IdosData.h"
#include "../data/Macros.h"

int populateConnectionMatrix(Node*** matrix, const IdosData* idosData)
{
    char buffer[256];
    for(int i = 0; i < *idosData->connectionCount; i++)
    {
        unsigned long start;
        unsigned long end;
        unsigned long start_time;
        unsigned long time_it_takes;

        fgets(buffer, 256, stdin);
        sscanf(buffer, "%lu %lu %lu %lu", &start, &end, &start_time, &time_it_takes); // NOLINT(cert-err34-c)
        printf("start_time: %lu, end: %lu, start_time: %lu, time_it_takes: %lu\n",
               start, end, start_time, time_it_takes);

        if(matrix[start][end] == NULLPTR)
        {
            // TODO: make the allocation of node and settings its params a function
            matrix[start][end] = sbrk(sizeof(Node*));
            matrix[start][end]->connection_value = sbrk(sizeof(Connection*));
            matrix[start][end]->connection_value->start_time = sbrk(sizeof(unsigned long));
            matrix[start][end]->connection_value->time_it_takes = sbrk(sizeof(unsigned long));

            *matrix[start][end]->connection_value->start_time = start_time;
            *matrix[start][end]->connection_value->time_it_takes = time_it_takes;
            matrix[start][end]->next_connection = NULLPTR;
        }
        else
        {
            Node* currentConnectionNode = matrix[start][end];
            while(matrix[start][end]->next_connection != NULLPTR)
            {
                currentConnectionNode = currentConnectionNode->next_connection;
            }

            currentConnectionNode->next_connection = sbrk(sizeof(Node*));
            currentConnectionNode->next_connection->connection_value = sbrk(sizeof(Connection*));
            currentConnectionNode->next_connection->connection_value->start_time = sbrk(sizeof(unsigned long));
            currentConnectionNode->next_connection->connection_value->time_it_takes = sbrk(sizeof(unsigned long));

            *currentConnectionNode->next_connection->connection_value->start_time = start_time;
            *currentConnectionNode->next_connection->connection_value->time_it_takes = time_it_takes;
            currentConnectionNode->next_connection->next_connection = NULLPTR;

        }

    }

    return 0;
}
