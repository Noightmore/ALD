#ifndef PROBLEM_IDOS_IDOSDATA_H
#define PROBLEM_IDOS_IDOSDATA_H

#include "../../connectionMatrix/include/treeNode.h"

typedef struct idosData {
        unsigned long* stationCount;
        unsigned long* connectionCount;
        treeNode*** connectionMatrix;

} idosData;

#endif //PROBLEM_IDOS_IDOSDATA_H
