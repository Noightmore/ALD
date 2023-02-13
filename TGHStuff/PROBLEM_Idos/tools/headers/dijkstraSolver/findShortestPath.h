#ifndef PROBLEM_IDOS_FINDSHORTESTPATH_H
#define PROBLEM_IDOS_FINDSHORTESTPATH_H

#include <limits.h>

#include "../../../data/IdosData.h"
#include "../../../data/LinkedList.h"
#include "getMinDistance.h"
#include "updateDistanceValOfAdjacentVertices.h"

int findShortestPath(const Node*** matrix, const IdosData* idosData, const unsigned int* inputData);

#endif //PROBLEM_IDOS_FINDSHORTESTPATH_H
