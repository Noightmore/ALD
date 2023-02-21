#include "../include/initConnectionMatrix.h"

treeNode*** initConnectionMatrix(const unsigned long* size) {
    treeNode*** matrix =
            (treeNode***) malloc(*size * sizeof(treeNode*));

    for (unsigned long i = 0; i < *size; i++){
            matrix[i] =
                    (treeNode**) calloc(*size, sizeof(treeNode*));
    }

    return matrix;
}

