#include "../../headers/dijkstraSolver/findShortestPath.h"

int findShortestPath(const Node*** matrix, const IdosData* idosData, const unsigned int* inputData)
{
    int distances[*idosData->stationCount];
    bool visitedNodesSet[*idosData->stationCount];

    for(int i = 0; i < *idosData->stationCount; i++)
    {
        distances[i] = INT_MAX;
        visitedNodesSet[i] = false;
    }

    distances[inputData[0]] = 0;

    // find the shortest path
    for(int count = 0; count < *idosData->stationCount - 1; count++)
    {
        // get minimum distance to one of the unvisited nodes
        int u = getMinDistance(
                distances,
                visitedNodesSet,
                (const unsigned int *) idosData->stationCount);

        visitedNodesSet[u] = true; // already has been visited

        updateDistanceValOfAdjacentVertices(
                matrix,
                &u,
                distances,
                visitedNodesSet,
                (const unsigned int *) idosData->stationCount);
    }

    return 0;
}