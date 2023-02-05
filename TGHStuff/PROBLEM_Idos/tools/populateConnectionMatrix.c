#include <stdio.h>
#include <unistd.h>
#include "../data/LinkedList.h"
#include "../data/IdosData.h"
#include "../data/Macros.h"

// TODO: refactor this function
int populateConnectionMatrix(Node*** matrix, const IdosData* idosData)
{
    char buffer[BUFFER_SIZE];

    unsigned long start;
    unsigned long end;
    unsigned long start_time;
    unsigned long time_it_takes;

    for(int i = 0; i < *idosData->connectionCount; i++)
    {

        // TODO: check if the input is valid
        fgets(buffer, BUFFER_SIZE, stdin);
        sscanf(buffer, "%lu %lu %lu %lu", &start, &end, &start_time, &time_it_takes); // NOLINT(cert-err34-c)
        printf("start_time: %lu, end: %lu, start_time: %lu, time_it_takes: %lu\n",
               start, end, start_time, time_it_takes);


        // TODO: fix sussy bakka code
        // -------------------------------------------------------------------------------------------------------------
        if(matrix[start][end] == NULLPTR)
        {
            // TODO: make the allocation of node and settings its params a function
            // also if sbrk fails, we should handle it
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
