#include "../../headers/dijkstraSolver/updateDistanceValOfAdjacentVertices.h"

void updateDistanceValOfAdjacentVertices(const Node*** matrix,
                                         const int* src, int dist[], const bool sptSet[], const unsigned int* size)
{
    for(int v = 0; v < *size; v++)
    {
        // TODO: remake this so it actually uses the linked list and takes time into an account

        /*if(!sptSet[v] && matrix[*src][v] != NULL &&
        dist[*src] != INT_MAX &&
        dist[*src] + matrix[*src][v]->weight < dist[v])
        {
            dist[v] = dist[*src] + matrix[*src][v]->weight;
        }*/
    }
}