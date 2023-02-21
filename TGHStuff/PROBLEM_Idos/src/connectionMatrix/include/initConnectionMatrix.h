#ifndef PROBLEM_IDOS_INITCONNECTIONMATRIX_H
#define PROBLEM_IDOS_INITCONNECTIONMATRIX_H

#include <malloc.h>

#include "treeNode.h"

// just allocates memory for the matrix
// size = station count
// creates size x size matrix and zero initializes binary tree pointers to NULL
// returns a pointer to it
// caller takes the ownership of the memory
treeNode*** initConnectionMatrix(const unsigned long* size);

#endif //PROBLEM_IDOS_INITCONNECTIONMATRIX_H
