#ifndef PROBLEM_IDOS_UPDATEDISTANCEVALOFADJACENTVERTICES_H
#define PROBLEM_IDOS_UPDATEDISTANCEVALOFADJACENTVERTICES_H

#include <stddef.h>
#include <limits.h>

#include "../../../data/LinkedList.h"

void updateDistanceValOfAdjacentVertices(const Node*** matrix,
                                        const int* src, int dist[], const bool sptSet[], const unsigned int* size);

#endif //PROBLEM_IDOS_UPDATEDISTANCEVALOFADJACENTVERTICES_H
