#include "../../headers/connectionMatrix/matrixVisualizer.h"


int matrixVisualizer(Node*** matrix, const IdosData* idosData)
{
    // prints the contents of all linked lists in the matrix for each node in linked list
    // in this format:
    // (1st index of the matrix) - (2nd index of the matrix) - (start_time) - (time_it_takes)
    //
    for (int i = 0; i < *idosData->stationCount; ++i) {
        for (int j = 0; j < *idosData->stationCount; ++j) {
            Node* node = matrix[i][j];
            while (node != NULLPTR) {
                printf("%d -> %d - %lu - %lu\n",
                       i, j,
                       *node->connection_value->start_time,
                       *node->connection_value->time_it_takes);
                node = node->next_connection;
            }
        }
    }

    return 0;
}