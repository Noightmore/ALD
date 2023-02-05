#ifndef PROBLEM_IDOS_POPULATECONNECTIONMATRIX_H
#define PROBLEM_IDOS_POPULATECONNECTIONMATRIX_H

#include <stdio.h>
#include <malloc.h>

#include "../../../data/LinkedList.h"
#include "../../../data/IdosData.h"
#include "../../../data/Macros.h"
#include "createNode.h"
#include "addConnection.h"

int populateConnectionMatrix(Node*** matrix, const IdosData* idosData);

#endif //PROBLEM_IDOS_POPULATECONNECTIONMATRIX_H
