#include "../../headers/dijkstraSolver/getMinDistance.h"

int getMinDistance(const int dist[], const bool sptSet[], const unsigned int* size)
{
    int min = INT_MAX, min_index;

    for (int i = 0; i < *size; i++)
    {
        if (!sptSet[i] && dist[i] <= min)
        {
            min = dist[i], min_index = i;
        }
    }

    return min_index;
}