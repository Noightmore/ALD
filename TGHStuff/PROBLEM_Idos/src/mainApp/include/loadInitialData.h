#ifndef PROBLEM_IDOS_TEST_LOADINITIALDATA_H
#define PROBLEM_IDOS_LOADINITIALDATA_H

#include <stdio.h>
#include <malloc.h>

#include "idosData.h"
#include "macros.h"

// loads initial data from stdin
// initial data consist of 2 unsigned integers:
// 1. station count
// 2. connection count
//
// returns pointer to IdosData struct
// caller takes the ownership of the allocated memory
idosData* loadInitialData();

#endif //PROBLEM_IDOS_TEST_LOADINITIALDATA_H
